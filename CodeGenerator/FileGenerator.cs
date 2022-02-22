using System.Text;

namespace CodeGenerator;

public class FileGenerator
{
    private readonly List<Argument> _arguments;
    private readonly StringBuilder _stringBuilder;

    public FileGenerator(StringBuilder stringBuilder, List<Argument> arguments)
    {
        _arguments = arguments;
        _stringBuilder = stringBuilder;
        HandleVerbose();
        CreateFile();
    }

    private void HandleVerbose()
    {
        foreach (var unused in _arguments.Where(argument => argument.Key.Equals("-v")))
        {
            Console.WriteLine(_stringBuilder);
        }
    }

    private void CreateFile()
    {
    }
}