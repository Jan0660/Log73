using System.Drawing;

namespace Log73.Extensions.Logging
{
    public class LoggerNameLogInfo: ILogInfo
    {
        public ConsoleStyleOption Style { get; set; } = new()
        {
            Color = Color.DarkCyan
        };
        public string GetValue(LogInfoContext context)
        {
            if (context is LoggerLogInfoContext ctx)
            {
                return ctx.Logger.Name;
            }

            return null;
        }
    }
}