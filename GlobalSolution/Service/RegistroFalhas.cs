using GlobalSolution.Model;
using System.Text.Json;

public class RegistroFalhas
{
    private List<FalhaDeEnergia> falhas = new();

    private readonly string caminhoFalhas = "C:\\Users\\Aline\\Desktop\\facul\\3ESR\\c#\\GlobalSolution\\GlobalSolution\\Data\\falhas.json";

    private CadastroOuLogin usuarioService;

    public RegistroFalhas(CadastroOuLogin usuarioService)
    {
        this.usuarioService = usuarioService;
        CarregarFalhas();
    }

    public void AdicionarFalha(FalhaDeEnergia falha)
    {
        var tecnicos = usuarioService.ListarTecnicos();
        if (tecnicos.Any())
        {
            var tecnicoCompativel = tecnicos.FirstOrDefault(t => t.Especialidade.Equals(falha.Tipo, StringComparison.OrdinalIgnoreCase));
            if (tecnicoCompativel != null)
            {
                falha.TecnicoId = tecnicoCompativel.Id;
            }
            else
            {
                var tecnicoAleatorio = tecnicos[new Random().Next(tecnicos.Count)];
                falha.TecnicoId = tecnicoAleatorio.Id;
            }
        }
        else
        {
            falha.TecnicoId = "N/A";
        }

        falhas.Add(falha);
        Salvar();
    }

    public List<FalhaDeEnergia> ListarPorCidadao(string idCidadao)
    {
        return falhas.Where(f => f.IdCidadao == idCidadao).ToList();
    }

    private void CarregarFalhas()
    {
        if (File.Exists(caminhoFalhas))
        {
            var json = File.ReadAllText(caminhoFalhas);
            falhas = JsonSerializer.Deserialize<List<FalhaDeEnergia>>(json) ?? new();
        }
        else
        {
            falhas = new();
        }
    }

    private void Salvar()
    {
        File.WriteAllText(caminhoFalhas, JsonSerializer.Serialize(falhas, new JsonSerializerOptions { WriteIndented = true }));
    }

    public List<FalhaDeEnergia> ListarPorTecnico(string tecnicoId)
    {
        return falhas.Where(f => f.TecnicoId == tecnicoId).ToList();
    }

}
