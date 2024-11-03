namespace ClimaSprint.Domain.Interfaces.Dtos
{
    public interface IClimaDto
    {
        string Cidade { get; set; }
        DateTime DataConsulta { get; set; }
        double Temperatura { get; set; }
        string Condicao { get; set; }
        void Validate();
    }
}
