using System;
using System.Threading.Tasks;
using Console = Log73.Console;
using Out = System.Console;
using Log73;
using Log73.ExtensionMethod;
namespace Log73Testing
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.Options.LogLevel = LogLevel.Debug;
            MessageTypes.Error.ExtraInfo.Add(new TimeExtraInfo());
            Console.Options.Style.Error.Bold = true;
            Console.Options.Style.Error.Italic = true;
            Console.Options.Style.Error.Underline = true;
            Console.Options.Style.Error.Invert = true;
            Console.Options.Style.Error.CrossedOut = true;
            Console.Options.Style.Error.SlowBlink = true;
            Console.Error("the");
            Console.Options.Style.Error.Bold = false;
            Console.Options.Style.Error.Italic = false;
            Console.Options.Style.Error.Underline = false;
            Console.Error("not bold");
            Console.WriteLine("Hello Log73!");
            Console.Info("Info");
            Console.Warn("Warn");
            Console.Error("Error");
            Console.Debug("Debug");
            Console.Options.ObjectSerialization = ConsoleOptions.ObjectSerializationMethod.Json;
            Console.Info("Object serialization as default");
            Console.Info(new DummyObject());
            Console.Info("Object serialization as JSON");
            Console.ObjectJson(new DummyObject());
            Console.Info("Object serialization as XML");
            Console.ObjectXml(new DummyObject());
            Console.Info("Object serialization as YAML");
            Console.ObjectYaml(new DummyObject());
            Console.Task("TestErrorTask", TestErrorTask());
            Console.Task("TestSuccessTask", TestSuccessTask());
            12.Dump();
            await Task.Delay(2000);
        }

        static async Task TestErrorTask() => throw new Exception("bruh");
        static async Task TestSuccessTask() => Task.Delay(1000);

        public class DummyObject
        {
            public string DummyText = "Hello Log73!";
            public int Number = 73;

            public override string ToString()
                => "Hello!";
        }
    }
}
