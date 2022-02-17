namespace CodeGenerator;

public class Generator
{
    public Generator(string[] args)
    {
        StartGenerator(args);
    }

    private void StartGenerator(string[] args)
    {
        var argumentsHandler = new ArgumentsHandler(args);
    }
}