namespace Log73;

public class LoggerOptions
{
    public bool LogPreSpace { get; set; } = true;
    public LogLevel LogLevel { get; set; } = LogLevel.Info;
    public int InitialWriteLength { get; set; } = 1_000;
    public int WriteLengthOverhead { get; set; } = 500;
}