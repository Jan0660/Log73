using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Styles = Log73.ConsoleStyleOptions;
namespace Log73
{
    public static class MessageTypes
    {
        private static ConsoleStyleOptions Styles => Console.Options.Style;
        public static MessageType Info = new()
        {
            LogType = LogType.Info,
            Name = "Info",
            Style = Styles.Info
        };
        public static MessageType Start = new()
        {
            LogType = LogType.Info,
            Name = "Start",
            Style = Styles.Info
        };
        public static MessageType Done = new()
        {
            LogType = LogType.Info,
            Name = "Done",
            Style = new() { Color = Color.Lime }
        };
        public static MessageType Warn = new()
        {
            LogType = LogType.Warn,
            Name = "Warn",
            Style = Styles.Warn
        };
        public static MessageType Error = new()
        {
            LogType = LogType.Error,
            Name = "Error",
            Style = Styles.Error
        };
        public static MessageType Debug = new()
        {
            LogType = LogType.Debug,
            Name = "Debug",
            Style = Styles.Debug
        };

        public static MessageType Get(LogType logType)
            => logType switch
            {
                LogType.Info => Info,
                LogType.Warn => Warn,
                LogType.Error => Error,
                LogType.Debug => Debug,
            };
    }

    public class MessageType
    {
        public LogType LogType;
        public string Name;
        public ConsoleStyleOption Style = new();
        public List<ILogInfo> LogInfos = new();
        /// <summary>
        /// The style to be used for the content of the message
        /// </summary>
        public ConsoleStyleOption ContentStyle = new();
    }
}
