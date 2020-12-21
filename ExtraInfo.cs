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
        public string GetValue();
    }

    public class TimeExtraInfo : IExtraInfo
    {
        public ConsoleStyleOption Style { get; set; } = new() { Color = Color.Magenta};

        public string GetValue()
            => $"{DateTime.Now:hh:mm:ss}";
    }
}
