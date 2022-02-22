namespace CodeGenerator;

public static class Terminal
{
    public static void PrintArgumentsError()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("ERREUR, veuillez entrer les paramètres de la ligne de commande de la bonne façon");
        PrintParameters();
    }

    public static void PrintParserError()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("ERREUR, le processus pour lire le fichier JSON a échoué");
        Console.WriteLine("Veuillez vérifier l'existence du fichier et l'état du JSON");
        PrintParameters();
    }

    public static void PrintFile(string fileContent)
    {
        Console.WriteLine(fileContent);
    }

    private static void PrintParameters()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\nComment utiliser les paramètres de la ligne de commande ?");
        PrintMandatoryParameters();
        PrintOptionalParameters();
    }

    private static void PrintMandatoryParameters()
    {
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
        Console.WriteLine("Langages offerts: csharp, swift");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("-v, --verbose");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Si présent, affichera le code généré dans la console");
        Console.ForegroundColor = ConsoleColor.White;
    }
}