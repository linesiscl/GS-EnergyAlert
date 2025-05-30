namespace GlobalSolution.Model
{
    public interface IUsuario
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Senha { get; set; }
        public string Id { get; set; }

        void GerarIdUnico(IEnumerable<string> idsExistentes);
    }
}

