using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Log73.ExtensionMethod
{
    public static class DumpExtensionMethods
    {
        /// <summary>
        /// Logs the object to console
        /// </summary>
        /// <param name="obj"></param>
        public static void Dump(this object obj)
            => Console.Log(Console.Options.DumpLogType, obj);
        public static void DumpInfo(this object obj)
            => Console.Log(MessageTypes.Info, obj);
        public static void DumpWarn(this object obj)
            => Console.Log(MessageTypes.Warn, obj);
        public static void DumpError(this object obj)
            => Console.Log(MessageTypes.Error, obj);
        public static void DumpDebug(this object obj)
            => Console.Log(MessageTypes.Debug, obj);
        public static void DumpTask(this Task task, string name)
            => Console.Task(name, task);

        internal static bool IsValueType(this object obj)
        {
            return obj != null && obj.GetType().IsValueType;
        }
    }
}
