using System.Drawing;

namespace Log73;

public static class AnsiExtensions
{
    public static void ForegroundColor(ref this SpanStringBuilder sb, in Color color)
    {
        sb.Append("\x1b[38;2;");
        sb.AppendNumber(color.R);
        sb.Append(";");
        sb.AppendNumber(color.G);
        sb.Append(";");
        sb.AppendNumber(color.B);
        sb.Append("m");
    }
    public static void BackgroundColor(ref this SpanStringBuilder sb, in Color color)
    {
        sb.Append("\x1b[48;2;");
        sb.AppendNumber(color.R);
        sb.Append(";");
        sb.AppendNumber(color.G);
        sb.Append(";");
        sb.AppendNumber(color.B);
        sb.Append("m");
    }
}