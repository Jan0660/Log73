namespace Log73;

public abstract class LogPre
{
    public virtual Style? Style { get; set; }
    public virtual byte SpaceOutTo { get; set; }
    public abstract bool Write(in LogContext context, ref SpanStringBuilder builder);

    public virtual void SetStyle(in LogContext context)
    {
        
    }
}
