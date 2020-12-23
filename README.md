# Log73

#### A powerful and customizable .NET logging library

## Basic example

```csharp
using Console = Log73.Console;
using Log73;

Console.Options.LogLevel = LogLevel.Debug;
Console.Log("You can");
MessageTypes.Error.Style.Invert = true;
MessageTypes.Error.ExtraInfo.Add(new TimeExtraInfo());
Console.Error("log customized messages");
Console.Warn("with Log73!");
Console.ObjectYaml(new { AndAlso = "Log objects as Json, Xml or Yaml!" });
```

![output](https://i.imgur.com/AI3b8Lk.png)

## LogTypes

There are 4 LogTypes: Info, Warn, Error, Debug. Which look like this by default:

![conhost](https://i.imgur.com/ynya3z8.png)

## Styling

You can also customize the default styles for the log types, by changing color or enabling ANSI escape codes. (all of the colors/ANSI codes might not be supported in all terminals)

```csharp
Console.Error("No styles :(");
MessageTypes.Error.Style.Invert = true;
MessageTypes.Error.ContentStyle.Color = System.Drawing.Color.Orange;
MessageTypes.Error.ContentStyle.Italic = true;
MessageTypes.Error.ContentStyle.Underline = true;
Console.Error("Look at all this styling!");
```



Here you can see an example of styling, and also how Windows 10's conhost doesn't support italic.

![conhost styling](https://i.imgur.com/L6j0HIr.png)

While [Alacritty](https://github.com/alacritty/alacritty) supports it: (and also does weird stuff with the colors)

![styling alacritty](https://i.imgur.com/lHP0Tsw.png)

## Message Types

Log73 currently comes with 6 message types: Info, Warn, Error, Debug, Start, Done. All of them except Start and Done are used by their corresponding `Console.X(...)`.

The Start and Done message types are used for the `Console.Task(taskName, task)` method, which looks like this by default with the following code:

```csharp
Console.Options.AlwaysLogTaskStart = true;
Console.Task("TestSuccessTask", TestSuccessTask());
Console.Task("TestErrorTask", TestErrorTask());
```

![msgtype](https://i.imgur.com/pR63J3H.png)

If you like the words "Begin" and "Finished" more you can change them!

```csharp
Console.Options.AlwaysLogTaskStart = true;
MessageTypes.Start.Name = "Begin";
MessageTypes.Done.Name = "Finished";
Console.Task("TestSuccessTask", TestSuccessTask());
Console.Task("TestErrorTask", TestErrorTask());
```

![msgtype](https://i.imgur.com/T21fobE.png)

