using System.Text.Json;

namespace CodeGenerator;

public class Parser
{
    private readonly string _path;

    public Parser(string filePath)
    {
        _path =
            "C:/Users/xavie/OneDrive - Cegep de Sorel-Tracy/Session 4/POO III/codegenerator-Xavier7071/CodeGenerator/" +
            filePath;
        ValidateFile();
        if (!HasError)
        {
            ParseJson();
        }
    }

    public bool HasError { get; private set; }
    public JsonElement Root { get; private set; }

    private void ValidateFile()
    {
        try
        {
            var text = File.ReadAllText(_path);
            JsonDocument.Parse(text);
        }
        catch (Exception exception)
        {
            HasError = true;
        }
    }

    private void ParseJson()
    {
        var text = File.ReadAllText(_path);
        var doc = JsonDocument.Parse(text);
        Root = doc.RootElement;
    }
}