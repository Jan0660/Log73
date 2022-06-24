using Log73.Extensible;
using SConsole = System.Console;

namespace Log73;

public partial class Log73Logger
{
    /// <summary>
    /// Set to <see cref="System.Console.Out"/> by default.
    /// </summary>
    public TextWriter Out { get; set; } = SConsole.Out;

    /// <summary>
    /// Set to <see cref="System.Console.Error"/> by default.
    /// </summary>
    public TextWriter Err { get; set; } = SConsole.Error;

    public LoggerOptions Options = new();

    public LoggerLogTypes LogTypes = new();

    public Func<LogType?, bool> LogFilter { get; set; }
    public LogFunction LogFunction { get; set; }
    public LogWrite LogWrite { get; set; }
    public LoggerConfigureExtensible Configure { get; }
    public LoggerObjectExtensible Object { get; }

    public Log73Logger()
    {
        LogFilter = DefaultLogFilter;
        LogFunction = AnsiConsoleLogFunction;
        LogWrite = ConsoleLogWrite;
        Configure = new(this);
        Object = new(this);
    }

    public bool DefaultLogFilter(LogType? logType)
        => (byte)(logType?.LogLevel ?? LogLevel.Info) >= (byte)Options.LogLevel;


    // todo: make ReadOnlySpan<char> overloads for these methods
    // todo: some kind of serialization stuff like in v1
    public void WriteLine()
        => PreWrite(null, LogTypes.WriteLine);


    public void PreWrite(in ReadOnlySpan<char> value, LogType? logType, bool isWriteLine = true, object? extraContext = null)
    {
        if (!LogFilter(logType))
            return;
        LogFunction(new LogContext()
        {
            LogType = logType,
            Message = value,
            IsWriteLine = isWriteLine,
            ExtraContext = extraContext,
        });
    }

    public void AnsiConsoleLogFunction(in LogContext context)
    {
        var writeLength = Options.InitialWriteLength;
        // todo: add mode where instead of this it just abandons styling the message
        if (writeLength < context.Message.Length + Options.WriteLengthOverhead)
            writeLength += context.Message.Length;
        Span<char> finalSpan = stackalloc char[writeLength];
        var finalBuilder = new SpanStringBuilder(ref finalSpan);
        // LogPres
        if (context.LogType?.LogPres != null)
            foreach (var logpre in context.LogType.LogPres)
            {
                logpre.SetStyle(context);
                bool writeSpace;
                int writtenChars;
                if (logpre.Style != null)
                    writeSpace = logpre.Style.WriteLogPreStyled(ref finalBuilder, context, logpre, out writtenChars);
                else
                {
                    var prePos = finalBuilder.Position;
                    writeSpace = logpre.Write(context, ref finalBuilder);
                    writtenChars = finalBuilder.Position - prePos;
                }
                if (logpre.SpaceOutTo != 0)
                    for (var i = writtenChars; i < logpre.SpaceOutTo; i++)
                        finalBuilder.Append(' ');

                if (Options.LogPreSpace && writeSpace)
                    finalBuilder.Append(' ');
            }

        // Message
        if (context.LogType?.ContentStyle != null)
            context.LogType.ContentStyle.WriteStyled(ref finalBuilder, context.Message);
        else
            finalBuilder.Append(context.Message);

        // actually write it
        
        LogWrite(finalSpan[..finalBuilder.Position], context);
    }
    
    public void ConsoleLogWrite(in ReadOnlySpan<char> message, in LogContext context)
    {
        // todo: lol
        if (context.IsWriteLine)
            if (context.LogType?.StdErr ?? false)
                Err.WriteLine(message);
            else
                Out.WriteLine(message);
        else
        {
            if (context.LogType?.StdErr ?? false)
                Err.Write(message);
            else
                Out.Write(message);
        }
    }
}