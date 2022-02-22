namespace CodeGenerator;

public class Argument
{
    public readonly string Key;
    public readonly string? Value;

    public Argument(string key, string? value)
    {
        Key = key;
        Value = value;
    }
}