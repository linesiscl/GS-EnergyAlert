
namespace GlobalSolution.Model
{
    public class FalhaDeEnergia
    {
        public DateTime DataHora { get; set; }
        public string Local { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public string IdCidadao { get; set; }
        public string TecnicoResponsavelId { get; set; }  // ID do técnico atribuído
    }


}
