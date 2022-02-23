namespace CodeGenerator;

public class GeneratorManager
{
    private ArgumentsHandler? _argumentsHandler;
    private LanguageGenerator? _generator;
    private Parser? _parser;

    public GeneratorManager(string[] args)
    {
        StartArgumentsHandler(args);
        if (_argumentsHandler!.HasError) return;
        StartParser();
        if (_parser!.HasError) return;
        StartGenerator();
        GenerateFile();
    }

    private void StartArgumentsHandler(string[] args)
    {
        _argumentsHandler = new ArgumentsHandler(args);
        if (_argumentsHandler.HasError)
        {
            Terminal.PrintArgumentsError();
        }
    }

    private void StartParser()
    {
        foreach (var argument in _argumentsHandler!.GetArguments.Where(argument =>
                     argument.Key.Equals("-f") || argument.Key.Equals("--file")))
        {
            _parser = new Parser(argument.Value!);
        }

        if (_parser!.HasError)
        {
            Terminal.PrintParserError();
        }
    }

    private void StartGenerator()
    {
        var classNames = GetArgumentsNames();
        var arguments = _argumentsHandler!.GetArguments;
        foreach (var argument in
                 arguments.Where(argument => argument.Key.Equals("-c") || argument.Key.Equals("--code")))
        {
            if (argument.Value!.Equals("csharp"))
            {
                _generator = new CSharpLanguageGenerator(_parser!.Root, classNames);
                return;
            }

            _generator = new SwiftLanguageGenerator(_parser!.Root, classNames);
            return;
        }

        _generator = new CSharpLanguageGenerator(_parser!.Root, classNames);
    }

    private void GenerateFile()
    {
        var unused = new FileGenerator(_generator!.StringBuilder, GetArgumentsNames(), _argumentsHandler!.GetArguments);
    }

    private List<string> GetArgumentsNames()
    {
        var arguments = _argumentsHandler!.GetArguments;
        foreach (var argument in
                 arguments.Where(argument => argument.Key.Equals("-n") || argument.Key.Equals("--name")))
        {
            for (var i = argument.Value!.Length - 1; i > 0; i--)
            {
                if (argument.Value[i].Equals('.'))
                    return new List<string>
                    {
                        argument.Value[..i], argument.Value[(i + 1)..]
                    };
            }
        }

        return null!;
    }
}