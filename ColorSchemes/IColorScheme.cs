using System.Drawing;

namespace Log73.ColorSchemes
{
    public interface IColorScheme
    {
        public Color Black { get; set; }
        public Color Red { get; set; }
        public Color Green { get; set; }
        public Color Yellow { get; set; }
        public Color Blue { get; set; }
        public Color Magenta { get; set; }
        public Color Cyan { get; set; }
        public Color White { get; set; }
        public Color BrightBlack { get; set; }
        public Color BrightRed { get; set; }
        public Color BrightGreen { get; set; }
        public Color BrightYellow { get; set; }
        public Color BrightBlue { get; set; }
        public Color BrightMagenta { get; set; }
        public Color BrightCyan { get; set; }
        public Color BrightWhite { get; set; }
    }
}