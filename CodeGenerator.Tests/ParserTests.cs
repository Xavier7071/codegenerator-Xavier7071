using Xunit;

namespace CodeGenerator.Tests;

public class ParserTests
{
    [Fact]
    public void ValidFile()
    {
        var parser = new Parser("fa.json");
        Assert.False(parser.HasError);
    }

    [Fact]
    public void InvalidFile()
    {
        var parser = new Parser("test.json");
        Assert.True(parser.HasError);
    }
}