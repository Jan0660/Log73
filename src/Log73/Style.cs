using System.Drawing;

namespace Log73;

[Flags]
public enum AnsiStyle : byte
{
    Bold = 0b0000_0001,
    Faint = 0b0000_0010,
    Italic = 0b0000_0100,
    Underline = 0b0000_1000,
    Invert = 0b0001_0000,
    Strikethrough = 0b0010_0000,
    SlowBlink = 0b0100_0000,
    RapidBlink = 0b1000_0000,
}

public class Style
{
    public Color? ForegroundColor { get; set; }
    public Color? BackgroundColor { get; set; }
    public AnsiStyle AnsiStyle { get; set; }

    private void WriteBeforeStyle(ref SpanStringBuilder builder)
    {
        if (AnsiStyle.HasFlag(AnsiStyle.Bold))
            builder.Append(AnsiCodes.Bold);
        if (AnsiStyle.HasFlag(AnsiStyle.Italic))
            builder.Append(AnsiCodes.Italic);
        if (AnsiStyle.HasFlag(AnsiStyle.Underline))
            builder.Append(AnsiCodes.Underline);
        if (AnsiStyle.HasFlag(AnsiStyle.Invert))
            builder.Append(AnsiCodes.Invert);
        if (AnsiStyle.HasFlag(AnsiStyle.Strikethrough))
            builder.Append(AnsiCodes.Strikethrough);
        if (AnsiStyle.HasFlag(AnsiStyle.SlowBlink))
            builder.Append(AnsiCodes.SlowBlink);
        if (AnsiStyle.HasFlag(AnsiStyle.RapidBlink))
            builder.Append(AnsiCodes.RapidBlink);
        if (AnsiStyle.HasFlag(AnsiStyle.Faint))
            builder.Append(AnsiCodes.Faint);
        if(ForegroundColor.HasValue)
            builder.ForegroundColor(ForegroundColor.Value);
        if(BackgroundColor.HasValue)
            builder.BackgroundColor(BackgroundColor.Value);
    }

    public void WriteStyled(ref SpanStringBuilder builder, in ReadOnlySpan<char> message)
    {
        WriteBeforeStyle(ref builder);
        builder.Append(message);
        builder.Append(AnsiCodes.Reset);
    }
    public bool WriteLogPreStyled(ref SpanStringBuilder builder, in LogContext context, LogPre logpre, out int writtenChars)
    {
        WriteBeforeStyle(ref builder);
        var prePos = builder.Position;
        var writeSpace = logpre.Write(context, ref builder);
        writtenChars = builder.Position - prePos;
        builder.Append(AnsiCodes.Reset);
        return writeSpace;
    }
}