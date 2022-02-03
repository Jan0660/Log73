using System.Runtime.InteropServices;
using Log73.Extensible;

namespace Log73;

public static class IncludedExtensions
{
    [DllImport("kernel32.dll")]
    static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll")]
    static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);
    /// <summary>
    /// Enables VirtualTerminalProcessing for the console on Windows. On other platforms it does nothing.
    /// </summary>
    public static void EnableVirtualTerminalProcessing(this LoggerConfigureExtensible _)
    {
        if(!OperatingSystem.IsWindows())
            return;
        var handle = GetStdHandle(-11);
        GetConsoleMode(handle, out var mode);
        mode |= 0x0004;
        SetConsoleMode(handle, mode);
    }
}