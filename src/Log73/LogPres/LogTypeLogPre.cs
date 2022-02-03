namespace Log73.LogPres;

public class LogTypeLogPre : LogPre
{
    public override byte SpaceOutTo { get; set; } = 5;

    public override bool Write(in LogContext context, ref SpanStringBuilder builder)
    {
        builder.Append(context.LogType?.Name);
        return context.LogType?.Name != null;
    }
    public override void SetStyle(in LogContext context)
    {
        Style = context.LogType?.LogPreStyle;
    }
}