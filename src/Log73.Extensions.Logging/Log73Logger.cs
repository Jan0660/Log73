﻿using System;
using Microsoft.Extensions.Logging;
using MLogLevel = Microsoft.Extensions.Logging.LogLevel;
using Console = Log73.Console;

namespace Log73.Extensions.Logging
{
    public class Log73Logger : ILogger
    {
        /// <summary>
        /// The configuration for this <see cref="Log73Logger"/>.
        /// </summary>
        public Log73LoggerConfiguration Config { get; set; }

        /// <summary>
        /// The name for this logger.
        /// </summary>
        public readonly string Name;

        public Log73Logger(string name, Log73LoggerConfiguration config)
            => (Name, Config) = (name, config);

        public void Log<TState>(MLogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;
            Console.Log(Config.GetMessageType(logLevel), formatter(state, exception),
                new LoggerLogInfoContext()
                {
                    EventId = eventId,
                    Logger = this
                });
        }

        /// <summary>
        /// If the message should be logged under the current configuration's <see cref="Log73.LogLevel"/>.
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(MLogLevel logLevel)
        {
            if (logLevel == MLogLevel.None)
                return false;
            if ((int)logLevel >= (int)Config.LogLevel)
                return true;
            return false;
        }

        /// <inheritdoc/>
        public IDisposable BeginScope<TState>(TState state)
            => default;
    }
}