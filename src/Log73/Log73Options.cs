using System.Drawing;
using Log73.ColorSchemes;

namespace Log73
{
    public enum LogLevel
    {
        /// <summary>
        /// Only logs <see cref="LogType.Error"/> and <see cref="LogType.Warn"/> messages.
        /// </summary>
        Quiet,

        /// <summary>
        /// Logs <see cref="LogType.Error"/>, <see cref="LogType.Warn"/> and <see cref="LogType.Info"/> messages.
        /// </summary>
        Standard,

        /// <summary>
        /// Logs every <see cref="LogType"/>.
        /// </summary>
        Debug
    }

    public class Log73Options
    {
        public LogLevel LogLevel = LogLevel.Standard;

        /// <summary>
        /// The <see cref="MessageType"/> used in <see cref="Log73.ExtensionMethod.DumpExtensionMethods.Dump"/>.
        /// </summary>
        public MessageType DumpMessageType = MessageTypes.Info;

        /// <summary>
        /// The <see cref="IObjectSerializer"/> used for serializing objects.
        /// </summary>
        public IObjectSerializer ObjectSerializer = new ToStringSerializer();

        /// <summary>
        /// If <see langword="true"/> there are brackets ('[' and ']') around an <see cref="ILogInfo"/>'s value and the message's <see cref="LogType"/>.
        /// </summary>
        public bool UseBrackets = true;

        /// <summary>
        /// Write a space after a 
        /// </summary>
        public bool SpaceAfterInfo = true;

        /// <summary>
        /// Disables or enables the use of Ansi escape codes when logging.
        /// </summary>
        public bool UseAnsi = true;

        /// <summary>
        /// When <see cref="UseAnsi" /> is <see langword="true"/>, <see cref="ILogInfo"/>s are written together with the message
        /// and not written in separate write calls, if <see cref="UseAnsi" /> is <see langword="false"/> and this 
        /// is <see langword="true"/> they ARE written in separate calls because of limitations.
        /// </summary>
        public bool SeparateLogInfoWriteCalls = false;

        /// <summary>
        /// The 16 color <see cref="T:Log73.ColorSchemes.IColorScheme" /> to use when <see cref="UseAnsi" /> is <see langword="false"/>
        /// </summary>
        public IColorScheme ColorScheme = new WindowsConsoleColorScheme();

        public bool AlwaysLogTaskStart = false;

        /// <summary>
        /// The <see cref="MessageType"/> used for <see cref="Log73.Console.WriteLine(object)"/> and <see cref="Log73.Console.Log(object)"/>.
        /// </summary>
        public MessageType WriteLineMessageType = MessageTypes.Info;
    }

    public class Log73Style
    {
        public Color? ForegroundColor;
        public Color? BackgroundColor;
        public bool Bold;
        public bool Italic;
        public bool Underline;
        public bool ToUpper = true;
        public bool Invert;
        public bool CrossedOut;
        public bool SlowBlink;
        public bool RapidBlink;
        public bool Faint;

        /// <summary>
        /// Get a styled string.
        /// </summary>
        /// <param name="str">The string to style.</param>
        /// <param name="options"></param>
        /// <returns>The styled string.</returns>
        public string Style(string str, Log73Options options)
        {
            if (Bold)
                str = Ansi.Bold + str + Ansi.BoldOff;
            if (Italic)
                str = Ansi.Italic + str + Ansi.NotItalic;
            if (Underline)
                str = Ansi.Underline + str + Ansi.UnderlineOff;
            if (Invert)
                str = Ansi.Invert + str + Ansi.InvertOff;
            if (CrossedOut)
                str = Ansi.CrossedOut + str + Ansi.CrossedOutOff;
            if (SlowBlink)
                str = Ansi.SlowBlink + str + Ansi.BlinkOff;
            if (RapidBlink)
                str = Ansi.RapidBlink + str + Ansi.BlinkOff;
            if (Faint)
                str = Ansi.Faint + str + Ansi.NormalColor;
            str = Ansi.ApplyColor(str, ForegroundColor, BackgroundColor);
            return str;
        }
    }
}