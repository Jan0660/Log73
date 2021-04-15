using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MLogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Log73.Extensions.Logging
{
    public class Log73LoggerConfiguration
    {
        public MessageType Trace = _createMessageType("Trace", MessageTypes.Debug.Style);
        public MessageType Debug = _createMessageType("Debug", MessageTypes.Debug.Style);
        public MessageType Information = _createMessageType("Info", MessageTypes.Info.Style);
        public MessageType Warning = _createMessageType("Warning", MessageTypes.Warn.Style);
        public MessageType Error = _createMessageType("Error", MessageTypes.Error.Style, true);
        public MessageType Critical = _createMessageType("Critical", MessageTypes.Error.Style, true);

        public MLogLevel LogLevel { get; set; } = MLogLevel.Information;

        public MessageType GetMessageType(MLogLevel logLevel)
            => logLevel switch
            {
                MLogLevel.Critical => Critical,
                MLogLevel.Error => Error,
                MLogLevel.Warning => Warning,
                MLogLevel.Information => Information,
                MLogLevel.Debug => Debug,
                MLogLevel.Trace => Trace
            };

        private static MessageType _createMessageType(string name, ConsoleStyleOption style, bool writeToStdErr = false)
            => new()
            {
                Name = name,
                LogType = LogType.Error,
                Style = style,
                LogInfos = new()
                {
                    new LoggerNameLogInfo(),
                    new EventIdLogInfo()
                },
                WriteToStdErr = writeToStdErr
            };
    }
}