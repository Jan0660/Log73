using System.Drawing;

namespace Log73;

public class Style
{
    public Color? ForegroundColor { get; set; }
    public Color? BackgroundColor { get; set; }
    // todo: can probably store these in a bitfield/flag enum for memory efficiency
    public bool Bold { get; set; }
    public bool Italic { get; set; }
    public bool Underline { get; set; }
    public bool Invert { get; set; }
    public bool CrossedOut { get; set; }
    public bool SlowBlink { get; set; }
    public bool RapidBlink { get; set; }
    public bool Faint { get; set; }

    private void WriteBeforeStyle(ref SpanStringBuilder builder)
    {
        if (Bold)
            builder.Append(AnsiCodes.Bold);
        if (Italic)
            builder.Append(AnsiCodes.Italic);
        if (Underline)
            builder.Append(AnsiCodes.Underline);
        if (Invert)
            builder.Append(AnsiCodes.Invert);
        if (CrossedOut)
            builder.Append(AnsiCodes.Strikethrough);
        if (SlowBlink)
            builder.Append(AnsiCodes.SlowBlink);
        if (RapidBlink)
            builder.Append(AnsiCodes.RapidBlink);
        if (Faint)
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