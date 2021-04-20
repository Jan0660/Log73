# Log73
[![NuGet](https://img.shields.io/nuget/v/Log73)](https://www.nuget.org/packages/Log73/)
[![NuGet Prerelease](https://img.shields.io/nuget/vpre/Log73)](https://www.nuget.org/packages/Log73/)
[![chat on discord](https://img.shields.io/discord/749601186155462748?logo=discord)](https://discord.gg/zBbV56e)

#### A powerful and customizable .NET logging library

## Basic example

```csharp
using Console = Log73.Console;
using Log73;

Console.Options.LogLevel = LogLevel.Debug;
Console.Log("You can");
MessageTypes.Error.Style.Invert = true;
MessageTypes.Error.LogInfos.Add(new TimeExtraInfo());
Console.Error("log customized messages");
Console.Warn("with Log73!");
Console.ObjectYaml(new { AndAlso = "Log objects as Json, Xml or Yaml!" });
```

![output](https://i.imgur.com/AI3b8Lk.png)

# Table of contents

1. [Using Ansi](#ansi)
2. [LogTypes and LogLevels](#LogTypes%20and%20LogLevels)
3. [Styling](#styling)
4. [Message Types](#message-types)
5. [LogInfos](#loginfos)
6. [Progress Bars](#Progress%20Bars)
6. [Use with ASP.NET](#Use%20with%20ASP.NET)

## Ansi

All of the styling options are made possible thanks to [ANSI escape codes](https://en.wikipedia.org/wiki/ANSI_escape_code). Your terminal might not support them and your console will look like this:

![visual representation of pain when rider somehow disables ansi on windows](https://i.imgur.com/uUreBii.png)

Note: the only terminal that displays them like this I have discovered is JetBrains Rider on Windows(when on Linux it works fine)

If you happen to have this problem you can disable ansi:

```csharp
// note: this also automatically sets Console.Options.Use24BitAnsi to false
Console.Options.UseAnsi = false;
// you can also disable 24 bit colors separately
Console.Options.Use24BitAnsi = false;
```

After disabling ansi Log73 handles converting [`System.Drawing.Color`](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.color) to the [`System.ConsoleColor`](https://docs.microsoft.com/en-us/dotnet/api/system.consolecolor) enum for you

![color conversion](https://i.imgur.com/5e21xC9.png)

Many terminals seem to use their own color scheme for the basic 16 colors, meaning that it might not look the best if Log73 adjusts the color according to the wrong terminal, that's why you can use one of the premade ones or create your own by inheriting from the `IColorScheme` interface!



For example, if you use JetBrains Rider with the dark melon theme:

```csharp
Console.Options.ColorScheme = new RiderDarkMelonColorScheme();
```

Default color scheme used is `WindowsConsoleColorScheme`.

## LogTypes and LogLevels

A `LogType` basically determines whether or not a message will be logged according to your `LogLevel` in `Console.Options.LogLevel`:

|                 | `LogLevel.Quiet` | `LogLevel.Standard` | `LogLevel.Debug` |
| --------------- | ---------------- | ------------------- | ---------------- |
| `LogType.Error` | ✅                | ✅                   | ✅                |
| `LogType.Warn`  | ✅                | ✅                   | ✅                |
| `LogType.Info`  | ❌                | ✅                   | ✅                |
| `LogType.Debug` | ❌                | ❌                   | ✅                |



## Styling

You can also customize the default styles for the default `MessageType`s, by changing color or enabling ANSI escape codes. Please note that all of the colors/ANSI codes might not be supported in all terminal emulators.

```csharp
Console.Error("No styles :(");
MessageTypes.Error.Style.Invert = true;
MessageTypes.Error.ContentStyle.Color = System.Drawing.Color.Orange;
MessageTypes.Error.ContentStyle.Italic = true;
MessageTypes.Error.ContentStyle.Underline = true;
Console.Error("Look at all this styling!");
```



Here you can see an example of styling, and also how Windows 10's conhost doesn't support italic.

![styling in conhost](https://i.imgur.com/L6j0HIr.png)

While [Alacritty](https://github.com/alacritty/alacritty) supports it:

![styling in alacritty](https://i.imgur.com/MIUIezo.png)

## Message Types

Log73 currently comes with 6 message types: Info, Warn, Error, Debug, Start, Done. All of them except Start and Done are used by their corresponding `Console.X(...)`.

The Start and Done message types are used for the `Console.Task(taskName, task)` method, which looks like this by default with the following code:

```csharp
Console.Options.AlwaysLogTaskStart = true;
Console.Task("TestSuccessTask", TestSuccessTask());
Console.Task("TestErrorTask", TestErrorTask());
// Tasks defined somewhere else in your code
static async Task TestErrorTask() => throw new Exception("exception message");
static async Task TestSuccessTask() => await Task.Delay(1000);
```

![original MessageTypes](https://i.imgur.com/pR63J3H.png)

If you like the words "Begin" and "Finished" more you can change them!

```csharp
Console.Options.AlwaysLogTaskStart = true;
MessageTypes.Start.Name = "Begin";
MessageTypes.Done.Name = "Finished";
Console.Task("TestSuccessTask", TestSuccessTask());
Console.Task("TestErrorTask", TestErrorTask());
```

![edited MessageTypes](https://i.imgur.com/T21fobE.png)
## LogInfos
`LogInfo`s are a way to add extra information to a `MessageType` that is logged. You can either use the ones that come with Log73 by default or create your own by inheriting from the `IlogInfo` interface. You add it onto a `MessageType` like so:

```csharp
MessageTypes.Debug.LogInfos.Add(new TimeLogInfo());
```

TIP: use the `MessageTypes.AsArray` method to get the default `MessageType`s as an array.

Here's an example of how they look like:

![too many loginfos](https://i.imgur.com/ysLr5MN.png)

They can also be styled using the `Style` property. Log73 comes with the following `LogInfos` by default:

| Type name               | Description                                                  |
| ----------------------- | ------------------------------------------------------------ |
| `TimeLogInfo`           | Logs the current time. By default it uses the `hh:mm:ss` format, a different format can be used by passing it to the constructor. e.g. `08:47:57` |
| `CallingMethodLogInfo`* | Logs the full name of the method that the log statement has been called from. e.g. `Log73Testing.Program.Main` |
| `CallingClassLogInfo`*  | Logs the full name of the class that the log statement has been called from. e.g. `Log73Testing.Program` |
| `CallingModuleLogInfo`* | Logs the file name of the module that the log statement has been called from. e.g. `Log73Testing.dll` |
| `ThreadLogInfo`         | Logs the name of the current thread or `Unnamed` when the thread doesn't have a name. Along with `B` or/and `P` after `\` if the thread is a background thread and thread pool thread respectively. e.g. `Unnamed\BP` |
| `TypeLogInfo`           | Logs the type name of the object that is being logged. Can be switched between `FullName` and `Name`  of the object's type using the `FullName` property. e.g. `System.String` or `String` |

\* When used in async or anonymous method scenarios they have problem getting the stack frame and will appear as `Unable to get <>` when they cannot do so.

# Progress Bars
Another thing Log73 can do is write text and keep it at the bottom of your console, which can be used for progress bars!
```csharp
// write text to be kept at bottom of console
Console.AtBottomLog("Text kept at bottom.");
Console.WriteLine("Wow it works, cool.");
for (int i = 0; i <= 70; i++)  
{  
	Console.Log(i);  
	ConsoleProgressBar.Update(i, 70);  
	System.Threading.Thread.Sleep(25);  
}
// don't keep the progress bar at bottom of console
Console.AtBottomLog(null);
```

# Use with ASP.NET
For using Log73 with ASP.NET you need to install the [Log73.Extensions.Logging](https://nuget.org/packages/Log73.Extensions.Logging/) package.
Now you only need to add it in your  `CreateHostBuilder` method like so:
```csharp
using Log73.Extensions.Logging;

public static IHostBuilder CreateHostBuilder(string[] args) =>
	Host.CreateDefaultBuilder(args)
		.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>(); })
		// Add Log73 logging
		.ConfigureLogging(builder =>
			{
				builder.ClearProviders();
				builder.AddLog73Logger();
            });
```
Currently the ASP.NET logger forces `Log73.LogLevel.Error`, since configuring the LogLevel is done by configuration in the `AddLog73Logger` method like so:
```csharp
builder.AddLog73Logger(config =>
	{
		config.LogLevel = Microsoft.Extensions.Logging.LogLevel.Debug;
		// + configure the MessageTypes used by the logger!
		foreach(var msgType in config.MessageTypesAsArray())
		{
			msgType.Style = new() { ToUpper = false };
		}
	});
```