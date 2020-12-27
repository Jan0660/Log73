

namespace Log73
{
    public interface ILogInfo
    {
        public ConsoleStyleOption Style { get; set; }
        public string GetValue(LogInfoContext context);
    }

    public class LogInfoContext
    {
        public object Value { get; internal set; }
        public MessageType MessageType { get; internal set; }

        internal LogInfoContext() { }
    }
}
