namespace Log73;

public ref struct SpanStringBuilder
{
    public ushort Position;
    public Span<char> Buffer;

    public SpanStringBuilder(ref Span<char> buffer)
    {
        Buffer = buffer;
        Position = 0;
    }

    public void Append(in ReadOnlySpan<char> value)
    {
        value.CopyTo(Buffer[Position..]);
        Position += (ushort)value.Length;
    }
    public void Append(char value)
        => Buffer[Position++] = value;

    // todo: for numbers and ISpanFormattable
    public void AppendNumber(byte value, in ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
    {
        value.TryFormat(Buffer[Position..], out var charsWritten, format, provider);
        Position += (ushort)charsWritten;
    }

    public override string ToString() => new(Buffer.Slice(0, Position));

    public void AppendDateTime(in DateTime time, in ReadOnlySpan<char> format)
    {
        time.TryFormat(Buffer[Position..], out var charsWritten, format);
        Position += (ushort)charsWritten;
    }
}