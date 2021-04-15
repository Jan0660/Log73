using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Log73.Extensions.Logging
{
    public sealed class Log73LoggerProvider : ILoggerProvider
    {
        private readonly IDisposable _onChangeToken;
        private Log73LoggerConfiguration _currentConfig;
        private readonly ConcurrentDictionary<string, Log73Logger> _loggers = new();

        public Log73LoggerProvider(
            IOptionsMonitor<Log73LoggerConfiguration> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new Log73Logger(name, _currentConfig));

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken.Dispose();
        }
    }
}