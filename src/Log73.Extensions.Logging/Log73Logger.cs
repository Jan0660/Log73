using System;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.Logging;
using MLogLevel = Microsoft.Extensions.Logging.LogLevel;
using Console = Log73.Console;

namespace Log73.Extensions.Logging
{
    public class Log73Logger : ILogger
    {
        private readonly Log73LoggerConfiguration _config;
        public readonly string Name;

        public Log73Logger(string name, Log73LoggerConfiguration config)
            => (Name, _config) = (name, config);

        public void Log<TState>(MLogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;
            Console.Log(_config.GetMessageType(logLevel), formatter(state, exception),
                new LoggerLogInfoContext()
                {
                    EventId = eventId,
                    Logger = this
                });
        }

        public bool IsEnabled(MLogLevel logLevel)
        {
            if (logLevel == MLogLevel.None)
                return false;
            if ((int) logLevel >= (int) _config.LogLevel)
                return true;
            return false;
        }

        public IDisposable BeginScope<TState>(TState state)
            => default;
    }
}