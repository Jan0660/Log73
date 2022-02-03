using System.Drawing;
using Log73.LogPres;

namespace Log73;

public class LoggerLogTypes
{
    // todo: some kind of DefaultInstance for LogTypeLogPre
    public LogType? Info = new("Info", LogLevel.Info)
    {
        LogPreStyle = new()
        {
            ForegroundColor = Color.Aqua,
        },
        LogPres = new() { new LogTypeLogPre() },
    };

    public LogType? Error = new("Error", LogLevel.Error)
    {
        LogPreStyle = new()
        {
            ForegroundColor = Color.Red,
        },
        StdErr = true,
        LogPres = new() { new LogTypeLogPre() },
    };

    public LogType? Warn = new("Warn", LogLevel.Warn)
    {
        LogPreStyle = new()
        {
            ForegroundColor = Color.Yellow,
        },
        LogPres = new() { new LogTypeLogPre() },
    };

    public LogType? Debug = new("Debug", LogLevel.Debug)
    {
        LogPreStyle = new()
        {
            ForegroundColor = Color.White,
        },
        LogPres = new() { new LogTypeLogPre() },
    };

    public LogType? WriteLine;

    public void TraditionalsAddPre(LogPre logpre)
    {
        foreach (var logType in TraditionalsAsSpan())
        {
            if (logType == null) continue;
            logType.LogPres ??= new();
            logType.LogPres.Add(logpre);
        }
    }

    public LogType?[] TraditionalsAsArray() => new[] { Info, Error, Warn, Debug };
    public ReadOnlySpan<LogType?> TraditionalsAsSpan() => new[] { Info, Error, Warn, Debug };
}