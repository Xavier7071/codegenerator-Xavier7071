using Xunit;

namespace CodeGenerator.Tests;

public class GeneratorTests
{
    [Fact]
    public void ValidFile()
    {
        var parser = new Parser("fa.json");
        Assert.False(parser.HasError);
    }
}