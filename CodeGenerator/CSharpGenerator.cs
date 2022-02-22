using System.Text.Json;

namespace CodeGenerator;

public class CSharpLanguageGenerator : LanguageGenerator
{
    public CSharpLanguageGenerator(JsonElement jsonElement)
    {
        StartGenerator(jsonElement, "public partial MyClass Object\n{");
    }

    protected override void BuildObject(JsonProperty jsonObject)
    {
        var objectName = FirstCharToUpper(jsonObject.Name);
        StringBuilder.AppendLine($"\n    [JsonProperty(\"{jsonObject.Name}\")]");
        StringBuilder.Append($"    public {objectName} {objectName} ");
        StringBuilder.AppendLine("{ get; set; }");
    }

    protected override void BuildClass(JsonProperty jsonObject)
    {
        var objectName = FirstCharToUpper(jsonObject.Name);
        StringBuilder.AppendLine("}\n");
        StringBuilder.AppendLine($"public partial class {objectName}");
        StringBuilder.Append('{');
    }

    protected override void BuildProperty(JsonProperty jsonProperty)
    {
        var propertyName = FirstCharToUpper(jsonProperty.Name);
        StringBuilder.AppendLine($"\n    [JsonProperty(\"{jsonProperty.Name}\")]");
        StringBuilder.Append(jsonProperty.Value.ValueKind.ToString().Equals("Number")
            ? $"    public long {propertyName} "
            : $"    public {jsonProperty.Value.ValueKind.ToString().ToLower()} {propertyName} ");
        StringBuilder.AppendLine("{ get; set; }");
    }

    protected override void BuildArray(JsonProperty array)
    {
        var arrayName = FirstCharToUpper(array.Name);
        var arrayElement = array.Value.EnumerateArray().First();
        StringBuilder.AppendLine($"\n    [JsonProperty(\"{array.Name}\")]");
        switch (arrayElement.ValueKind.ToString())
        {
            case "Object":
                StringBuilder.Append($"    public List<{arrayName}> {arrayName} ");
                break;
            case "Number":
                StringBuilder.Append($"    public List<long> {arrayName} ");
                break;
            default:
                StringBuilder.Append($"    public List<{arrayElement.ValueKind.ToString().ToLower()}> {arrayName} ");
                break;
        }

        StringBuilder.AppendLine("{ get; set; }");
    }
}