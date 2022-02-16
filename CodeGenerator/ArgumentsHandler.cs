using System.Data;

namespace CodeGenerator;

public class ArgumentsHandler
{
    private string[] _args;
    private string[] _filteredArgs;

    public ArgumentsHandler(string[] args)
    {
        _args = args;
        _filteredArgs = new string[4];
        OrganizeArguments();
        ValidateArguments();
    }

    public bool HasError { get; set; }

    private void OrganizeArguments()
    {
        if (_args.Length == 0) {
            HasError = true;
        }
        else {
            for (var i = 0; i < _args.Length; i++) {
                switch (_args[i]) {
                    case "-n":
                    case "--name":
                        _filteredArgs[1] = _args[i + 1];
                        break;
                    case "-f":
                    case "--file":
                        _filteredArgs[2] = _args[i + 1];
                        break;
                    case "-c":
                    case "--code":
                        _filteredArgs[3] = _args[i + 1];
                        break;
                    case "-v":
                    case "--verbose":
                        _filteredArgs[4] = "-v";
                        break;
                }
            }
        }
    }

    private void ValidateArguments()
    {
        if (_filteredArgs[1] == null)
        {
            Console.WriteLine("salut");
        }
    }
}