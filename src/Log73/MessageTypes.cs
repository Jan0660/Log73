using System.Collections.Generic;
using System.Drawing;

namespace Log73
{
    /// <summary>
    /// Stores the default <see cref="MessageType"/>s included with Log73.
    /// </summary>
    public class MessageTypes
    {
        public static MessageType Info = new()
        {
            LogType = LogType.Info,
            Name = "Info",
            Style = new() { ForegroundColor = Color.Cyan }
        };

        public static MessageType Start = new()
        {
            LogType = LogType.Info,
            Name = "Start",
            Style = new() { ForegroundColor = Color.Cyan }
        };

        public static MessageType Done = new()
        {
            LogType = LogType.Info,
            Name = "Done",
            Style = new() { ForegroundColor = Color.Lime }
        };

        public static MessageType Warn = new()
        {
            LogType = LogType.Warn,
            Name = "Warn",
            Style = new() { ForegroundColor = Color.Yellow }
        };

        public static MessageType Error = new()
        {
            LogType = LogType.Error,
            Name = "Error",
            Style = new() { ForegroundColor = Color.Red },
            WriteToStdErr = true
        };

        public static MessageType Debug = new()
        {
            LogType = LogType.Debug,
            Name = "Debug",
            Style = new() { ForegroundColor = Color.White }
        };

        /// <summary>
        /// Gets the corresponding <see cref="MessageType"/> to the specified <see cref="LogType"/>.
        /// </summary>
        /// <param name="logType"></param>
        /// <returns></returns>
        public static MessageType Get(LogType logType)
            => logType switch
            {
                LogType.Info => Info,
                LogType.Warn => Warn,
                LogType.Error => Error,
                LogType.Debug => Debug,
            };

        /// <summary>
        /// Gets all of the <see cref="MessageType"/>s in this class as an array.
        /// </summary>
        /// <returns><see cref="Info"/>, <see cref="Warn"/>, <see cref="Error"/>, <see cref="Debug"/>, <see cref="Start"/> and <see cref="Done"/> in an array, in this order.</returns>
        public static MessageType[] AsArray()
            => new[] { Info, Warn, Error, Debug, Start, Done };
    }

    /// <summary>
    /// Class to store the information on how to log a message like it's <see cref="Log73.LogType"/> and <see cref="Log73Style" />s.
    /// </summary>
    public class MessageType
    {
        public LogType LogType = LogType.Info;
        public string Name;
        public Log73Style Style = new();
        public List<ILogInfo> LogInfos = new();
        public bool WriteToStdErr = false;

        /// <summary>
        /// The style to be used for the content of the message
        /// </summary>
        public Log73Style ContentStyle = new();
    }
}