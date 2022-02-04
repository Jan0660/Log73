using Log73.Markan;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Log73.Tests;

[TestClass]
public class MarkanTests
{
    [TestMethod]
    public void Italic()
        => Assert.AreEqual($"{AnsiCodes.Italic}italic{AnsiCodes.ItalicOff}", MarkanRenderer.Render("*italic*"));

    [TestMethod]
    public void Bold()
        => Assert.AreEqual($"{AnsiCodes.Bold}bold{AnsiCodes.NormalIntensity}", MarkanRenderer.Render("**bold**"));

    [TestMethod]
    public void BoldAndItalic()
        => Assert.AreEqual(
            $"{AnsiCodes.Bold}{AnsiCodes.Italic}bold and italic{AnsiCodes.NormalIntensity}{AnsiCodes.ItalicOff}",
            MarkanRenderer.Render("***bold and italic***"));

    [TestMethod]
    public void Underline()
        => Assert.AreEqual($"{AnsiCodes.Underline}underline{AnsiCodes.UnderlineOff}",
            MarkanRenderer.Render("__underline__"));

    [TestMethod]
    public void Strikethrough()
        => Assert.AreEqual($"{AnsiCodes.Strikethrough}strikethrough{AnsiCodes.StrikethroughOff}",
            MarkanRenderer.Render("~~strikethrough~~"));

    [TestMethod]
    public void Hyperlink()
        => Assert.AreEqual("\u001b]8;;https://example.com\u001b\\hyperlink\u001b]8;;\u001b\\",
            MarkanRenderer.Render("[hyperlink](https://example.com)"));
}