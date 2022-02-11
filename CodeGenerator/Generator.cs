using System.Text.Json;

namespace CodeGenerator;

public class Generator
{
    public Generator(string jsonPath)
    {
        var json = JsonSerializer.Deserialize<object>(jsonPath);
        //fonction pour parse + valider avec erreurs
    }
}