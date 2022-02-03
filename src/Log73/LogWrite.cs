namespace Log73;

public delegate void LogWrite(in ReadOnlySpan<char> message, in LogContext context);