using System;
using Out = System.Console;
using OSM = Log73.ConsoleOptions.ObjectSerializationMethod;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Log73.ExtensionMethod;
using System.Diagnostics;

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

    public static class Console
    {
        private static bool _lock = false;
        private static List<(MessageType msgType, object value)> _logQueue = new();
        private static TextWriter StdOut = Out.Out;
        private static TextWriter StdErr = Out.Error;
        private static TextReader StdIn = Out.In;

        public static ConsoleOptions Options = new ConsoleOptions();
        
        /// <summary>
        /// Logs the <paramref name="value"/> using the <see cref="MessageTypes.Info"/> <see cref="MessageType"/>.
        /// </summary>
        public static void WriteLine(object value)
            => Log(MessageTypes.Info, value);

        public static void Write(object value)
            => StdOut.Write(value.Serialize());

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
        
        /// <summary>
        /// Logs the <paramref name="obj"/> serialized as JSON using the <see cref="MessageTypes.Info"/> <see cref="MessageType"/>.
        /// </summary>
        public static void ObjectJson(object obj)
            => ObjectJson(MessageTypes.Info, obj);
        /// <summary>
        /// Logs the <paramref name="value"/> serialized as JSON using the matching <see cref="MessageType"/> for the <paramref name="logType"/>.
        /// </summary>
        public static void ObjectJson(LogType logType, object obj)
            => ObjectJson(MessageTypes.Get(logType), obj);
        
        /// <summary>
        /// Logs the <paramref name="obj"/> serialized as JSON using the specified <see cref="MessageType"/>.
        /// </summary>
        public static void ObjectJson(MessageType msgType, object obj)
            => Log(msgType, obj.SerializeAsJson());
        
        /// <summary>
        /// Logs the <paramref name="obj"/> serialized as XML using the <see cref="MessageTypes.Info"/> <see cref="MessageType"/>.
        /// </summary>
        public static void ObjectXml(object obj)
            => ObjectXml(MessageTypes.Info, obj);
        
        /// <summary>
        /// Logs the <paramref name="obj"/> serialized as XML using the matching <see cref="MessageType"/> for the <paramref name="logType"/>.
        /// </summary>
        public static void ObjectXml(LogType logType, object obj)
            => ObjectXml(MessageTypes.Get(logType), obj);
        
        /// <summary>
        /// Logs the <paramref name="obj"/> serialized as XML using the specified <see cref="MessageType"/>.
        /// </summary>
        public static void ObjectXml(MessageType msgType, object obj)
            => Log(msgType, obj.SerializeAsXml());
        
        /// <summary>
        /// Logs the <paramref name="obj"/> serialized as YAML using the <see cref="MessageTypes.Info"/> <see cref="MessageType"/>.
        /// </summary>
        public static void ObjectYaml(object obj)
            => ObjectYaml(MessageTypes.Info, obj);
        
        /// <summary>
        /// Logs the <paramref name="obj"/> serialized as YAML using the matching <see cref="MessageType"/> for the <paramref name="logType"/>.
        /// </summary>
        public static void ObjectYaml(LogType logType, object obj)
            => ObjectYaml(MessageTypes.Get(logType), obj);
        
        /// <summary>
        /// Logs the <paramref name="obj"/> serialized as YAML using the specified <see cref="MessageType"/>.
        /// </summary>
        public static void ObjectYaml(MessageType msgType, object obj)
        {
            Log(msgType, obj.SerializeAsYaml());
        }
        #endregion

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

            task.ContinueWith((task, state) =>
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

        public static void Log(MessageType msgType, object value)
        {
            // a message is currently being logged, add it to the log queue
            if (_lock == true)
            {
                _logQueue.Add((msgType, value));
                return;
            }

            _lock = true;
            _log(msgType, value);
            // handle the messages in log queue
            _handleLogQueue();
            _lock = false;
        }

        public static void _handleLogQueue()
        {
            // log all of the messages in log queue and remove them after they're logged
            while (_logQueue.Count != 0)
            {
                var msg = _logQueue[0];
                _log(msg.msgType, msg.value);
                _logQueue.RemoveAt(0);
            }
        }

        /// <summary>
        /// the method that actually logs
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="value"></param>
        private static void _log(MessageType msgType, object value)
        {
            if (msgType.LogType == LogType.Debug && Options.LogLevel != LogLevel.Debug)
                return;
            if (Options.LogLevel == LogLevel.Quiet && (
                msgType.LogType == LogType.Info ||
                msgType.LogType == LogType.Warn
            ))
                return;
            var outStream = msgType.WriteToStdErr ? StdErr : StdOut;
            var entireMessage = "";
            // write the LogType
            if (msgType.Name != null)
            {
                var logTypeString = $"{(msgType.Style.ToUpper ? msgType.Name.ToUpper() : msgType.Name)}";
                if (!Options.Use24BitAnsi | Options.SeperateLogInfoWriteCalls)
                    _writeInfo(logTypeString, msgType.Style, outStream);
                else
                    entireMessage += _getStyledAsLogInfo(logTypeString, msgType.Style);
            }
            // write log infos
            var logInfoContext = new LogInfoContext { Value = value, MessageType = msgType };
            foreach (var extra in msgType.LogInfos)
            {
                var val = extra.GetValue(logInfoContext);
                if (val == null)
                    continue;
                if (!Options.Use24BitAnsi | Options.SeperateLogInfoWriteCalls)
                    _writeInfo(val, extra.Style, outStream);
                else
                {
                    entireMessage += _getStyledAsLogInfo(val, extra.Style);
                }
            }
            // write the actual message
            if (!Options.Use24BitAnsi | Options.SeperateLogInfoWriteCalls)
                _writeStyle($"{value.Serialize()}\n", msgType.ContentStyle, outStream);
            else
                outStream.Write(GetStyled($"{entireMessage}{value.Serialize()}\n", msgType.ContentStyle));
        }
        private static string _getStyledAsLogInfo(string str, ConsoleStyleOption style)
        {
            str = Options.UseBrackets ? $"[{str}]" : str;
            var stri = GetStyled(str, style);
            if (Options.SpaceAfterInfo)
                stri += ' ';
            return stri;
        }
        /// <summary>
        /// Writes a string to <see cref="StdOut"/> with color using <see cref="System.Console.ForegroundColor"/> and <see cref="System.Console.BackgroundColor"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="style"></param>
        private static void _writeBasicColored(string str, ConsoleStyleOption style, TextWriter writer)
        {
            var prevForegroundColor = Out.ForegroundColor;
            var prevBackgroundColor = Out.BackgroundColor;
            if (style.Color != null)
            {
                var match = Ansi.BestMatch(style.Color.Value, Options.ColorScheme);
                var con = Ansi.ColorToConsoleColor(match, Options.ColorScheme);
                Out.ForegroundColor = con;
            }
            if (style.BackgroundColor != null)
            {
                var match = Ansi.BestMatch(style.BackgroundColor.Value, Options.ColorScheme);
                var con = Ansi.ColorToConsoleColor(match, Options.ColorScheme);
                Out.BackgroundColor = con;
            }
            writer.Write(str);
            Out.ForegroundColor = prevForegroundColor;
            Out.BackgroundColor = prevBackgroundColor;
        }

        public static void _writeStyle(object value, ConsoleStyleOption style, TextWriter writer)
        {
            var str = value.ToString();
            if(Options.Use24BitAnsi)
                writer.Write(GetStyled(str, style));
            if(!Options.Use24BitAnsi)
            {
                _writeBasicColored(GetStyled(str, style), style, writer);
            }
        }

        public static string GetStyled(string str, ConsoleStyleOption style)
        {
            if (style.Bold)
                str = Ansi.Bold + str + Ansi.BoldOff;
            if (style.Italic)
                str = Ansi.Italic + str + Ansi.NotItalic;
            if (style.Underline)
                str = Ansi.Underline + str + Ansi.UnderlineOff;
            if (style.Invert)
                str = Ansi.Invert + str + Ansi.InvertOff;
            if (style.CrossedOut)
                str = Ansi.CrossedOut + str + Ansi.CrossedOutOff;
            if (style.SlowBlink)
                str = Ansi.SlowBlink + str + Ansi.BlinkOff;
            if (style.RapidBlink)
                str = Ansi.RapidBlink + str + Ansi.BlinkOff;
            if (style.Faint)
                str = Ansi.Faint + str + Ansi.NormalColor;
            if (Options.Use24BitAnsi)
                str = Ansi.ApplyColor(str, style.Color, style.BackgroundColor, Options.Use24BitAnsi);
            return str;
        }

        private static void _writeInfo(object value, ConsoleStyleOption style, TextWriter writer)
        {
            var str = Options.UseBrackets ? $"[{value}]" : $"{value}";
            _writeStyle(str, style, writer);
            if (Options.SpaceAfterInfo)
                writer.Write(" ");
        }

        public static void Exception(Exception exception)
        {
            Log(MessageTypes.Error, $@"An exception has occurred: {exception.Message}
{exception.StackTrace}");
        }

        #region System.Console compatibility
        /// <inheritdoc cref="System.Console.Beep"/>
        public static void Beep()
            => Out.Beep();
        
        /// <inheritdoc cref="System.Console.Beep(int, int)"/>
        public static void Beep(int frequency, int duration)
            => Out.Beep(frequency, duration);
        
        /// <inheritdoc cref="System.Console.Clear"/>
        public static void Clear()
            => Out.Clear();
        
        /// <inheritdoc cref="System.Console.SetWindowSize(int, int)"/>
        public static void SetWindowSize(int width, int height)
            => Out.SetWindowSize(width, height);
        
        /// <inheritdoc cref="System.Console.SetCursorPosition(int, int)"/>
        public static void SetCursorPosition(int left, int top)
            => Out.SetCursorPosition(left, top);
        
        /// <inheritdoc cref="System.Console.SetBufferSize(int, int)"/>
        public static void SetBufferSize(int width, int height)
            => Out.SetBufferSize(width, height);
        
        /// <inheritdoc cref="System.Console.ResetColor"/>
        public static void ResetColor()
            => Out.ResetColor();
        
        /// <inheritdoc cref="TextReader.Read"/>
        public static int Read()
            => StdIn.Read();
        
        /// <inheritdoc cref="TextReader.ReadLine"/>
        public static string ReadLine()
            => StdIn.ReadLine();

        public static (int, int) GetCursorPosition()
            => (Out.CursorLeft, Out.CursorTop);

        public static void SetIn(TextReader textReader)
        {
            StdIn = textReader ?? throw new ArgumentNullException(nameof(textReader));
        }
        
        public static void SetOut(TextWriter textWriter)
        {
            StdOut = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
        }
        
        public static void SetError(TextWriter textWriter)
        {
            StdErr = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
        }

        #endregion

        public static void SetCursorPosition((int, int) position)
            => SetCursorPosition(position.Item1, position.Item2);

        private static string Serialize(this object obj)
        {
            // doesnt work with is
            // ReSharper disable once OperatorIsCanBeUsed
            if (obj.IsValueType() | obj.GetType() == typeof(string))
            {
                return obj.ToString();
            }
            else
            {
                if ((int) Options.ObjectSerialization % (int) 2 == 1)
                {
                    // check if ToString is overriden
                    // todo: probly not best way of doing this
                    if (obj.ToString() != obj.GetType().ToString())
                    {
                        // ToString is overriden
                        return obj.ToString();
                    }
                }

                return Options.ObjectSerialization switch
                {
                    OSM.AlwaysToString => obj.ToString(),
                    OSM.Json => obj.SerializeAsJson(),
                    OSM.AlwaysJson => obj.SerializeAsJson(),
                    OSM.Xml => obj.SerializeAsXml(),
                    OSM.AlwaysXml => obj.SerializeAsXml(),
                    OSM.Yaml => obj.SerializeAsYaml(),
                    OSM.AlwaysYaml => obj.SerializeAsYaml(),
                    _ => throw new Exception("invalid Options.ObjectSerialization")
                };
                throw new Exception("invalid Options.ObjectSerialization");
            }
        }
    }
}