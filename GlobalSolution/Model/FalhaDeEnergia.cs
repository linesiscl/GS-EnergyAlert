
namespace GlobalSolution.Model
{
    public class FalhaDeEnergia : IRegistroDeOcorrencia
    {
        public DateTime DataHora { get; set; }
        public string Local { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public string IdCidadao { get; set; }
        public string TecnicoId { get; set; }
    }
}

