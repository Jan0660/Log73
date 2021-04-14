using System.Drawing;

namespace Log73.ColorSchemes
{
    public class RiderDarkMelonColorScheme : IColorScheme
    {
        public Color Black { get; set; } = Color.FromArgb(189,189,189);
        public Color Red { get; set; } = Color.FromArgb(255, 86, 71);
        public Color Green { get; set; } = Color.FromArgb(133, 196, 108);
        public Color Yellow { get; set; } = Color.FromArgb(217, 183, 43);
        public Color Blue { get; set; } = Color.FromArgb(108, 149, 235);
        public Color Magenta { get; set; } = Color.FromArgb(214, 136, 212);
        public Color Cyan { get; set; } = Color.FromArgb(57, 204, 143);
        public Color White { get; set; } = Color.FromArgb(120, 120, 120);
        public Color BrightBlack { get; set; } = Color.FromArgb(79, 79, 79);
        public Color BrightRed { get; set; } = Color.FromArgb(255, 136, 112);
        public Color BrightGreen { get; set; } = Color.FromArgb(173, 235, 150);
        public Color BrightYellow { get; set; } = Color.FromArgb(245, 216, 106);
        public Color BrightBlue { get; set; } = Color.FromArgb(173, 211, 255);
        public Color BrightMagenta { get; set; } = Color.FromArgb(255, 191, 254);
        public Color BrightCyan { get; set; } = Color.FromArgb(125, 240, 192);
        public Color BrightWhite { get; set; } = Color.FromArgb(104, 104, 104);
    }
}