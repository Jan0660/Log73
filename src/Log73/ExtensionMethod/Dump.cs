using System.Threading.Tasks;

namespace Log73.ExtensionMethod
{
    public static class DumpExtensionMethods
    {
        /// <summary>
        /// Logs the object to console using the <see cref="LogType"/> specified in <see cref="ConsoleOptions.DumpLogType"/>.
        /// </summary>
        public static void Dump(this object obj)
            => Console.Log(Console.Options.DumpMessageType, obj);
        /// <summary>
        /// Logs the object to console using the <see cref="MessageTypes.Info"/> <see cref="MessageType"/>.
        /// </summary>
        public static void DumpInfo(this object obj)
            => Console.Log(MessageTypes.Info, obj);
        /// <summary>
        /// Logs the object to console using the <see cref="MessageTypes.Warn"/> <see cref="MessageType"/>.
        /// </summary>
        public static void DumpWarn(this object obj)
            => Console.Log(MessageTypes.Warn, obj);
        /// <summary>
        /// Logs the object to console using the <see cref="MessageTypes.Error"/> <see cref="MessageType"/>.
        /// </summary>
        public static void DumpError(this object obj)
            => Console.Log(MessageTypes.Error, obj);
        /// <summary>
        /// Logs the object to console using the <see cref="MessageTypes.Debug"/> <see cref="MessageType"/>.
        /// </summary>
        public static void DumpDebug(this object obj)
            => Console.Log(MessageTypes.Debug, obj);
        /// <inheritdoc cref="Log73.Console.Task(string, Task)"/>
        public static void DumpTask(this Task task, string name)
            => Console.Task(name, task);
    }
}
