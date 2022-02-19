using Xunit;

namespace CodeGenerator.Tests;

public class ArgumentsHandlerTests
{
    [Theory]
    [InlineData("-n", "-f", "test.json", "-c", "csharp", "-v")]
    [InlineData("-f", "test.json")]
    [InlineData("-n", "class", "-f")]
    [InlineData("-n", "class", "-f", "test.json", "-c")]
    public void HandleArgumentsWithError(params string[] args)
    {
        var argumentsHandler = new ArgumentsHandler(args);
        Assert.True(argumentsHandler.HasError);
    }

    [Theory]
    [InlineData("-n", "class", "-f", "test.json", "-c", "csharp")]
    [InlineData("-f", "test.json", "-n", "class", "-v")]
    [InlineData("-v", "-c", "csharp", "-f", "test.json", "-n", "class")]
    [InlineData("-n", "class", "-f", "test.json")]
    public void HandleArgumentsWithoutError(params string[] args)
    {
        var argumentsHandler = new ArgumentsHandler(args);
        Assert.False(argumentsHandler.HasError);
    }
}