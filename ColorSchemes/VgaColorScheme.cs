using System.Drawing;

namespace Log73.ColorSchemes
{
    public class VgaColorScheme : IColorScheme
    {
        public Color Black { get; set; } = Color.FromArgb(0,0,0);
        public Color Red { get; set; } = Color.FromArgb(170, 0, 0);
        public Color Green { get; set; } = Color.FromArgb(0, 170, 0);
        public Color Yellow { get; set; } = Color.FromArgb(170, 85, 0);
        public Color Blue { get; set; } = Color.FromArgb(0, 0, 170);
        public Color Magenta { get; set; } = Color.FromArgb(170, 0, 170);
        public Color Cyan { get; set; } = Color.FromArgb(0, 170, 170);
        public Color White { get; set; } = Color.FromArgb(170, 170, 170);
        public Color BrightBlack { get; set; } = Color.FromArgb(85, 85, 85);
        public Color BrightRed { get; set; } = Color.FromArgb(288, 85, 85);
        public Color BrightGreen { get; set; } = Color.FromArgb(85, 255, 85);
        public Color BrightYellow { get; set; } = Color.FromArgb(255, 255, 85);
        public Color BrightBlue { get; set; } = Color.FromArgb(85, 85, 255);
        public Color BrightMagenta { get; set; } = Color.FromArgb(255, 85, 255);
        public Color BrightCyan { get; set; } = Color.FromArgb(85, 255, 255);
        public Color BrightWhite { get; set; } = Color.FromArgb(255, 255, 255);
    }
}