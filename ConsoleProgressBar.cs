﻿using System.Text;

namespace Log73
{
    public static class ConsoleProgressBar
    {
        public static char FilledCharacter = '#';
        public static char EmptyCharacter = ' ';

        public static void Update(int current, int max)
        {
            int width = System.Console.WindowWidth - 1;
            // how much we expect the braces and counts to take up
            var bloatSize = 5 + (max.ToString().Length * 2);
            var availableForBlocks = width - bloatSize;
            //var grab = pain(current, max, availableForBlocks);
            var grab = (int) (current / (double) max * availableForBlocks);
            // get blocks
            var blocks = "";
            blocks += RepeatChar(FilledCharacter, grab);
            blocks += RepeatChar(EmptyCharacter, availableForBlocks - grab);
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