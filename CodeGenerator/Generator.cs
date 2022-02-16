namespace CodeGenerator;

public class Generator
{
    private readonly string[] _args;

    public Generator(string[] args)
    {
        _args = args;
        StartGenerator();
    }

    private void StartGenerator()
    {
        var argumentsHandler = new ArgumentsHandler(_args);
    }
}