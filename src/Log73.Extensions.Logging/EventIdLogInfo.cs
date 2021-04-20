using System.Drawing;

namespace Log73.Extensions.Logging
{
    /// <summary>
    /// Provides the Name in a <see cref="Microsoft.Extensions.Logging.EventId"/>. Accepts <see cref="LoggerLogInfoContext"/>.
    /// </summary>
    public class EventIdLogInfo : ILogInfo
    {
        public ConsoleStyleOption Style { get; set; } = new()
        {
            Color = Color.Blue
        };
        public string GetValue(LogInfoContext context)
        {
            if (context is LoggerLogInfoContext ctx)
            {
                return ctx.EventId.Name;
            }

            return null;
        }
    }
}