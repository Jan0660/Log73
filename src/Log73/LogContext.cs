namespace Log73;

public ref struct LogContext
{
    public ReadOnlySpan<char> Message { get; set; }
    public LogType? LogType { get; set; }
    public bool IsWriteLine { get; set; }
    public object? ExtraContext { get; set; }
}