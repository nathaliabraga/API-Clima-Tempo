using ClimaSprint.Domain.Entities;

namespace ClimaSprint.Domain.Interfaces
{
    public interface IClimaService
    {
        Task<Endereco?> ObterEnderecoPorCepAsync(string cep);
    }
}
