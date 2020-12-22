using System;
using System.Collections.Generic;
using System.Text;

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

        internal static bool IsValueType(this object obj)
        {
            return obj != null && obj.GetType().IsValueType;
        }
    }
}
