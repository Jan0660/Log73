global using Console = Log73.Console;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Log73;
using Log73.LogPres;
using Log73.Markan;
using SConsole = System.Console;

Console.LogTypes.TraditionalsAddPre(new TimeLogPre());

Console.Configure.EnableVirtualTerminalProcessing();

Console.WriteLine("WriteLine");
Console.WriteLine();
Console.Options.LogLevel = LogLevel.Trace;
Console.Debug("Debug");
Console.Info("Info");
Console.Warn("Warn");
Console.Error("Error");

Console.WriteLine();

void WithStyle(string text, Style style)
    => Console.Logger.PreWrite(text, new LogType { ContentStyle = style });

WithStyle("Bold", new() { AnsiStyle = AnsiStyle.Bold});
WithStyle("Faint", new() { AnsiStyle = AnsiStyle.Faint });
WithStyle("Italic", new() { AnsiStyle = AnsiStyle.Italic });
WithStyle("Underline", new() { AnsiStyle = AnsiStyle.Underline });
WithStyle("SlowBlink", new() { AnsiStyle = AnsiStyle.SlowBlink });
WithStyle("RapidBlink", new() { AnsiStyle = AnsiStyle.RapidBlink });
WithStyle("Invert", new() { AnsiStyle = AnsiStyle.Invert });
WithStyle("CrossedOut", new() { AnsiStyle = AnsiStyle.Strikethrough });

Console.WriteLine();

Console.WriteLine(new Mn("**Bold**"));
Console.WriteLine(new Mn("*Italic*"));
Console.WriteLine(new Mn("***Bold and Italic***"));
Console.WriteLine(new Mn("__Underline__"));
Console.WriteLine(new Mn("~~Strikethrough~~"));
Console.WriteLine(new Mn("[f#ff0000]Red foreground[f#]"));
Console.WriteLine(new Mn("[b#ff0000]Red background[b#]"));
Console.WriteLine(new Mn("[f#ff0000][b#ff0000]Red[b#][f#]"));
Console.WriteLine(new Mn("[hyperlink](https://example.com)"));
