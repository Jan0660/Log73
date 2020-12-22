﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using Newtonsoft.Json;

namespace Log73
{
    public enum LogLevel { Quiet, Standard, Debug }
    public class ConsoleOptions
    {
        public LogType WriteLogType = LogType.Info;

        public LogLevel LogLevel = LogLevel.Standard;

        public LogType DumpLogType = LogType.Info;
        [Flags]
        public enum ObjectSerializationMethod
        {
            /// <summary>
            /// Always uses .ToString(), even if it is not overriden
            /// </summary>
            AlwaysToString = 0,
            /// <summary>
            /// Uses .ToString() if it is overriden
            /// </summary>
            OverridenToString = 1,
            /// <summary>
            /// Always uses Json
            /// </summary>
            AlwaysJson = 2,
            /// <summary>
            /// Uses .ToString() if it is overriden, otherwise uses Json
            /// </summary>
            Json = 3,
            /// <summary>
            /// Always uses Xml
            /// </summary>
            AlwaysXml = 4,
            /// <summary>
            /// Uses .ToString() if it is overriden, otherwise uses Xml
            /// </summary>
            Xml = 5,
            /// <summary>
            /// Always uses Yaml
            /// </summary>
            AlwaysYaml = 8,
            /// <summary>
            /// Uses .ToString() if it is overriden, otherwise uses Yaml
            /// </summary>
            Yaml = 9
        }
        /// <summary>
        /// The method of object serialization to use when logging a non-value type
        /// </summary>
        public ObjectSerializationMethod ObjectSerialization = ObjectSerializationMethod.AlwaysToString;

        public bool UseBrackets = true;

        public bool SpaceAfterInfo = true;
        // TODO: Colorful.Console might also have this
        public bool UseAnsi = true;

        public JsonSerializerSettings JsonSerializerSettings = new()
        {
            Formatting = Newtonsoft.Json.Formatting.Indented
        };

        public XmlWriterSettings XmlWriterSettings = new() { Indent = true };

        public ConsoleStyleOptions Style = new();
    }

    public class ConsoleStyleOptions
    {
        public ConsoleStyleOption Info = new() { Color = Color.Cyan };
        public ConsoleStyleOption Warn = new() { Color = Color.Yellow };
        public ConsoleStyleOption Error = new() { Color = Color.Red, Bold = true };
        public ConsoleStyleOption Debug = new() { Color = Color.White };
        /// <summary>
        /// if you want to put a space after message type
        /// </summary>
        public bool SpaceAfterType = true;
        public ConsoleStyleOption Get(LogType logType)
            => logType switch
            {
                LogType.Info => Info,
                LogType.Warn => Warn,
                LogType.Error => Error,
                LogType.Debug => Debug
            };
    }

    public class ConsoleStyleOption
    {
        public Color? Color;
        public bool Bold;
        public bool Italic;
        public bool Underline;
        public bool ToUpper = true;
        public bool Invert;
        public bool CrossedOut;
        public bool SlowBlink;
        public bool RapidBlink;
        public bool Faint;
    }
}
