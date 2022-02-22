using System.Text;

namespace CodeGenerator;

public class FileGenerator
{
    private readonly List<string> _argumentNames;
    private readonly List<Argument> _arguments;
    private readonly StringBuilder _stringBuilder;

    public FileGenerator(StringBuilder stringBuilder, List<string> argumentNames, List<Argument> arguments)
    {
        _argumentNames = argumentNames;
        _arguments = arguments;
        _stringBuilder = stringBuilder;
        HandleVerbose();
        CreateFile();
    }

    private void HandleVerbose()
    {
        foreach (var unused in _arguments.Where(argument => argument.Key.Equals("-v")))
            Terminal.PrintFile(_stringBuilder.ToString());
    }

    private void CreateFile()
    {
        foreach (var argument in
                 _arguments.Where(argument => argument.Key.Equals("-c") || argument.Key.Equals("--code")))
        {
            if (argument.Value!.Equals("csharp"))
            {
                File.WriteAllText($"{_argumentNames[1]}.cs", _stringBuilder.ToString());
                return;
            }

            File.WriteAllText($"{_argumentNames[1]}.swift", _stringBuilder.ToString());
            return;
        }

        File.WriteAllText($"{_argumentNames[1]}.cs", _stringBuilder.ToString());
    }
}