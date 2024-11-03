using ClimaSprint.Domain.Entities;
using ClimaSprint.Domain.Interfaces;
using ClimaSprint.Domain.Interfaces.Dtos;

namespace ClimaSprint.Application.Services
{
    public class PrevisaoClimaApplicationService : IPrevisaoClimaApplicationService
    {
        private readonly IPrevisaoClimaRepository _repository;
        private readonly IClimaService _climaService;

        public PrevisaoClimaApplicationService(IPrevisaoClimaRepository repository, IClimaService climaService)
        {
            _repository = repository;
            _climaService = climaService;
        }

        public PrevisaoClimaEntity? AdicionarPrevisao(IPrevisaoClimaDto dto)
        {
            var entity = new PrevisaoClimaEntity
            {
                Cidade = dto.Cidade,
                Temperatura = dto.Temperatura,
                Condicao = dto.Condicao,
                Data = dto.Data
            };
            return _repository.Adicionar(entity);
        }

        public PrevisaoClimaEntity? EditarPrevisao(int id, IPrevisaoClimaDto dto)
        {
            var entity = new PrevisaoClimaEntity
            {
                Id = id,
                Cidade = dto.Cidade,
                Temperatura = dto.Temperatura,
                Condicao = dto.Condicao,
                Data = dto.Data
            };
            return _repository.Editar(entity);
        }

        public PrevisaoClimaEntity? ObterPrevisaoPorId(int id)
        {
            return _repository.ObterPorId(id);
        }

        public IEnumerable<PrevisaoClimaEntity>? ObterTodasPrevisoes()
        {
            return _repository.ObterTodos();
        }

        public PrevisaoClimaEntity? RemoverPrevisao(int id)
        {
            return _repository.Remover(id);
        }

        public async Task<PrevisaoClimaEntity?> ObterPrevisaoAtualAsync(string cidade)
        {
            var previsao = await _climaService.ObterPrevisaoAtualAsync(cidade);
            return previsao;
        }
    }
}
