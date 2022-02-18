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
        if (argumentsHandler.HasError)
        {
            PrintError();
            PrintMandatoryParameters();
            PrintOptionalParameters();
        }
        else
        {
            
        }
    }

    private static void PrintError()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("ERREUR, veuillez entrer les paramètres de la ligne de commande de la bonne façon");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\nComment utiliser les paramètres de la ligne de commande ?");
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
        Console.WriteLine("Langages offerts: csharp");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("-f, --file <json_file_name>");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Si présent, affichera le code généré dans la console");
        Console.ForegroundColor = ConsoleColor.White;
    }
}