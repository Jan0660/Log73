using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing;

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
}
