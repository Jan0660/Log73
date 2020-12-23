using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Log73
{
    // https://en.wikipedia.org/wiki/ANSI_escape_code
    public static class Ansi
    {
        private static bool en => Console.Options.UseAnsi;
        public static string normal => en ? "\u001b[0m" : "";
        public static string Bold => en ? "\u001b[1m" : "";
        public static string BoldOff => en ? "\u001b[21m" : "";
        public static string NormalColor => en ? "\u001b[22m" : "";
        public static string Faint => en ? "\u001b[2m" : "";
        public static string Underline => en ? "\u001b[4m" : "";
        public static string UnderlineOff => en ? "\u001b[24m" : "";
        public static string Italic => en ? "\u001b[3m" : "";
        public static string NotItalic => en ? "\u001b[23m" : "";
        public static string SlowBlink => en ? "\u001b[5m" : "";
        public static string RapidBlink => en ? "\u001b[6m" : "";
        public static string BlinkOff => en ? "\u001b[25m" : "";
        public static string Invert => en ? "\u001b[7m" : "";
        public static string InvertOff => en ? "\u001b[27m" : "";
        public static string CrossedOut => en ? "\u001b[9m" : "";
        public static string CrossedOutOff => en ? "\u001b[9m" : "";
        public static string DefaultForegroundColor => en ? "\u001b[39m" : "";
        public static string DefaultBackgroundColor => en ? "\u001b[49m" : "";

        public static string ForegroundColor(string str, Color color)
            => $"\x1b[38;2;{color.R};{color.G};{color.B}m{str}{DefaultForegroundColor}";

        public static string BackgroundColor(string str, Color color)
            => $"\x1b[48;2;{color.R};{color.G};{color.B}m{str}{DefaultBackgroundColor}";

        public static string ApplyColor(string str, Color? foregroundColor, Color? backgroundColor)
        {
            if (foregroundColor != null)
                str = ForegroundColor(str, foregroundColor.Value);
            if (backgroundColor != null)
                str = BackgroundColor(str, backgroundColor.Value);
            return str;
        }
    }
}