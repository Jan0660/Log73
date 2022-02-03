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

    #region Logger methods pass-through

    /// <inheritdoc cref="Log73Logger.WriteLine(object)"/>
    public static void WriteLine(object? value)
        => Logger.WriteLine(value?.ToString());

    /// <inheritdoc cref="Log73Logger.WriteLine(string)"/>
    public static void WriteLine(string? value)
        => Logger.WriteLine(value);

    /// <inheritdoc cref="Log73Logger.WriteLine()"/>
    public static void WriteLine()
        => Logger.WriteLine(null);

    /// <inheritdoc cref="Log73Logger.Info(object)"/>
    public static void Info(object? value)
        => Logger.Info(value);

    /// <inheritdoc cref="Log73Logger.Info(string)"/>
    public static void Info(string? value)
        => Logger.Info(value);

    /// <inheritdoc cref="Log73Logger.Warn(object)"/>
    public static void Warn(object? value)
        => Logger.Warn(value);

    /// <inheritdoc cref="Log73Logger.Warn(string)"/>
    public static void Warn(string? value)
        => Logger.Warn(value);

    /// <inheritdoc cref="Log73Logger.Error(object)"/>
    public static void Error(object? value)
        => Logger.Error(value);

    /// <inheritdoc cref="Log73Logger.Error(string)"/>
    public static void Error(string? value)
        => Logger.Error(value);

    /// <inheritdoc cref="Log73Logger.Debug(object)"/>
    public static void Debug(object? value)
        => Logger.Debug(value);

    /// <inheritdoc cref="Log73Logger.Debug(string)"/>
    public static void Debug(string? value)
        => Logger.Debug(value);

    #endregion
}