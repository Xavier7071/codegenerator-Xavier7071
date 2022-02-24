using System.Text.Json;

namespace CodeGenerator;

public class SwiftLanguageGenerator : LanguageGenerator
{
    public SwiftLanguageGenerator(JsonElement jsonElement, List<string> argumentNames)
    {
        ArgumentNames = argumentNames;
        StartGenerator(jsonElement);
    }

    protected override void BuildHeader()
    {
        StringBuilder.AppendLine("import Foundation\n");
        StringBuilder.AppendLine($"struct {ArgumentNames![1]}: Codable ");
        StringBuilder.Append("{\n");
    }

    protected override void BuildObject(JsonProperty jsonObject)
    {
        var objectName = FirstCharToUpper(jsonObject.Name);
        StringBuilder.AppendLine($"    let {jsonObject.Name}: {objectName} ");
    }

    protected override void BuildClass(JsonProperty jsonObject)
    {
        var objectName = FirstCharToUpper(jsonObject.Name);
        StringBuilder.AppendLine("}\n");
        StringBuilder.Append($"struct {objectName}: Codable ");
        StringBuilder.AppendLine("{");
    }

    protected override void BuildProperty(JsonProperty jsonProperty)
    {
        StringBuilder.AppendLine(jsonProperty.Value.ValueKind == JsonValueKind.Number
            ? $"    let {jsonProperty.Name}: Int"
            : $"    let {jsonProperty.Name}: {jsonProperty.Value.ValueKind.ToString()}");
    }

    protected override void BuildArray(JsonProperty array)
    {
        var arrayName = FirstCharToUpper(array.Name);
        var arrayElement = array.Value.EnumerateArray().First();
        switch (arrayElement.ValueKind)
        {
            case JsonValueKind.Object:
                StringBuilder.AppendLine($"    let {array.Name}: [{arrayName}]");
                break;
            case JsonValueKind.Number:
                StringBuilder.AppendLine($"    let {array.Name}: [Int]");
                break;
            default:
                StringBuilder.AppendLine($"    let {array.Name}: [{arrayElement.ValueKind.ToString()}]");
                break;
        }
    }

    protected override void BuildFooter()
    {
        StringBuilder.Append('}');
    }
}