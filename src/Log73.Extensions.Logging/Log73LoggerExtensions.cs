using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace Log73.Extensions.Logging
{
    /// <summary>
    /// Extensions for <see cref="ILoggingBuilder"/>.
    /// </summary>
    public static class Log73LoggerExtensions
    {
        public static ILoggingBuilder AddLog73Logger(
            this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, Log73LoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions
                <Log73LoggerConfiguration, Log73LoggerProvider>(builder.Services);

            return builder;
        }

        public static ILoggingBuilder AddLog73Logger(
            this ILoggingBuilder builder,
            Action<Log73LoggerConfiguration> configure)
        {
            builder.AddLog73Logger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}