using GlobalSolution.Model;
using System.Text.Json;

public class CadastroOuLogin
{
    private List<Cidadao> cidadaos = new();
    private List<Tecnico> tecnicos = new();
    private List<Administrador> administradores = new();

    private readonly string caminhoBase = @"C:\Users\Aline\Desktop\facul\3ESR\c#\GlobalSolution\GlobalSolution\Data\";

    private readonly string caminhoCidadaos;
    private readonly string caminhoTecnicos;
    private readonly string caminhoAdmin;


    public CadastroOuLogin()
    {
        caminhoCidadaos = Path.Combine(caminhoBase, "cidadaos.json");
        caminhoTecnicos = Path.Combine(caminhoBase, "tecnicos.json");
        caminhoAdmin = Path.Combine(caminhoBase, "admin.json");

        CarregarUsuarios();
        CarregarAdministradores();
    }

    public void CadastrarCidadao(Cidadao c)
    {
        var idsExistentes = cidadaos.Select(u => u.Id).ToList();
        c.GerarIdUnico(idsExistentes);
        cidadaos.Add(c);
        SalvarUsuarios();
    }

    public void CadastrarTecnico(Tecnico t)
    {
        var idsExistentes = tecnicos.Select(u => u.Id).ToList();
        t.GerarIdUnico(idsExistentes);
        tecnicos.Add(t);
        SalvarUsuarios();
    }

    public Cidadao? LoginCidadao(string nome, string sobrenome, string senha)
    {
        return cidadaos.FirstOrDefault(c =>
            c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase) &&
            c.Sobrenome.Equals(sobrenome, StringComparison.OrdinalIgnoreCase) &&
            c.Senha == senha);
    }

    public Tecnico? LoginTecnico(string nome, string sobrenome, string senha)
    {
        return tecnicos.FirstOrDefault(t =>
            t.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase) &&
            t.Sobrenome.Equals(sobrenome, StringComparison.OrdinalIgnoreCase) &&
            t.Senha == senha);
    }

    public bool LoginAdministrador(string usuario, string senha)
    {
        return administradores.Any(a =>
            a.Usuario.Equals(usuario, StringComparison.OrdinalIgnoreCase) &&
            a.Senha == senha);
    }

    public List<Tecnico> ListarTecnicos() => tecnicos;

    private void CarregarUsuarios()
    {
        if (File.Exists(caminhoCidadaos))
        {
            var json = File.ReadAllText(caminhoCidadaos);
            cidadaos = JsonSerializer.Deserialize<List<Cidadao>>(json) ?? new();
        }

        if (File.Exists(caminhoTecnicos))
        {
            var json = File.ReadAllText(caminhoTecnicos);
            tecnicos = JsonSerializer.Deserialize<List<Tecnico>>(json) ?? new();
        }
    }

    private void CarregarAdministradores()
    {
        if (File.Exists(caminhoAdmin))
        {
            var json = File.ReadAllText(caminhoAdmin);
            administradores = JsonSerializer.Deserialize<List<Administrador>>(json) ?? new();
        }
        else
        {
            // Cria um administrador padrão se o arquivo não existir
            administradores = new List<Administrador>
        {
            new Administrador { Usuario = "admin", Senha = "admin123" }
        };

            // Garante que a pasta Data exista
            Directory.CreateDirectory(Path.GetDirectoryName(caminhoAdmin)!);

            File.WriteAllText(caminhoAdmin,
                JsonSerializer.Serialize(administradores, new JsonSerializerOptions { WriteIndented = true }));
        }
    }

    private void SalvarUsuarios()
    {
        Directory.CreateDirectory("Data");

        var cidadaoJson = JsonSerializer.Serialize(cidadaos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(caminhoCidadaos, cidadaoJson);

        var tecnicoJson = JsonSerializer.Serialize(tecnicos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(caminhoTecnicos, tecnicoJson);
    }
}
