﻿namespace CodeGenerator;

public class ArgumentsHandler
{
    private string[] _args;
    private string[] _allArgs = {"-n", "--name", "-f", "--file", "-c", "--code", "-v", "--verbose"};
    private List<Argument> _arguments;

    public ArgumentsHandler(string[] args)
    {
        _args = args;
        _arguments = new List<Argument>();
        StartArgumentsHandler();
    }

    public bool HasError { get; set; }

    private void StartArgumentsHandler()
    {
        if (_args.Length == 0)
        {
            HasError = true;
        }
        else
        {
            OrganizeArguments();
            ValidateMandatoryArguments();
            if (HasError)
            {
                Console.WriteLine("erreur");
            }
            else
            {
                Console.WriteLine("toute est beau");
            }
        }
    }

    private void OrganizeArguments()
    {
        for (var i = 0; i < _args.Length; i++)
        {
            foreach (var arg in _allArgs)
            {
                if (!_args[i].Equals(arg)) continue;
                if (_args[i].Equals("-v") || _args[i].Equals("--verbose"))
                {
                    AddArgument("-v", null);
                }
                else
                {
                    ValidateArgument(i);
                    ValidateCode(i);
                    if (!HasError)
                    {
                        AddArgument(_args[i], _args[i + 1]);
                    }
                }
            }
        }
    }

    private void ValidateArgument(int i)
    {
        if (_args.Length <= i + 1)
        {
            HasError = true;
        }
        else if (_args[i + 1].Equals(null))
        {
            HasError = true;
        }
        else
        {
            foreach (var arg in _allArgs)
            {
                if (_args[i + 1].Equals(arg))
                {
                    HasError = true;
                }
            }
        }
    }

    private void ValidateCode(int i)
    {
        if (!_args[i].Equals("-c") && !_args[i].Equals("--code") || HasError) return;
        if (!_args[i + 1].Equals("csharp"))
        {
            HasError = true;
        }
    }

    private void AddArgument(string key, string? value)
    {
        _arguments.Add(new Argument(key, value));
    }

    private void ValidateMandatoryArguments()
    {
        var hasName = false;
        var hasFile = false;
        foreach (var argument in _arguments)
        {
            switch (argument.key)
            {
                case "-n":
                case "--name":
                    hasName = true;
                    break;
                case "-f":
                case "--file":
                    hasFile = true;
                    break;
            }
        }

        if (!hasName || !hasFile)
        {
            HasError = true;
        }
    }
}