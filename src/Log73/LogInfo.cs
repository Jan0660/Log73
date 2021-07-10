namespace Log73
{
    public interface ILogInfo
    {
        public ConsoleStyleOption Style { get; set; }
        public string GetValue(LogInfoContext context);
    }

    /// <summary>
    /// Class to store information given to <see cref="ILogInfo.GetValue(LogInfoContext)"/> when getting an <see cref="ILogInfo"/>'s value to log.
    /// </summary>
    public class LogInfoContext
    {
        public object Value { get; internal set; }
        
        public string ValueString { get; internal set; }
        public MessageType MessageType { get; internal set; }
    }
}
