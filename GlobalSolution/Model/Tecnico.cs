
namespace GlobalSolution.Model
{
    public class Tecnico : IUsuario
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Senha { get; set; }

        public string Id { get; set; }
        public string Especialidade { get; set; }

        public void GerarIdUnico(IEnumerable<string> idsExistentes)
        {
            string baseId = $"{Nome.ToLower()}_{Sobrenome.ToLower()}";
            string novoId = baseId;
            int contador = 1;

            while (idsExistentes.Contains(novoId))
            {
                novoId = $"{baseId}{contador}";
                contador++;
            }

            Id = novoId;
        }
    }
}

