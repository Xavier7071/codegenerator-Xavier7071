﻿namespace CodeGenerator;

public class Generator
{
    private Parser? _parser;
    private ArgumentsHandler? _argumentsHandler;

    public Generator(string[] args)
    {
        StartArgumentsHandler(args);
        if (!_argumentsHandler!.HasError)
        {
            StartParser();
        }
    }

    private void StartArgumentsHandler(string[] args)
    {
        _argumentsHandler = new ArgumentsHandler(args);
        if (_argumentsHandler.HasError)
        {
            PrintArgumentsError();
        }
    }

    private void StartParser()
    {
        foreach (var argument in _argumentsHandler!.GetArguments)
        {
            if (argument.key.Equals("-f") || argument.key.Equals("--file"))
            {
                _parser = new Parser(argument.value!);
            }
        }

        if (_parser!.HasError)
        {
            PrintParserError();
        }
    }

    private static void PrintArgumentsError()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("ERREUR, veuillez entrer les paramètres de la ligne de commande de la bonne façon");
        PrintMandatoryParameters();
        PrintOptionalParameters();
    }

    private static void PrintParserError()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("ERREUR, le processus pour lire le fichier JSON a échoué");
        Console.WriteLine("Veuillez vérifier l'existence du fichier et l'état du JSON");
        PrintMandatoryParameters();
        PrintOptionalParameters();
    }

    private static void PrintMandatoryParameters()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\nComment utiliser les paramètres de la ligne de commande ?");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Paramètres obligatoires :");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("-n, --name <fully_qualified_class_name>");
        Console.WriteLine("-f, --file <json_file_name>");
    }

    private static void PrintOptionalParameters()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Paramètres optionnels :");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("-c, --code <lang_code>");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Si non présent, par défaut, le code sera généré en C#");
        Console.WriteLine("Langages offerts: csharp");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("-f, --file <json_file_name>");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Si présent, affichera le code généré dans la console");
        Console.ForegroundColor = ConsoleColor.White;
    }
}