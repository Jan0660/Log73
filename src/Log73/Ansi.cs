using System;
using System.Drawing;
using System.Numerics;
using Log73.ColorSchemes;

namespace Log73
{
    /// <summary>
    /// Class to store the Ansi codes required for styling, all of the properties return <see cref="string.Empty"/> if <see cref="ConsoleOptions.UseAnsi"/> is <see langword="false"/>.
    /// Mostly derived from https://en.wikipedia.org/wiki/ANSI_escape_code.
    /// </summary>
    public static class Ansi
    {
        private static bool Enabled => Console.Options.UseAnsi;

        /// <summary>
        /// Disable all ANSI effects.
        /// </summary>
        public static string Normal => Enabled ? "\u001b[0m" : "";

        public static string Bold => Enabled ? "\u001b[1m" : "";
        public static string BoldOff => Enabled ? "\u001b[21m" : "";
        public static string NormalColor => Enabled ? "\u001b[22m" : "";
        public static string Faint => Enabled ? "\u001b[2m" : "";
        public static string Underline => Enabled ? "\u001b[4m" : "";
        public static string UnderlineOff => Enabled ? "\u001b[24m" : "";
        public static string Italic => Enabled ? "\u001b[3m" : "";
        public static string NotItalic => Enabled ? "\u001b[23m" : "";
        public static string SlowBlink => Enabled ? "\u001b[5m" : "";
        public static string RapidBlink => Enabled ? "\u001b[6m" : "";
        public static string BlinkOff => Enabled ? "\u001b[25m" : "";
        public static string Invert => Enabled ? "\u001b[7m" : "";
        public static string InvertOff => Enabled ? "\u001b[27m" : "";
        public static string CrossedOut => Enabled ? "\u001b[9m" : "";
        public static string CrossedOutOff => Enabled ? "\u001b[9m" : "";
        public static string DefaultForegroundColor => Enabled ? "\u001b[39m" : "";
        public static string DefaultBackgroundColor => Enabled ? "\u001b[49m" : "";

        public static string ForegroundColor(string str, Color color)
            => $"\x1b[38;2;{color.R};{color.G};{color.B}m{str}{DefaultForegroundColor}";

        public static string BackgroundColor(string str, Color color)
            => $"\x1b[48;2;{color.R};{color.G};{color.B}m{str}{DefaultBackgroundColor}";

        public static string ApplyColor(string str, Color? foregroundColor, Color? backgroundColor,
            bool enable24BitColor)
        {
            if (enable24BitColor)
            {
                if (foregroundColor != null)
                    str = ForegroundColor(str, foregroundColor.Value);
                if (backgroundColor != null)
                    str = BackgroundColor(str, backgroundColor.Value);
            }

            return str;
        }

        public static Color BestMatch(Color input, IColorScheme colorScheme)
        {
            Color[] colors = new[]
            {
                colorScheme.Black,
                colorScheme.Blue,
                colorScheme.Cyan,
                colorScheme.Green,
                colorScheme.Magenta,
                colorScheme.White,
                colorScheme.Red,
                colorScheme.Yellow,
                colorScheme.BrightBlack,
                colorScheme.BrightBlue,
                colorScheme.BrightCyan,
                colorScheme.BrightGreen,
                colorScheme.BrightMagenta,
                colorScheme.BrightWhite,
                colorScheme.BrightRed,
                colorScheme.BrightYellow
            };
            byte a = input.A;
            byte r = input.R;
            byte g = input.G;
            byte b = input.B;
            Vector4 aways;
            int bestAway = 2949000;
            Color bestMatch = Color.Black;
            foreach (Color color in colors)
            {
                aways = new Vector4(a - color.A, r - color.R, g - color.G, b - color.B);
                if (aways.X < 0)
                    aways.X *= -1;
                if (aways.Y < 0)
                    aways.Y *= -1;
                if (aways.Z < 0)
                    aways.Z *= -1;
                if (aways.W < 0)
                    aways.W *= -1;
                int awaysSum = (int)(aways.X + aways.Y + aways.Z + aways.W);
                if (awaysSum < bestAway)
                {
                    bestAway = awaysSum;
                    bestMatch = color;
                }
            }

            return bestMatch;
        }

        public static ConsoleColor ColorToConsoleColor(Color color, IColorScheme colorScheme)
        {
            if (color == colorScheme.Black)
                return ConsoleColor.Black;
            if (color == colorScheme.Blue)
                return ConsoleColor.DarkBlue;
            if (color == colorScheme.Cyan)
                return ConsoleColor.DarkCyan;
            if (color == colorScheme.Green)
                return ConsoleColor.DarkGreen;
            if (color == colorScheme.Magenta)
                return ConsoleColor.DarkMagenta;
            if (color == colorScheme.Red)
                return ConsoleColor.DarkRed;
            if (color == colorScheme.White)
                return ConsoleColor.DarkGray;
            if (color == colorScheme.Yellow)
                return ConsoleColor.DarkYellow;
            //
            if (color == colorScheme.BrightBlack)
                return ConsoleColor.Gray;
            if (color == colorScheme.BrightBlue)
                return ConsoleColor.Blue;
            if (color == colorScheme.BrightCyan)
                return ConsoleColor.Cyan;
            if (color == colorScheme.BrightGreen)
                return ConsoleColor.Green;
            if (color == colorScheme.BrightMagenta)
                return ConsoleColor.Magenta;
            if (color == colorScheme.BrightRed)
                return ConsoleColor.Red;
            if (color == colorScheme.BrightWhite)
                return ConsoleColor.White;
            if (color == colorScheme.BrightYellow)
                return ConsoleColor.Yellow;
            throw new Exception("invalid color");
        }
    }
}