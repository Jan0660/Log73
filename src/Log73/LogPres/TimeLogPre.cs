using System.Drawing;

namespace Log73.LogPres;

public class TimeLogPre : LogPre
{
    public override Style? Style { get; set; } = new()
    {
        ForegroundColor = Color.Gold
    };

    public string Format { get; set; } = "hh:mm:ss";

    public override bool Write(in LogContext context, ref SpanStringBuilder builder)
    {
        builder.AppendDateTime(DateTime.Now, Format);
        return true;
    }
}