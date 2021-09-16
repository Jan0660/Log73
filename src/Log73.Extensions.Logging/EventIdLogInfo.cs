using System.Drawing;

namespace Log73.Extensions.Logging
{
    /// <summary>
    /// Provides the Name in a <see cref="Microsoft.Extensions.Logging.EventId"/>. Accepts <see cref="LoggerLogInfoContext"/>.
    /// </summary>
    public class EventIdLogInfo : ILogInfo
    {
        /// <summary>
        /// The style to apply to the string returned by <see cref="GetValue"/> and the brackets.
        /// </summary>
        public Log73Style Style { get; set; } = new()
        {
            ForegroundColor = Color.Blue
        };
        /// <summary>
        /// Get the string for the LogInfo.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
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