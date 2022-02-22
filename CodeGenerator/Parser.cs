using System.Text.Json;

namespace CodeGenerator;

public class Parser
{
    private readonly string _path;

    public Parser(string filePath)
    {
        _path = filePath;
        ValidateFile();
        if (!HasError) ParseJson();
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
        catch (Exception)
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