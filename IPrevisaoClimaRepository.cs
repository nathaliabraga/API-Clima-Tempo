using ClimaSprint.Domain.Entities;

namespace ClimaSprint.Domain.Interfaces
{
    public interface IPrevisaoClimaRepository
    {
        PrevisaoClimaEntity? ObterPorId(int id);
        IEnumerable<PrevisaoClimaEntity>? ObterTodos();
        PrevisaoClimaEntity? Adicionar(PrevisaoClimaEntity previsao);
        PrevisaoClimaEntity? Editar(PrevisaoClimaEntity previsao);
        PrevisaoClimaEntity? Remover(int id);
    }
}
