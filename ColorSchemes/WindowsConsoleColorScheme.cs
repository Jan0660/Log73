using System.Drawing;

namespace Log73.ColorSchemes
{
    public class WindowsConsoleColorScheme : IColorScheme
    {
        public Color Black { get; set; } = Color.FromArgb(0,0,0);
        public Color Red { get; set; } = Color.FromArgb(128, 0, 0);
        public Color Green { get; set; } = Color.FromArgb(0, 128, 0);
        public Color Yellow { get; set; } = Color.FromArgb(128, 128, 0);
        public Color Blue { get; set; } = Color.FromArgb(0, 0, 128);
        public Color Magenta { get; set; } = Color.FromArgb(128, 0, 128);
        public Color Cyan { get; set; } = Color.FromArgb(0, 128, 128);
        public Color White { get; set; } = Color.FromArgb(192, 192, 192);
        public Color BrightBlack { get; set; } = Color.FromArgb(128, 128, 128);
        public Color BrightRed { get; set; } = Color.FromArgb(255, 0, 0);
        public Color BrightGreen { get; set; } = Color.FromArgb(0, 255, 0);
        public Color BrightYellow { get; set; } = Color.FromArgb(255, 255, 0);
        public Color BrightBlue { get; set; } = Color.FromArgb(0, 0, 255);
        public Color BrightMagenta { get; set; } = Color.FromArgb(255, 0, 255);
        public Color BrightCyan { get; set; } = Color.FromArgb(0, 255, 255);
        public Color BrightWhite { get; set; } = Color.FromArgb(255, 255, 255);
    }
}