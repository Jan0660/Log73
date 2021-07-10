using System.Drawing;
using System.Xml;
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
    public class ConsoleOptions
    {
        public LogLevel LogLevel = LogLevel.Standard;

        public MessageType DumpMessageType = MessageTypes.Info;

        public IObjectSerializer ObjectSerializer = new ToStringSerializer();

        /// <summary>
        /// If <see langword="true"/> there are brackets ('[' and ']') around an <see cref="ILogInfo"/>'s value and the message's <see cref="LogType"/>.
        /// </summary>
        public bool UseBrackets = true;

        public bool SpaceAfterInfo = true;
        private bool _useAnsi = true;

        /// <summary>
        /// Disables or enables the use of Ansi escape codes when logging. When setting this property <see cref="Use24BitAnsi"/> is also set to the
        /// specified value.
        /// </summary>
        public bool UseAnsi
        {
            get
            {
                return _useAnsi;
            }
            set
            {
                _useAnsi = value;
                Use24BitAnsi = value;
            }
        }

        /// <summary>
        /// Disables or enables the use of 24bit color Ansi escape codes.
        /// </summary>
        public bool Use24BitAnsi = true;

        /// <summary>
        /// When <see cref="Use24BitAnsi" /> is <see langword="true"/>, <see cref="ILogInfo"/>s are written together with the message
        /// and not written in separate write calls, if <see cref="Use24BitAnsi" /> is <see langword="false"/> and this 
        /// is <see langword="true"/> they ARE written in seperate calls because of limitations.
        /// </summary>
        public bool SeparateLogInfoWriteCalls = false;

        /// <summary>
        /// The 16 color <see cref="T:Log73.ColorSchemes.IColorScheme" /> to use when <see cref="Use24BitAnsi" /> is <see langword="false"/>
        /// </summary>

        public IColorScheme ColorScheme = new WindowsConsoleColorScheme();

        public XmlWriterSettings XmlWriterSettings = new() { Indent = true };

        public bool AlwaysLogTaskStart = false;

        public MessageType WriteLineMessageType = MessageTypes.Info;
        public ConsoleOptions()
        {

        }
    }

    public class ConsoleStyleOptions
    {
        public ConsoleStyleOption Info = new() { Color = Color.Cyan };
        public ConsoleStyleOption Warn = new() { Color = Color.Yellow };
        public ConsoleStyleOption Error = new() { Color = Color.Red };
        public ConsoleStyleOption Debug = new() { Color = Color.White };
        /// <summary>
        /// if you want to put a space after message type
        /// </summary>
        public bool SpaceAfterType = true;
        public ConsoleStyleOption Get(LogType logType)
            => logType switch
            {
                LogType.Info => Info,
                LogType.Warn => Warn,
                LogType.Error => Error,
                LogType.Debug => Debug
            };
    }

    public class ConsoleStyleOption
    {
        public Color? Color;
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
    }
}
