namespace Log73;

public class LogType
{
    public string? Name { get; set; }
    public List<LogPre>? LogPres { get; set; }

    /// <summary>
    /// Whether to write to stderr or stdout.
    /// </summary>
    public bool StdErr { get; set; }
    public Style? ContentStyle { get; set; }
    public Style? LogPreStyle { get; set; }
    public LogLevel LogLevel { get; set; }
    
    public LogType(string? name = null, LogLevel logLevel = LogLevel.Fatal, bool stderr = false)
    {
        Name = name;
        StdErr = stderr;
        LogLevel = logLevel;
    }
}