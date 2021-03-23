﻿using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Console = Log73.Console;
using Out = System.Console;
using Log73;
using Log73.ColorSchemes;
using Log73.ExtensionMethod;

namespace Log73Testing
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            //Console.Info("Info");
            //Console.Warn("Warn");
            Console.Options.ObjectSerialization = ConsoleOptions.ObjectSerializationMethod.AlwaysJson;
            Console.Options.UseAnsi = false;
            Console.Options.SeparateLogInfoWriteCalls = true;
            Console.Options.ColorScheme = new RiderDarkMelonColorScheme();
            SomeOtherMethod();
            Console.Options.ObjectSerialization = ConsoleOptions.ObjectSerializationMethod.AlwaysToString;
            Console.AtBottomLog(null);
            AtBottomLogs();
            for (int i = 0; i <= 200; i++)
            {
                Console.Log(i);
                ConsoleProgressBar.Update(i, 200);
                await Task.Delay(20);
            }

            await All();
        }

        static void AtBottomLogs()
        {
            // go through this code line by line for *true* immersion
            Console.Log("There should be `hello` under this line.");
            Console.AtBottomLog("hello");
            Console.Log("Now it's under this one!");
            Console.AtBottomLog("bye", true);
            Console.Log("Now it's been overwritten by `bye`!");
            Console.AtBottomLog(null);
            Console.Log("Now its not kept at the bottom anymore.");
        }

        static async Task All()
        {
            ReadmeBasicExample();
            ReadmeStyles();
            ReadmeLogInfos();
            ObjectSerialization();
            await ReadmeMessageTypes();
            AllLogTypes();
        }

        static void ReadmeBasicExample()
        {
            Console.Options.LogLevel = LogLevel.Debug;
            Console.Log("You can");
            MessageTypes.Error.Style.Invert = true;
            MessageTypes.Error.LogInfos.Add(new TimeLogInfo());
            Console.Error("log customized messages");
            Console.Warn("with Log73!");
            Console.ObjectYaml(new { AndAlso = "Log objects as Json, Xml or Yaml!" });
        }

        static void ReadmeStyles()
        {
            Console.Error("No styles :(");
            MessageTypes.Error.Style.Invert = true;
            MessageTypes.Error.ContentStyle.Color = System.Drawing.Color.Orange;
            MessageTypes.Error.ContentStyle.Italic = true;
            MessageTypes.Error.ContentStyle.Underline = true;
            Console.Error("Look at all this styling!");
        }

        static void ReadmeLogInfos()
        {
            MessageTypes.Debug.LogInfos.Add(new CallingMethodLogInfo());
            MessageTypes.Debug.LogInfos.Add(new ThreadLogInfo());
            MessageTypes.Debug.LogInfos.Add(new TimeLogInfo());
            MessageTypes.Debug.LogInfos.Add(new CallingModuleLogInfo());
            MessageTypes.Debug.LogInfos.Add(new TypeLogInfo());
            MessageTypes.Debug.LogInfos.Add(new NullLogInfo());
            MessageTypes.Debug.LogInfos.Add(new CallingClassLogInfo()
                {Style = new() {Color = System.Drawing.Color.LightPink}});
            Console.Debug("Hello");
        }

        static void ObjectSerialization()
        {
            Console.Info("Object serialization using ToString by default");
            Console.Info(new DummyObject());
            Console.Info("Object serialization as JSON");
            Console.ObjectJson(new DummyObject());
            Console.Info("Object serialization as XML");
            Console.ObjectXml(new DummyObject());
            Console.Info("Object serialization as YAML");
            Console.ObjectYaml(new DummyObject());
        }

        static async Task ReadmeMessageTypes()
        {
            Console.Options.AlwaysLogTaskStart = true;
            MessageTypes.Start.Name = "Begin";
            MessageTypes.Done.Name = "Finished";
            Console.Task("TestSuccessTask", Task.Delay(1000));
            Console.Task("TestErrorTask", Task.Factory.StartNew(() => throw new Exception("exception message")));
            await Task.Delay(2000);
        }

        static void AllLogTypes()
        {
            Console.Options.LogLevel = LogLevel.Debug;
            foreach (var value in Enum.GetValues<LogType>())
            {
                Console.Log(value, value.ToString());
            }
        }

        static async Task TestErrorTask() => throw new Exception("exception message");
        static async Task TestSuccessTask() => await Task.Delay(1000);

        public class DummyObject
        {
            public string DummyText = "Hello Log73!";
            public int Number = 73;

            public override string ToString()
                => "Hello!";
        }

        public static void SomeOtherMethod()
            => "Hello".DumpDebug();

        public class NullLogInfo : ILogInfo
        {
            public ConsoleStyleOption Style { get; set; } = new ConsoleStyleOption();

            public string GetValue(LogInfoContext context)
                => null;
        }
    }
}
