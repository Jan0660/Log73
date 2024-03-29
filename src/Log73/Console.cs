﻿using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Log73.Extensible;
using SConsole = System.Console;

namespace Log73
{
    [Flags]
    public enum LogType
    {
        Info = 1,
        Warn = 2,
        Error = 4,
        Debug = 8
    }

    /// <summary>
    /// The Log73 console.
    /// </summary>
    public static partial class Console
    {
        private static bool _lock = false;

        private static BlockingCollection<(MessageType msgType, object value, LogInfoContext context)>
            _logQueue = new();

        /// <summary>
        /// Set to <see cref="System.Console.Out"/> by default.
        /// </summary>
        public static TextWriter Out { get; set; } = SConsole.Out;
        /// <summary>
        /// Set to <see cref="System.Console.Error"/> by default.
        /// </summary>
        public static TextWriter Err { get; set; } = SConsole.Error;
        /// <summary>
        /// Set to <see cref="System.Console.In"/> by default.
        /// </summary>
        public static TextReader In { get; set; } = SConsole.In;
        private static string _lastKeepMessage = null;
        public static Log73Options Options = new Log73Options();
        /// <inheritdoc cref="Log73.Extensible.ConsoleLogObject"/>
        public static readonly ConsoleLogObject Object = new();
        /// <inheritdoc cref="Log73.Extensible.ConsoleConfigureObject"/>
        public static readonly ConsoleConfigureObject Configure = new();

        /// <summary>
        /// Keeps <paramref name="str"/> at the bottom of your console.
        /// </summary>
        /// <param name="str">The string to keep at the bottom of your console. Less than the width of your console.</param>
        /// <param name="clearPrevious">If you want to overwrite the previous bottom message.</param>
        public static void AtBottomLog(string str, bool clearPrevious = false)
        {
            if (clearPrevious && _lastKeepMessage != null)
                ClearLastLine();
            _lastKeepMessage = str;
            if (str != null)
                Out.WriteLine(str);
        }

        /// <summary>
        /// Logs the <paramref name="value"/> using the <see cref="MessageTypes.Info"/> <see cref="MessageType"/>.
        /// </summary>
        public static void WriteLine(object value)
            => Log(Options.WriteLineMessageType, value);

        /// <summary>
        /// Logs a newline.
        /// </summary>
        public static void WriteLine()
            => Write('\n');

        public static void Write(object value)
            => Out.Write(value.SerializeObject());

        #region Info, Warn, Error, Debug + ObjectX methods

        /// <summary>
        /// Logs the <paramref name="value"/> using the <see cref="MessageTypes.Info"/> <see cref="MessageType"/>.
        /// </summary>
        /// <param name="value"></param>
        public static void Info(object value)
            => Log(MessageTypes.Info, value);

        /// <summary>
        /// Logs the <paramref name="value"/> using the <see cref="MessageTypes.Warn"/> <see cref="MessageType"/>.
        /// </summary>
        /// <param name="value"></param>
        public static void Warn(object value)
            => Log(MessageTypes.Warn, value);

        /// <summary>
        /// Logs the <paramref name="value"/> using the <see cref="MessageTypes.Error"/> <see cref="MessageType"/>.
        /// </summary>
        /// <param name="value"></param>
        public static void Error(object value)
            => Log(MessageTypes.Error, value);

        /// <summary>
        /// Logs the <paramref name="value"/> using the <see cref="MessageTypes.Debug"/> <see cref="MessageType"/>.
        /// </summary>
        /// <param name="value"></param>
        public static void Debug(object value)
            => Log(MessageTypes.Debug, value);

        #endregion

        /// <summary>
        /// Log a <see cref="System.Threading.Tasks.Task"/>'s start and failing or finishing.
        /// </summary>
        public static void Task(string name, Task task)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                if (Options.AlwaysLogTaskStart)
                    Log(MessageTypes.Start, $"{name}");
                task.Start();
                if (!Options.AlwaysLogTaskStart)
                    Log(MessageTypes.Start, $"{name}");
            }
            catch
            {
            }

