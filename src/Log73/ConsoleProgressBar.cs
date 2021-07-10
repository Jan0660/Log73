using System.Text;

namespace Log73
{
    /// <summary>
    /// Provides progress bars in Log73.
    /// </summary>
    public static class ConsoleProgressBar
    {
        public static char FilledCharacter = '#';
        public static char EmptyCharacter = ' ';

        /// <param name="current">Must be less than or equal to <paramref name="max"/></param>
        /// <param name="max"></param>
        public static void Update(int current, int max)
        {
            int width = System.Console.WindowWidth - 1;
            // how much we expect the braces and counts to take up
            var bloatSize = 5 + (max.ToString().Length * 2);
            var availableForBlocks = width - bloatSize;
            var grab = (int) (current / (double) max * availableForBlocks);
            // get blocks
            var blocks = new StringBuilder();
            blocks.Append(RepeatChar(FilledCharacter, grab));
            blocks.Append(RepeatChar(EmptyCharacter, availableForBlocks - grab));
            var currentCountPadding = RepeatChar(' ', max.ToString().Length - current.ToString().Length);
            Console.AtBottomLog("[" + currentCountPadding + current + "/" + max + "]" + "[" + blocks + "]", true);
        }

        private static string RepeatChar(char ch, int times)
        {
            var builder = new StringBuilder(times);
            for (int i = 0; i < times; i++)
                builder.Append(ch);
            return builder.ToString();
        }
    }
}