namespace GlobalSolution.Model
{
    public class Alerta : IRegistroDeOcorrencia
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime DataHora { get; set; } = DateTime.Now;
        public string Local { get; set; }
        public string Tipo { get; set; } // "Perigo" ou "Resolvido"
        public string Descricao { get; set; }
        public string TecnicoId { get; set; }
    }
}

