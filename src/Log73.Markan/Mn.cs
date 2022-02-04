namespace Log73.Markan;

public struct Mn
{
    public string Rendered { get; }

    public Mn(string content)
    {
        Rendered = MarkanRenderer.Render(content);
    }

    public override string ToString()
        => Rendered;
}