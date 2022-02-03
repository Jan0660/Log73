global using Console = Log73.Console;
using Log73;
using Log73.LogPres;
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
