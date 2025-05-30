namespace GlobalSolution.Model
{
    public interface IRegistroDeOcorrencia
    {
        DateTime DataHora { get; set; }
        string Local { get; set; }
        string Tipo { get; set; }
        string Descricao { get; set; }
    }
}

