using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace Log73
{
    /// <summary>
    /// <see cref="ILogInfo"/> for the current time. Default format is hh:mm:ss.
    /// </summary>
    public class TimeLogInfo : ILogInfo
    {
        public ConsoleStyleOption Style { get; set; } = new() { Color = Color.Gold };

        public string TimeFormat = "hh:mm:ss";

        public TimeLogInfo()
        {
        }

        public TimeLogInfo(string timeFormat) => TimeFormat = timeFormat;

        public string GetValue(LogInfoContext context)
            => $"{DateTime.Now.ToString(TimeFormat)}";
    }

    /// <summary>
    /// <see cref="ILogInfo"/> for the name of the calling method.
    /// </summary>
    public class CallingMethodLogInfo : ILogInfo
    {
        public ConsoleStyleOption Style { get; set; } = new() { Color = Color.HotPink };
        public bool FullName = true;
        public string UnableToGet = "Unable to get method";

        public string GetValue(LogInfoContext context)
        {
            try
            {
                var frame = GetCallerFrame();
                return FullName
                    ? frame.GetMethod().DeclaringType.FullName + "." + frame.GetMethod().Name
                    : frame.GetMethod().Name;
            }
            catch
            {
                return UnableToGet;
            }
        }

        /// <summary>
        /// The method used to get the caller <see cref="StackFrame"/> in the <see cref="CallingMethodLogInfo"/>, <see cref="CallingClassLogInfo"/> and <see cref="CallingModuleLogInfo"/> <see cref="ILogInfo"/>s.
        /// </summary>
        /// <exception cref="T:System.Exception">If fails to get the caller <see cref="StackFrame"/> an exception is thrown.</exception>
        public static StackFrame GetCallerFrame()
        {
            var log73 = typeof(Console).Assembly;
            foreach (var frame in new StackTrace().GetFrames())
            {
                if (frame.GetMethod().DeclaringType?.Assembly != log73)
                {
                    if (frame.GetMethod().Module.Name == "System.Private.CoreLib.dll")
                        continue;
                    if (frame.GetMethod().Name == "MoveNext")
                        continue;
                    return frame;
                }
            }

            throw new Exception("failed to get caller frame");
        }
    }

    /// <summary>
    /// <see cref="ILogInfo"/> for the name of the calling method's class.
    /// </summary>
    public class CallingClassLogInfo : ILogInfo
    {
        public ConsoleStyleOption Style { get; set; } = new() { Color = Color.HotPink };
        public bool FullName = true;
        public string UnableToGet = "Unable to get class";

        public string GetValue(LogInfoContext context)
        {
            try
            {
                var frame = CallingMethodLogInfo.GetCallerFrame();
                return FullName ? frame.GetMethod().DeclaringType.FullName : frame.GetMethod().DeclaringType.Name;
            }
            catch
            {
                return UnableToGet;
            }
        }
    }

    /// <summary>
    /// <see cref="ILogInfo"/> for the name of the calling method's module.
    /// </summary>
    public class CallingModuleLogInfo : ILogInfo
    {
        public ConsoleStyleOption Style { get; set; } = new() { Color = Color.HotPink };
        public bool FullName = false;
        public string UnableToGet = "Unable to get module";

        public string GetValue(LogInfoContext context)
        {
            try
            {
                var frame = CallingMethodLogInfo.GetCallerFrame();
                return FullName ? frame.GetMethod().Module.FullyQualifiedName : frame.GetMethod().Module.Name;
            }
            catch
            {
                return UnableToGet;
            }
        }
    }

    /// <summary>
    /// <see cref="ILogInfo"/> for the name of the current <see cref="Thread"/>.
    /// </summary>
    public class ThreadLogInfo : ILogInfo
    {
        public ConsoleStyleOption Style { get; set; } = new() { Color = Color.HotPink };
        public bool Attributes = true;

        public string GetValue(LogInfoContext context)
        {
            var thread = Thread.CurrentThread;
            var str = thread.Name ?? "Unnamed";
            if (Attributes)
            {
                str += "\\";
                if (thread.IsBackground) str += 'B';
                if (thread.IsThreadPoolThread) str += 'P';
            }

            return str;
        }
    }

    /// <summary>
    /// <see cref="ILogInfo"/> for the logged object's type.
    /// </summary>
    public class TypeLogInfo : ILogInfo
    {
        public ConsoleStyleOption Style { get; set; } = new() { Color = Color.HotPink };

        /// <summary>
        /// For example: If true, returns "Log73.TypeLogInfo". If false, returns "TypeLogInfo"
        /// </summary>
        public bool FullName = true;

        public string GetValue(LogInfoContext context)
        {
            var type = context.Value.GetType();
            return FullName ? type.FullName : type.Name;
        }
    }
}