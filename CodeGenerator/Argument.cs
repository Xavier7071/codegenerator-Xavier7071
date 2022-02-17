namespace CodeGenerator;

public class Argument
{
    public string key;
    public string? value;

    public Argument(string key, string? value)
    {
        this.key = key;
        this.value = value;
    }
}