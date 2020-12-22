using System;
using Out = System.Console;
using ColorOut = Colorful.Console;
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
    public enum LogType { Info = 1, Warn = 2, Error = 4, Debug = 8 }
    public static class Console
    {
        private static bool _lock = false;
        private static List<(MessageType msgType, object value)> _logQueue = new();

        public static ConsoleOptions Options = new ConsoleOptions();
        public static void WriteLine(object value)
            => Log(Options.WriteLogType, value);
        public static void Write(object value)
            => Log(Options.WriteLogType, value);
        public static void Info(object value)
            => Log(LogType.Info, value);
        public static void Warn(object value)
           => Log(LogType.Warn, value);
        public static void Error(object value)
           => Log(LogType.Error, value);
        public static void Debug(object value)
           => Log(LogType.Debug, value);
        public static void ObjectJson(object obj)
            => ObjectJson(MessageTypes.Info, obj);
        public static void ObjectJson(LogType logType, object obj)
            => ObjectJson(MessageTypes.Get(logType), obj);
        public static void ObjectJson(MessageType msgType, object obj)
            => Log(msgType, obj.SerializeAsJson());
        public static void ObjectXml(object obj)
            => ObjectXml(MessageTypes.Info, obj);
        public static void ObjectXml(LogType logType, object obj)
            => ObjectXml(MessageTypes.Get(logType), obj);
        /// <summary>
        /// only works with public types
        /// </summary>
        /// <param name="obj"></param>
        public static void ObjectXml(MessageType msgType, object obj)
            => Log(msgType, obj.SerializeAsXml());
        public static void ObjectYaml(object obj)
            => ObjectYaml(MessageTypes.Info, obj);
        public static void ObjectYaml(LogType logType, object obj)
            => ObjectYaml(MessageTypes.Get(logType), obj);

        public static void ObjectYaml(MessageType msgType, object obj)
        {
            Log(msgType, obj.SerializeAsYaml());
        }

        public static void Task(string name, Task task)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                task.Start();
                Log(MessageTypes.Start, $"{name}");
            }
            catch { }
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
            _writeInfo($"{(msgType.Style.ToUpper ? msgType.Name.ToUpper() : msgType.Name)}", msgType.Style);
            foreach (var extra in msgType.ExtraInfo)
            {
                _writeInfo(extra.GetValue(new ExtraInfoContext { Value = value, MessageType = msgType }), extra.Style);
            }
            //if (value.GetType() == typeof(string))
            //    value = ((string)value).Replace("{", "{{").Replace("}", "}}");
            _writeStyle($"{value.Serialize()}\n", msgType.ContentStyle);
        }

        public static void _writeStyle(object value, ConsoleStyleOption style)
        {
            var str = value.ToString();
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
            if (style.Color != null)
                ColorOut.Write(str, style.Color.Value);
            else
                Out.Write(str);
        }

        private static void _writeInfo(object value, ConsoleStyleOption style)
        {
            var str = Options.UseBrackets ? $"[{value}]" : $"{value}";
            _writeStyle(str, style);
            if (Options.SpaceAfterInfo)
                Out.Write(" ");
        }

        public static void Exception(Exception exception)
        {
            Log(MessageTypes.Error, $@"An exception has ocurred: {exception.Message}
{exception.StackTrace}");
        }

        #region System.Console compatibility
        public static void Beep()
            => Out.Beep();
        public static void Beep(int frequency, int duration)
            => Out.Beep(frequency, duration);
        public static void Clear()
            => Out.Clear();
        public static void SetWindowSize(int width, int height)
            => Out.SetWindowSize(width, height);
        public static void SetCursorPosition(int left, int top)
            => Out.SetCursorPosition(left, top);
        public static void SetBufferSize(int width, int height)
            => Out.SetBufferSize(width, height);
        public static void ResetColor()
            => Out.ResetColor();
        public static int Read()
            => Out.Read();
        public static string ReadLine()
            => Out.ReadLine();
        public static ConsoleKeyInfo ReadKey()
            => Out.ReadKey();
        public static ConsoleKeyInfo ReadKey(bool intercept)
            => Out.ReadKey(intercept);
        public static (int, int) GetCursorPosition()
            => (Out.CursorLeft, Out.CursorTop);
        #endregion

        private static string Serialize(this object obj)
        {
            if (obj.IsValueType())
            {
                return obj.ToString();
            }
            else
            {
                if ((int)Options.ObjectSerialization % (int)2 == 1)
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
