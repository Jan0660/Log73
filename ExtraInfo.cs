using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;

namespace Log73
{
    public interface IExtraInfo
    {
        public ConsoleStyleOption Style { get; set; }
        public string GetValue(ExtraInfoContext context);
    }

    public class ExtraInfoContext
    {
        public object Value { get; internal set; }
        public MessageType MessageType { get; internal set; }

        internal ExtraInfoContext() { }
    }

    public class TimeExtraInfo : IExtraInfo
    {
        public ConsoleStyleOption Style { get; set; } = new() { Color = Color.Magenta };

        public string GetValue(ExtraInfoContext context)
            => $"{DateTime.Now:hh:mm:ss}";
    }
    public class CallingMethodExtraInfo : IExtraInfo
    {
        public ConsoleStyleOption Style { get; set; } = new() { Color = Color.HotPink };
        public bool FullName = true;

        public string GetValue(ExtraInfoContext context)
        {
            var frame = GetCallerFrame();
            return FullName ? frame.GetMethod().DeclaringType.FullName + "." + frame.GetMethod().Name : frame.GetMethod().Name;
        }

        public static StackFrame GetCallerFrame()
        {
            var h = new StackTrace();
            var frames = h.GetFrames();
            foreach (var frame in frames)
            {
                if (frame.GetMethod().DeclaringType.Assembly != Assembly.GetExecutingAssembly())
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
    public class CallingClassExtraInfo : IExtraInfo
    {
        public ConsoleStyleOption Style { get; set; } = new() { Color = Color.HotPink };
        public bool FullName = true;

        public string GetValue(ExtraInfoContext context)
        {
            var frame = CallingMethodExtraInfo.GetCallerFrame();
            return FullName ? frame.GetMethod().DeclaringType.FullName : frame.GetMethod().DeclaringType.Name;
        }
    }
    public class CallingModuleExtraInfo : IExtraInfo
    {
        public ConsoleStyleOption Style { get; set; } = new() { Color = Color.HotPink };
        public bool FullName = false;

        public string GetValue(ExtraInfoContext context)
        {
            var frame = CallingMethodExtraInfo.GetCallerFrame();
            return FullName ? frame.GetMethod().Module.FullyQualifiedName : frame.GetMethod().Module.Name;
        }
    }
    public class ThreadExtraInfo : IExtraInfo
    {
        public ConsoleStyleOption Style { get; set; } = new() { Color = Color.HotPink };
        public bool Attributes = true;

        public string GetValue(ExtraInfoContext context)
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
    public class TypeExtraInfo : IExtraInfo
    {
        public ConsoleStyleOption Style { get; set; } = new() { Color = Color.HotPink };
        public bool FullName = true;

        public string GetValue(ExtraInfoContext context)
        {
            var type = context.Value.GetType();
            return FullName ? type.FullName : type.Name;
        }
    }
}
