using Microsoft.Extensions.Logging;

namespace Log73.Extensions.Logging
{
    public class LoggerLogInfoContext : LogInfoContext
    {
        public EventId EventId;
        public Log73Logger Logger;
    }
}