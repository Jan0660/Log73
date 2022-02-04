namespace Log73;

public class AnsiCodes
{
    /// <summary>
    /// Disable all ANSI effects.
    /// </summary>
    public const string Reset = "\u001b[0m";

    public const string Bold = "\u001b[1m";
    public const string NormalIntensity = "\u001b[22m";
    public const string Faint = "\u001b[2m";
    public const string Underline = "\u001b[4m";
    public const string UnderlineOff = "\u001b[24m";
    public const string Italic = "\u001b[3m";
    public const string ItalicOff = "\u001b[23m";
    public const string SlowBlink = "\u001b[5m";
    public const string RapidBlink = "\u001b[6m";
    public const string BlinkOff = "\u001b[25m";
    public const string Invert = "\u001b[7m";
    public const string InvertOff = "\u001b[27m";
    public const string Strikethrough = "\u001b[9m";
    public const string StrikethroughOff = "\u001b[29m";
    public const string DefaultForegroundColor = "\u001b[39m";
    public const string DefaultBackgroundColor = "\u001b[49m";
}