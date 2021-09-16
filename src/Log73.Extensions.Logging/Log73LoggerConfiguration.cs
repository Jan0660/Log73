using MLogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Log73.Extensions.Logging
{
    /// <summary>
    /// The configuration for <see cref="Log73Logger"/>
    /// </summary>
    public class Log73LoggerConfiguration
    {
        /// <summary>
        /// The <see cref="MessageType"/> used for <see cref="Microsoft.Extensions.Logging.LogLevel.Trace"/>.
        /// </summary>
        public MessageType Trace = _createMessageType("Trace", MessageTypes.Debug.Style);
        /// <summary>
        /// The <see cref="MessageType"/> used for <see cref="Microsoft.Extensions.Logging.LogLevel.Debug"/>.
        /// </summary>
        public MessageType Debug = _createMessageType("Debug", MessageTypes.Debug.Style);
        /// <summary>
        /// The <see cref="MessageType"/> used for <see cref="Microsoft.Extensions.Logging.LogLevel.Information"/>.
        /// </summary>
        public MessageType Information = _createMessageType("Info", MessageTypes.Info.Style);
        /// <summary>
        /// The <see cref="MessageType"/> used for <see cref="Microsoft.Extensions.Logging.LogLevel.Warning"/>.
        /// </summary>
        public MessageType Warning = _createMessageType("Warning", MessageTypes.Warn.Style);
        /// <summary>
        /// The <see cref="MessageType"/> used for <see cref="Microsoft.Extensions.Logging.LogLevel.Error"/>.
        /// </summary>
        public MessageType Error = _createMessageType("Error", MessageTypes.Error.Style, true);
        /// <summary>
        /// The <see cref="MessageType"/> used for <see cref="Microsoft.Extensions.Logging.LogLevel.Critical"/>.
        /// </summary>
        public MessageType Critical = _createMessageType("Critical", MessageTypes.Error.Style, true);

        public MLogLevel LogLevel { get; set; } = MLogLevel.Information;

        /// <summary>
        /// Gets the corresponding <see cref="MessageType"/> for the <see cref="MLogLevel"/>.
        /// </summary>
        public MessageType GetMessageType(MLogLevel logLevel)
            => logLevel switch
            {
                MLogLevel.Critical => Critical,
                MLogLevel.Error => Error,
                MLogLevel.Warning => Warning,
                MLogLevel.Information => Information,
                MLogLevel.Debug => Debug,
                MLogLevel.Trace => Trace,
            };
        
        
        /// <summary>
        /// Gets all of the <see cref="MessageType"/>s in this class as an array.
        /// </summary>
        public MessageType[] MessageTypesAsArray()
            => new []{ Critical, Error, Warning, Information, Debug, Trace };

        private static MessageType _createMessageType(string name, Log73Style style, bool writeToStdErr = false)
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