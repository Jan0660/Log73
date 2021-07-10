using System;
using System.IO;
using System.Text;
using SConsole = System.Console;

namespace Log73
{
    public static partial class Console
    {
        
        /// <inheritdoc cref="System.Console.Title"/>
        public static string Title
        {
            get => SConsole.Title;
            set => SConsole.Title = value;
        }

        /// <inheritdoc cref="System.Console.BufferHeight"/>
        public static int BufferHeight
        {
            get => SConsole.BufferHeight;
            set => SConsole.BufferHeight = value;
        }

        /// <inheritdoc cref="System.Console.BufferWidth"/>
        public static int BufferWidth
        {
            get => SConsole.BufferWidth;
            set => SConsole.BufferWidth = value;
        }

        /// <inheritdoc cref="System.Console.CapsLock"/>
        public static bool CapsLock => SConsole.CapsLock;

        /// <inheritdoc cref="System.Console.CursorLeft"/>
        public static int CursorLeft
        {
            get => SConsole.CursorLeft;
            set => SConsole.CursorLeft = value;
        }

        /// <inheritdoc cref="System.Console.CursorSize"/>
        public static int CursorSize
        {
            get => SConsole.CursorSize;
            set => SConsole.CursorSize = value;
        }

        /// <inheritdoc cref="System.Console.CursorTop"/>
        public static int CursorTop
        {
            get => SConsole.CursorTop;
            set => SConsole.CursorTop = value;
        }

        /// <inheritdoc cref="System.Console.CursorVisible"/>
        public static bool CursorVisible
        {
            get => SConsole.CursorVisible;
            set => SConsole.CursorVisible = value;
        }

        /// <inheritdoc cref="System.Console.InputEncoding"/>
        public static Encoding InputEncoding
        {
            get => SConsole.InputEncoding;
            set => SConsole.InputEncoding = value;
        }

        /// <inheritdoc cref="System.Console.KeyAvailable"/>
        public static bool KeyAvailable => SConsole.KeyAvailable;

        /// <inheritdoc cref="System.Console.NumberLock"/>
        public static bool NumberLock => SConsole.NumberLock;

        /// <inheritdoc cref="System.Console.OutputEncoding"/>
        public static Encoding OutputEncoding
        {
            get => SConsole.OutputEncoding;
            set => SConsole.OutputEncoding = value;
        }

        /// <inheritdoc cref="System.Console.WindowHeight"/>
        public static int WindowHeight
        {
            get => SConsole.WindowHeight;
            set => SConsole.WindowHeight = value;
        }

        /// <inheritdoc cref="System.Console.WindowLeft"/>
        public static int WindowLeft
        {
            get => SConsole.WindowLeft;
            set => SConsole.WindowLeft = value;
        }

        /// <inheritdoc cref="System.Console.WindowTop"/>
        public static int WindowTop
        {
            get => SConsole.WindowTop;
            set => SConsole.WindowTop = value;
        }

        /// <inheritdoc cref="System.Console.WindowWidth"/>
        public static int WindowWidth
        {
            get => SConsole.WindowWidth;
            set => SConsole.WindowWidth = value;
        }

        /// <inheritdoc cref="System.Console.IsErrorRedirected"/>
        public static bool IsErrorRedirected => SConsole.IsErrorRedirected;

        /// <inheritdoc cref="System.Console.IsInputRedirected"/>
        public static bool IsInputRedirected => SConsole.IsInputRedirected;

        /// <inheritdoc cref="System.Console.IsOutputRedirected"/>
        public static bool IsOutputRedirected => SConsole.IsOutputRedirected;

        /// <inheritdoc cref="System.Console.LargestWindowHeight"/>
        public static int LargerWindowHeight => SConsole.LargestWindowHeight;

        /// <inheritdoc cref="System.Console.LargestWindowWidth"/>
        public static int LargerWindowWidth => SConsole.LargestWindowWidth;

        /// <inheritdoc cref="System.Console.TreatControlCAsInput"/>
        public static bool TreatControlCAsInput
        {
            get => SConsole.TreatControlCAsInput;
            set => SConsole.TreatControlCAsInput = value;
        }

        /// <inheritdoc cref="System.Console.CancelKeyPress"/>
        public static event ConsoleCancelEventHandler? CancelKeyPress
        {
            add => SConsole.CancelKeyPress += value;
            remove => SConsole.CancelKeyPress -= value;
        }
        
        

        /// <inheritdoc cref="System.Console.Beep"/>
        public static void Beep()
            => SConsole.Beep();

        /// <inheritdoc cref="System.Console.Beep(int, int)"/>
        public static void Beep(int frequency, int duration)
            => SConsole.Beep(frequency, duration);

        /// <inheritdoc cref="System.Console.Clear"/>
        public static void Clear()
            => SConsole.Clear();

        /// <inheritdoc cref="System.Console.SetWindowSize(int, int)"/>
        public static void SetWindowSize(int width, int height)
            => SConsole.SetWindowSize(width, height);

        /// <inheritdoc cref="System.Console.SetCursorPosition(int, int)"/>
        public static void SetCursorPosition(int left, int top)
            => SConsole.SetCursorPosition(left, top);

        /// <inheritdoc cref="System.Console.SetBufferSize(int, int)"/>
        public static void SetBufferSize(int width, int height)
            => SConsole.SetBufferSize(width, height);

        /// <inheritdoc cref="System.Console.ResetColor"/>
        public static void ResetColor()
            => SConsole.ResetColor();

        /// <inheritdoc cref="TextReader.Read"/>
        public static int Read()
            => In.Read();

        /// <inheritdoc cref="TextReader.ReadLine"/>
        public static string ReadLine()
            => In.ReadLine();

        public static (int Left, int Top) GetCursorPosition()
            => (SConsole.CursorLeft, SConsole.CursorTop);

        /// <inheritdoc cref="System.Console.SetIn"/>
        public static void SetIn(TextReader textReader)
        {
            In = textReader ?? throw new ArgumentNullException(nameof(textReader));
        }

        /// <inheritdoc cref="System.Console.SetOut"/>
        public static void SetOut(TextWriter textWriter)
        {
            Out = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
        }

        /// <inheritdoc cref="System.Console.SetError"/>
        public static void SetError(TextWriter textWriter)
        {
            Err = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
        }
    }
}