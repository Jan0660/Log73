using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;

namespace Log73
{
    public class TimeLogInfo : ILogInfo
    {
        public ConsoleStyleOption Style { get; set; } = new() {Color = Color.Magenta};

        public string TimeFormat = "hh:mm:ss";

        public TimeLogInfo()
        {
        }

        public TimeLogInfo(string timeFormat)
        {
            TimeFormat = timeFormat;
        }

        public string GetValue(LogInfoContext context)
            => $"{DateTime.Now.ToString(TimeFormat)}";
    }

    public class CallingMethodLogInfo : ILogInfo
    {
        public ConsoleStyleOption Style { get; set; } = new() {Color = Color.HotPink};
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

        public static StackFrame GetCallerFrame()
        {
            var h = new StackTrace();
            var frames = h.GetFrames();
            var log73 = typeof(Console).Assembly;
            foreach (var frame in frames)
            {
                if (frame.GetMethod().DeclaringType.Assembly != log73)
                {
                    var ass = frame.GetMethod().DeclaringType.Assembly;
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

    public class CallingClassLogInfo : ILogInfo
    {
        public ConsoleStyleOption Style { get; set; } = new() {Color = Color.HotPink};
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

    public class CallingModuleLogInfo : ILogInfo
    {
        public ConsoleStyleOption Style { get; set; } = new() {Color = Color.HotPink};
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

    public class ThreadLogInfo : ILogInfo
    {
        public ConsoleStyleOption Style { get; set; } = new() {Color = Color.HotPink};
        public bool Attributes = true;

        public string GetValue(LogInfoContext context)
        {
            var thread = Thread.CurrentThread;
            var str = thread.Name == null ? "Unnamed" : thread.Name;
            if (Attributes)
            {
                str += "\\";
                if (thread.IsBackground) str += 'B';
                if (thread.IsThreadPoolThread) str += 'P';
            }

            return str;
        }
    }

    public class TypeLogInfo : ILogInfo
    {
        public ConsoleStyleOption Style { get; set; } = new() {Color = Color.HotPink};
        public bool FullName = true;

        public string GetValue(LogInfoContext context)
        {
            var type = context.Value.GetType();
            return FullName ? type.FullName : type.Name;
        }
    }
}