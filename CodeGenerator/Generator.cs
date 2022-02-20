using System.Text;
using System.Text.Json;

namespace CodeGenerator;

public class Generator
{
    private StringBuilder _stringBuilder;

    public Generator(JsonElement jsonElement)
    {
        _stringBuilder = new StringBuilder();
        ReadJson(jsonElement);
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
                    ReadJson(prop.Value);
                    break;
                case "Array":
                    BuildArray(prop);
                    break;
                default:
                    BuildProperty(prop);
                    break;
            }
        }
    }

    private void BuildObject(JsonProperty jsonObject)
    {
        var objectName = FirstCharToUpper(jsonObject.Name);
        _stringBuilder.AppendLine($"[JsonProperty(\"{jsonObject.Name}\")]");
        _stringBuilder.Append($"public {objectName} {objectName} ");
        _stringBuilder.AppendLine("{ get; set; }\n");
    }

    private void BuildArray(JsonProperty array)
    {
        var arrayName = FirstCharToUpper(array.Name);
        var arrayElement = array.Value.EnumerateArray().First();
        _stringBuilder.AppendLine($"[JsonProperty(\"{array.Name}\")]");
        _stringBuilder.Append(arrayElement.ValueKind.ToString().Equals("Object")
            ? $"public List<{arrayName}> {arrayName} "
            : $"public List<{arrayElement.ValueKind.ToString().ToLower()}> {arrayName} ");
        _stringBuilder.AppendLine("{ get; set; }\n");
    }

    private void BuildProperty(JsonProperty jsonProperty)
    {
        var propertyName = FirstCharToUpper(jsonProperty.Name);
        _stringBuilder.AppendLine($"[JsonProperty(\"{jsonProperty.Name}\")]");
        _stringBuilder.Append(jsonProperty.Value.ValueKind.ToString().Equals("Number")
            ? $"public long {propertyName} "
            : $"public {jsonProperty.Value.ValueKind.ToString().ToLower()} {propertyName} ");
        _stringBuilder.AppendLine("{ get; set; }\n");
    }

    private static string FirstCharToUpper(string name)
    {
        return char.ToUpper(name[0]) + name[1..];
    }
}