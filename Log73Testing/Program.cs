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
            //Console.Log("You can");
            ////MessageTypes.Error.Style.Invert = true;
            ////MessageTypes.Error.ExtraInfo.Add(new TimeExtraInfo());
            //Console.Error("log customized messages");
            //Console.Warn("with Log73!");
            //Console.ObjectYaml(new { AndAlso = "Log objects as Json, Xml or Yaml!" });
            //Console.Info("Info");
            //Console.Warn("Warn");
            //Console.Error("Error");
            //Console.Debug("Debug");

            //Console.Error("No styles :(");
            //MessageTypes.Error.Style.Invert = true;
            //MessageTypes.Error.ContentStyle.Color = System.Drawing.Color.Orange;
            //MessageTypes.Error.ContentStyle.Italic = true;
            //MessageTypes.Error.ContentStyle.Underline = true;
            //Console.Error("Look at all this styling!");

            //Console.Options.ObjectSerialization = ConsoleOptions.ObjectSerializationMethod.Json;
            //Console.Info("Object serialization as default");
            //Console.Info(new DummyObject());
            //Console.Info("Object serialization as JSON");
            //Console.ObjectJson(new DummyObject());
            //Console.Info("Object serialization as XML");
            //Console.ObjectXml(new DummyObject());
            //Console.Info("Object serialization as YAML");
            //Console.ObjectYaml(new DummyObject());
            //Console.Task("TestErrorTask", TestErrorTask());
            //Console.Task("TestSuccessTask", TestSuccessTask());
            //12.Dump();
            Console.Options.AlwaysLogTaskStart = true;
            MessageTypes.Start.Name = "Begin";
            MessageTypes.Done.Name = "Finished";
            Console.Task("TestSuccessTask", TestSuccessTask());
            Console.Task("TestErrorTask", TestErrorTask());
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
    }
}
