using System;
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
            Console.Options.LogLevel = LogLevel.Debug;
            //Console.Log("You can");
            ////MessageTypes.Error.Style.Invert = true;
            ////MessageTypes.Error.LogInfo.Add(new TimeLogInfo());
            //Console.Error("log customized messages");
            //Console.Warn("with Log73!");
            //Console.ObjectYaml(new { AndAlso = "Log objects as Json, Xml or Yaml!" });
            //Console.Info("Info");
            //Console.Warn("Warn");
            Console.Options.ObjectSerialization = ConsoleOptions.ObjectSerializationMethod.AlwaysJson;
            //Console.Options.Use24BitAnsi = false;
            Console.Options.SeparateLogInfoWriteCalls = true;
            Console.Options.ColorScheme = new RiderDarkMelonColorScheme();
            Console.Options.SpaceAfterInfo = false;
            //MessageTypes.Debug.ContentStyle.BackgroundColor = Color.Black;
            MessageTypes.Debug.LogInfos.Add(new CallingMethodLogInfo());
            MessageTypes.Debug.LogInfos.Add(new ThreadLogInfo());
            MessageTypes.Debug.LogInfos.Add(new TimeLogInfo());
            MessageTypes.Debug.LogInfos.Add(new CallingModuleLogInfo());
            MessageTypes.Debug.LogInfos.Add(new TypeLogInfo());
            MessageTypes.Debug.LogInfos.Add(new NullLogInfo());
            MessageTypes.Debug.LogInfos.Add(new CallingClassLogInfo()
                {Style = new() {Color = System.Drawing.Color.LightPink}});
            Console.Log(new object());
            Console.Debug("Debug");
            MessageTypes.Warn.Name = null;
            Console.Warn("Warn with null");
            SomeOtherMethod();
            "pls be fixed".Dump();
            await Console.StopwatchTask(() => Task.Delay(100), l => Console.Log(new MessageType()
            {
                Name = "Stopwatched",
                Style = new()
                {
                    Color = Color.Blue
                }
            }, $"Stopwatched task finished in {l}ms."));
            Console.Error("No styles :(");
            MessageTypes.Error.Style.Invert = true;
            MessageTypes.Error.ContentStyle.Color = System.Drawing.Color.Orange;
            MessageTypes.Error.ContentStyle.Italic = true;
            MessageTypes.Error.ContentStyle.Underline = true;
            Console.Error("Look at all this styling!");

            Console.Options.ObjectSerialization = ConsoleOptions.ObjectSerializationMethod.AlwaysToString;
            Console.Info("Object serialization using ToString by default");
            Console.Info(new DummyObject());
            Console.Info("Object serialization as JSON");
            Console.ObjectJson(new DummyObject());
            Console.Info("Object serialization as XML");
            Console.ObjectXml(new DummyObject());
            Console.Info("Object serialization as YAML");
            Console.ObjectYaml(new DummyObject());
            //12.Dump();
            Console.Options.AlwaysLogTaskStart = true;
            MessageTypes.Start.Name = "Begin";
            MessageTypes.Done.Name = "Finished";
            Console.Task("TestSuccessTask", Task.Delay(1000));
            Console.Task("TestErrorTask", Task.Factory.StartNew(() => throw new Exception("exception message")));
            await Task.Delay(2000);
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
