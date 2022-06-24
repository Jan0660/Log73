using Log73.Extensible;

namespace Log73;

public static partial class Console
{
    public static Log73Logger Logger { get; set; } = new();

    #region Logger properties pass-through

    /// <inheritdoc cref="Log73Logger.Out"/>
    public static TextWriter Out
    {
        get => Logger.Out;
        set => Logger.Out = value;
    }

    /// <inheritdoc cref="Log73Logger.Err"/>
    public static TextWriter Err
    {
        get => Logger.Err;
        set => Logger.Err = value;
    }

    /// <inheritdoc cref="Log73Logger.Options"/>
    public static LoggerOptions Options
    {
        get => Logger.Options;
        set => Logger.Options = value;
    }

    /// <inheritdoc cref="Log73Logger.LogTypes"/>
    public static LoggerLogTypes LogTypes
    {
        get => Logger.LogTypes;
        set => Logger.LogTypes = value;
    }

    /// <inheritdoc cref="Log73Logger.Configure"/>
    public static LoggerConfigureExtensible Configure => Logger.Configure;

    /// <inheritdoc cref="Log73Logger.Object"/>
    public static LoggerObjectExtensible Object => Logger.Object;

    #endregion

    /// <inheritdoc cref="Log73Logger.WriteLine()"/>
    public static void WriteLine()
        => Logger.WriteLine(null);
}