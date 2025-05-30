
namespace GlobalSolution.Model
{
    public class Tecnico : IUsuario
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Senha { get; set; }

        public string Id => $"{Nome.ToLower()}_{Sobrenome.ToLower()}";
    }
}

