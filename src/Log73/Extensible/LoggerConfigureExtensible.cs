namespace Log73.Extensible;

public class LoggerConfigureExtensible
{
    public Log73Logger Logger { get; }
    public LoggerConfigureExtensible(Log73Logger logger) => Logger = logger;
}