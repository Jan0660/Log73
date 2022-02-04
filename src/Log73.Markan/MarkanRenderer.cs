using System.Collections.ObjectModel;
using System.Text;
using static Log73.Markan.MarkanRenderer.Trilean;

namespace Log73.Markan;

public static class MarkanRenderer
{
    private static readonly ReadOnlyDictionary<Token, string> TokenValues = new(new Dictionary<Token, string>
    {
        [Token.Italic] = "*",
        [Token.Bold] = "**",
        [Token.Underline] = "__",
        [Token.Strikethrough] = "~~",
    });

    internal enum Token : byte
    {
        Italic = 0b_0000_0001,
        Bold = 0b_0000_0010,
        Underline = 0b_0000_0100,
        Strikethrough = 0b_0000_1000,
    }

    internal enum Trilean : byte
    {
        False,
        True,
        Neither,
    }

    public static string Render(string input)
    {
        var builder = new StringBuilder();
        // todo: substitute this with a wrap of a Span<>?
        var inside = new List<Token>(TokenValues.Count);
        for (var i = 0; i < input.Length; i++)
        {
            var ch = input[i];

            bool IsMatchStr(string toMatch)
            {
                if (i + toMatch.Length > input.Length)
                    return false;
                // check if the next n chars are the same and if not escaped by a \
                if (input.AsSpan()[i..(i + toMatch.Length)].SequenceEqual(toMatch) && (i != 0 ? input[i - 1] != '\\' : true))
                {
                    i += toMatch.Length - 1;
                    return true;
                }

                return false;
            }

            // returns True if beginning, Neither if ending, or False if not a match
            Trilean IsMatch(Token token)
            {
                var toMatch = TokenValues[token];
                if (!IsMatchStr(toMatch)) return False;
                if (inside.Contains(token))
                {
                    inside.Remove(token);
                    return Neither;
                }

                inside.Add(token);
                return True;
            }

            bool Do(Token token, string on, string off)
            {
                var m = IsMatch(token) switch
                {
                    True => on,
                    Neither => off,
                    _ => "",
                };
                if (m == "") return false;
                builder.Append(m);
                return true;
            }

            if (!Do(Token.Bold, AnsiCodes.Bold, AnsiCodes.NormalIntensity))
                if (!Do(Token.Italic, AnsiCodes.Italic, AnsiCodes.ItalicOff))
                    if (!Do(Token.Underline, AnsiCodes.Underline, AnsiCodes.UnderlineOff))
                        if (!Do(Token.Strikethrough, AnsiCodes.Strikethrough, AnsiCodes.StrikethroughOff))
                        {
                            bool DoColor(bool isForeground)
                            {
                                if (IsMatchStr(isForeground ? "[f#" : "[b#"))
                                {
                                    // find next ]
                                    var end = input.IndexOf(']', i);
                                    if (end == -1)
                                        throw new Exception("Unclosed color");
                                    i++;
                                    var len = end - i;
                                    if (len == 6)
                                    {
                                        var hex = input.AsSpan()[i..(i + len)];
                                        // parse hex into color
                                        var color = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
                                        var c = System.Drawing.Color.FromArgb(color);
                                        Span<char> buffer = stackalloc char[50];
                                        var bu = new SpanStringBuilder(ref buffer);
                                        if (isForeground) bu.ForegroundColor(c);
                                        else bu.BackgroundColor(c);
                                        builder.Append(buffer[..bu.Position]);
                                    }
                                    else if (len == 0)
                                        builder.Append(isForeground
                                            ? AnsiCodes.DefaultForegroundColor
                                            : AnsiCodes.DefaultBackgroundColor);

                                    i = end;
                                    return true;
                                }
                                else return false;
                            }

                            bool DoHyperlink()
                            {
                                if (!IsMatchStr("[")) return false;
                                var end = input.IndexOf(']', i);
                                var firstL = input.IndexOf('(', i);
                                var endL = input.IndexOf(')', i);
                                if (end == -1 || firstL == -1 || endL == -1)
                                    throw new Exception("Unclosed hyperlink");
                                var text = input.AsSpan()[(i + 1)..end];
                                var link = input.AsSpan()[(firstL + 1)..endL];
                                builder.Append($"\u001b]8;;{link}\u001b\\{text}\u001b]8;;\u001b\\");
                                i = endL + 1;
                                return true;
                            }

                            if (DoColor(true))
                                continue;
                            if (DoColor(false))
                                continue;
                            if (DoHyperlink())
                                continue;
                            builder.Append(ch);
                        }
        }

        return builder.ToString();
    }
}