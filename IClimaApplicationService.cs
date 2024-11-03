using ClimaSprint.Domain.Entities;
using ClimaSprint.Domain.Interfaces.Dtos;

namespace ClimaSprint.Domain.Interfaces
{
    public interface IClimaApplicationService
    {
        IEnumerable<ClimaEntity> ObterTodosClimas();
        ClimaEntity ObterClimaPorId(int id);
        ClimaEntity AdicionarClima(IClimaDto dto);
        ClimaEntity EditarClima(int id, IClimaDto dto);
        ClimaEntity RemoverClima(int id);
        Task<Endereco?> ObterEnderecoPorCepAsync(string cep);
    }
}
