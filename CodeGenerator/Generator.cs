using System.Collections;
using System.Text;
using System.Text.Json;

namespace CodeGenerator;

public class Generator
{
    private StringBuilder _stringBuilder;
    private List<string> savedObjects;

    public Generator(JsonElement jsonElement)
    {
        savedObjects = new List<string>();
        _stringBuilder = new StringBuilder();
        _stringBuilder.Append("public partial class Object\n{");
        ReadJson(jsonElement);
        _stringBuilder.Append('}');
        Console.WriteLine(_stringBuilder);
    }

    private void ReadJson(JsonElement jsonElement)
    {
        foreach (var prop in jsonElement.EnumerateObject())
        {
            switch (prop.Value.ValueKind.ToString())
            {
                case "Object":
                    BuildObject(prop);
                    savedObjects.Add(prop.Name);
                    break;
                case "Array":
                    BuildArray(prop);
                    BuildArrayOfObjects(prop);
                    break;
                default:
                    BuildProperty(prop);
                    break;
            }
        }

        CheckForOtherClasses(jsonElement);
    }

    private void CheckForOtherClasses(JsonElement jsonElement)
    {
        foreach (var savedObject in savedObjects)
        {
            foreach (var prop in jsonElement.EnumerateObject())
            {
                if (prop.Name.Equals(savedObject))
                {
                    savedObjects.Remove(savedObject);
                    BuildClass(prop);
                    ReadJson(prop.Value);
                    return;
                }
            }
        }
    }

    private void BuildObject(JsonProperty jsonObject)
    {
        var objectName = FirstCharToUpper(jsonObject.Name);
        _stringBuilder.AppendLine($"\n[JsonProperty(\"{jsonObject.Name}\")]");
        _stringBuilder.Append($"public {objectName} {objectName} ");
        _stringBuilder.AppendLine("{ get; set; }");
    }

    private void BuildClass(JsonProperty jsonObject)
    {
        var objectName = FirstCharToUpper(jsonObject.Name);
        _stringBuilder.AppendLine("}\n");
        _stringBuilder.AppendLine($"public partial class {objectName}");
        _stringBuilder.Append('{');
    }

    private void BuildArray(JsonProperty array)
    {
        var arrayName = FirstCharToUpper(array.Name);
        var arrayElement = array.Value.EnumerateArray().First();
        _stringBuilder.AppendLine($"\n[JsonProperty(\"{array.Name}\")]");
        switch (arrayElement.ValueKind.ToString())
        {
            case "Object":
                _stringBuilder.Append($"public List<{arrayName}> {arrayName} ");
                break;
            case "Number":
                _stringBuilder.Append($"public List<long> {arrayName} ");
                break;
            default:
                _stringBuilder.Append($"public List<{arrayElement.ValueKind.ToString().ToLower()}> {arrayName} ");
                break;
        }

        _stringBuilder.AppendLine("{ get; set; }");
    }

    private void BuildArrayOfObjects(JsonProperty array)
    {
        var arrayElement = array.Value.EnumerateArray().First();
        if (arrayElement.ValueKind.ToString().Equals("Object"))
        {
        }
    }

    private void BuildProperty(JsonProperty jsonProperty)
    {
        var propertyName = FirstCharToUpper(jsonProperty.Name);
        _stringBuilder.AppendLine($"\n[JsonProperty(\"{jsonProperty.Name}\")]");
        _stringBuilder.Append(jsonProperty.Value.ValueKind.ToString().Equals("Number")
            ? $"public long {propertyName} "
            : $"public {jsonProperty.Value.ValueKind.ToString().ToLower()} {propertyName} ");
        _stringBuilder.AppendLine("{ get; set; }");
    }

    private static string FirstCharToUpper(string name)
    {
        return char.ToUpper(name[0]) + name[1..];
    }
}