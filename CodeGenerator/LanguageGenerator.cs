using System.Text;
using System.Text.Json;

namespace CodeGenerator;

public abstract class LanguageGenerator
{
    private readonly List<string> _savedObjects;
    private JsonElement _savedJsonElement;
    protected List<string>? ArgumentNames;

    protected LanguageGenerator()
    {
        _savedObjects = new List<string>();
        StringBuilder = new StringBuilder();
    }

    public StringBuilder StringBuilder { get; }
    protected abstract void BuildHeader();
    protected abstract void BuildObject(JsonProperty jsonObject);
    protected abstract void BuildClass(JsonProperty jsonObject);
    protected abstract void BuildProperty(JsonProperty jsonProperty);
    protected abstract void BuildArray(JsonProperty array);
    protected abstract void BuildFooter();

    protected void StartGenerator(JsonElement jsonElement)
    {
        _savedJsonElement = jsonElement;
        BuildHeader();
        ReadJson(_savedJsonElement);
        BuildFooter();
    }

    protected static string FirstCharToUpper(string name)
    {
        return char.ToUpper(name[0]) + name[1..];
    }

    private void ReadJson(JsonElement jsonElement)
    {
        var props = new List<string>();
        if (_savedObjects.Count == 0)
        {
            _savedJsonElement = jsonElement;
        }

        foreach (var prop in jsonElement.EnumerateObject().Where(prop => !props.Contains(prop.Name)))
        {
            props.Add(prop.Name);
            DeterminePropType(prop);
        }

        CheckForOtherClasses();
    }

    private void DeterminePropType(JsonProperty prop)
    {
        switch (prop.Value.ValueKind)
        {
            case JsonValueKind.Object:
                BuildObject(prop);
                _savedObjects.Add(prop.Name);
                break;
            case JsonValueKind.Array:
                BuildArray(prop);
                BuildArrayOfObjects(prop);
                break;
            default:
                BuildProperty(prop);
                break;
        }
    }

    private void CheckForOtherClasses()
    {
        foreach (var savedObject in _savedObjects)
        {
            foreach (var prop in _savedJsonElement.EnumerateObject().Where(prop => savedObject.Equals(prop.Name)))
            {
                _savedObjects.Remove(savedObject);
                BuildNewClass(prop);
                return;
            }
        }
    }

    private void BuildNewClass(JsonProperty prop)
    {
        BuildClass(prop);
        ReadJson(prop.Value.ValueKind.ToString().Equals("Array") ? prop.Value.EnumerateArray().First() : prop.Value);
    }

    private void BuildArrayOfObjects(JsonProperty array)
    {
        var arrayElement = array.Value.EnumerateArray().First();
        if (arrayElement.ValueKind == JsonValueKind.Object) _savedObjects.Add(array.Name);
    }
}