            task.ContinueWith((task, _) =>
            {
                if (task.IsFaulted)
                {
                    Log(MessageTypes.Error, $"Task {name} Failed!");
                    Exception(task.Exception);
                }
                else
                    Log(MessageTypes.Done, $"{name} in {stopwatch.ElapsedMilliseconds}ms");
            }, null);
        }

        public static void Log(object value) => WriteLine(value);

        public static void Log(LogType logType, object value)
            => Log(MessageTypes.Get(logType), value);

        public static void Log(MessageType msgType, object value, LogInfoContext context = null)
        {
            context ??= new();
            // a message is currently being logged, add it to the log queue
            if (_lock == true)
            {
                _logQueue.Add((msgType, value, context));
                return;
            }

            _lock = true;
            _log(msgType, value, context);
            // handle the messages in log queue
            _handleLogQueue();
            _lock = false;
        }

        private static void _handleLogQueue()
        {
            // log all of the messages in log queue
            while (_logQueue.TryTake(out var msg))
            {
                _log(msg.msgType, msg.value, msg.context);
            }
        }

        /// <summary>
        /// the method that actually logs
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="value"></param>
        private static void _log(MessageType msgType, object value, LogInfoContext context)
        {
            if (msgType.LogType == LogType.Debug && Options.LogLevel != LogLevel.Debug)
                return;
            if (Options.LogLevel == LogLevel.Quiet && (
                msgType.LogType == LogType.Info ||
                msgType.LogType == LogType.Warn
            ))
                return;
            if (_lastKeepMessage != null)
                ClearLastLine();
            var outStream = msgType.WriteToStdErr ? Err : Out;
            var entireMessage = "";
            // write the LogType
            if (msgType.Name != null)
            {
                var logTypeString = $"{(msgType.Style.ToUpper ? msgType.Name.ToUpper() : msgType.Name)}";
                if (!Options.UseAnsi | Options.SeparateLogInfoWriteCalls)
                    _writeInfo(logTypeString, msgType.Style, outStream);
                else
                    entireMessage += _getStyledAsLogInfo(logTypeString, msgType.Style);
            }

            var serialized = value.SerializeObject();
            // write log infos
            context.Value = value;
            context.MessageType = msgType;
            context.ValueString = serialized;
            foreach (var extra in msgType.LogInfos)
            {
                var val = extra.GetValue(context);
                if (val == null)
                    continue;
                if (!Options.UseAnsi | Options.SeparateLogInfoWriteCalls)
                    _writeInfo(val, extra.Style, outStream);
                else
                {
                    entireMessage += _getStyledAsLogInfo(val, extra.Style);
                }
            }

            // write the actual message
            if (!Options.UseAnsi | Options.SeparateLogInfoWriteCalls)
                _writeStyle($"{serialized}\n", msgType.ContentStyle, outStream);
            else
                outStream.Write(entireMessage + msgType.ContentStyle.Style($"{serialized}\n", Options));
            if (_lastKeepMessage != null)
                outStream.WriteLine(_lastKeepMessage);
        }

        private static string _getStyledAsLogInfo(string str, Log73Style style)
        {
            var stri = style.Style(Options.UseBrackets ? $"[{str}]" : str, Options);
            if (Options.SpaceAfterInfo)
                stri += ' ';
            return stri;
        }

        /// <summary>
        /// Writes a string to <see cref="Out"/> with color using <see cref="System.Console.ForegroundColor"/> and <see cref="System.Console.BackgroundColor"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="style"></param>
        private static void _writeBasicColored(string str, Log73Style style, TextWriter writer)
        {
            var prevForegroundColor = SConsole.ForegroundColor;
            var prevBackgroundColor = SConsole.BackgroundColor;
            if (style.ForegroundColor != null)
            {
                var match = Ansi.BestMatch(style.ForegroundColor.Value, Options.ColorScheme);
                var con = Ansi.ColorToConsoleColor(match, Options.ColorScheme);
                SConsole.ForegroundColor = con;
            }

            if (style.BackgroundColor != null)
            {
                var match = Ansi.BestMatch(style.BackgroundColor.Value, Options.ColorScheme);
                var con = Ansi.ColorToConsoleColor(match, Options.ColorScheme);
                SConsole.BackgroundColor = con;
            }

            writer.Write(str);
            SConsole.ForegroundColor = prevForegroundColor;
            SConsole.BackgroundColor = prevBackgroundColor;
        }

        private static void _writeStyle(object value, Log73Style style, TextWriter writer)
        {
            var str = value.ToString();
            if (Options.UseAnsi)
                writer.Write(style.Style(str, Options));
            if (!Options.UseAnsi)
                _writeBasicColored(str, style, writer);
        }

        private static void _writeInfo(object value, Log73Style style, TextWriter writer)
        {
            var str = Options.UseBrackets ? $"[{value}]" : $"{value}";
            _writeStyle(str, style, writer);
            if (Options.SpaceAfterInfo)
                writer.Write(" ");
        }

        /// <summary>
        /// Logs an exception with <see cref="MessageTypes.Error"/> with it's StackTrace if it isn't null.
        /// </summary>
        /// <param name="exception"></param>
        public static void Exception(Exception exception)
        {
            Log(MessageTypes.Error, $@"An exception has occurred: {exception.Message}"
                                    + (exception.StackTrace == null ? "" : exception.StackTrace + '\n'));
        }

        /// <summary>
        /// Invokes <paramref name="finished"/> after the <see cref="Task"/> returned by <paramref name="code"/> has finished.
        /// </summary>
        /// <param name="code">The <see cref="Task"/> to be timed.</param>
        /// <param name="finished"></param>
        public static async Task StopwatchTask(Func<Task> code, Action<long> finished)
        {
            var stopwatch = Stopwatch.StartNew();
            await code();
            finished(stopwatch.ElapsedMilliseconds);
        }

        public static void SetCursorPosition((int left, int top) position)
            => SetCursorPosition(position.left, position.top);

        public static void ClearLastLine()
        {
            var str = "";
            for (int i = 0; i < SConsole.WindowWidth; i++)
                str += " ";
            Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
            Console.Write(str);
            Console.SetCursorPosition(0, Console.GetCursorPosition().Top);
        }

        private static string SerializeObject(this object obj)
        {
            // doesnt work with is
            // ReSharper disable once OperatorIsCanBeUsed
            var type = obj?.GetType();
            if ((type?.IsPrimitive ?? false) || type == typeof(string))
            {
                return obj?.ToString();
            }
            else
            {
                // check if ToString is overriden
                if (type?.GetMethod("ToString")?.DeclaringType != typeof(object))
                {
                    // ToString is overriden
                    return obj?.ToString();
                }

                return Options.ObjectSerializer.Serialize(obj);
            }
        }
    }
}