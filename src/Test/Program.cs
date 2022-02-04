global using Console = Log73.Console;
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

WithStyle("Bold", new() { Bold = true });
WithStyle("Faint", new() { Faint = true });
WithStyle("Italic", new() { Italic = true });
WithStyle("Underline", new() { Underline = true });
WithStyle("SlowBlink", new() { SlowBlink = true });
WithStyle("RapidBlink", new() { RapidBlink = true });
WithStyle("Invert", new() { Invert = true });
WithStyle("CrossedOut", new() { CrossedOut = true });

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
