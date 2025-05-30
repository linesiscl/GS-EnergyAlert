using GlobalSolution.Model;
using System.Text.Json;

public class ServicoDeAlerta
{
    private List<Alerta> alertas = new();
    private readonly string caminhoAlertas = "C:\\Users\\Aline\\Desktop\\facul\\3ESR\\c#\\GlobalSolution\\GlobalSolution\\Data\\alertas.json";

    public ServicoDeAlerta()
    {
        Carregar();
    }

    public void AdicionarAlerta(Alerta alerta)
    {
        alertas.Add(alerta);
        Salvar();
    }

    public List<Alerta> ListarAlertas()
    {
        return alertas;
    }

    private void Carregar()
    {
        if (File.Exists(caminhoAlertas))
        {
            var json = File.ReadAllText(caminhoAlertas);
            alertas = JsonSerializer.Deserialize<List<Alerta>>(json) ?? new();
        }
    }

    private void Salvar()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(caminhoAlertas));
        File.WriteAllText(caminhoAlertas, JsonSerializer.Serialize(alertas, new JsonSerializerOptions { WriteIndented = true }));
    }
}

