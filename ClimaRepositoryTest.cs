using ClimaSprint.Data.AppData;
using ClimaSprint.Data.Repositories;
using ClimaSprint.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClimaSprint.Tests
{
    public class PrevisaoClimaRepositoryTests
    {
        private readonly DbContextOptions<ClimaContext> _options;
        private readonly ApplicationContext _context;
        private readonly PrevisaoClimaRepository _previsaoRepository;

        public PrevisaoClimaRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ClimaContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ClimaContext(_options);
            _previsaoRepository = new PrevisaoClimaRepository(_context);
        }

        [Fact]
        public void Adicionar_DeveAdicionarPrevisaoClimaERetornarPrevisaoClimaEntity()
        {
            // Arrange
            var previsao = new PrevisaoClimaEntity { Cidade = "São Paulo", Temperatura = 25, Condicao = "Ensolarado" };

            // Act
            var resultado = _previsaoRepository.Adicionar(previsao);

            // Assert
            var previsaoNoDb = _context.PrevisoesClimaticas.FirstOrDefault(p => p.Id == resultado.Id);
            Assert.NotNull(previsaoNoDb);
            Assert.Equal(previsao.Cidade, previsaoNoDb.Cidade);
            Assert.Equal(previsao.Temperatura, previsaoNoDb.Temperatura);
            Assert.Equal(previsao.Condicao, previsaoNoDb.Condicao);
        }

        [Fact]
        public void Editar_DeveAtualizarPrevisaoClimaERetornarPrevisaoClimaEntity_QuandoPrevisaoExiste()
        {
            // Arrange
            var previsao = new PrevisaoClimaEntity { Cidade = "Rio de Janeiro", Temperatura = 30, Condicao = "Nublado" };
            _context.PrevisoesClimaticas.Add(previsao);
            _context.SaveChanges();

            previsao.Temperatura = 32;
            previsao.Condicao = "Parcialmente Nublado";

            // Act
            var resultado = _previsaoRepository.Editar(previsao);

            // Assert
            var previsaoNoDb = _context.PrevisoesClimaticas.FirstOrDefault(p => p.Id == previsao.Id);
            Assert.NotNull(previsaoNoDb);
            Assert.Equal(32, previsaoNoDb.Temperatura);
            Assert.Equal("Parcialmente Nublado", previsaoNoDb.Condicao);
        }

        [Fact]
        public void ObterPorId_DeveRetornarPrevisaoClimaEntity_QuandoPrevisaoExiste()
        {
            // Arrange
            var previsao = new PrevisaoClimaEntity { Cidade = "Salvador", Temperatura = 28, Condicao = "Chuvoso" };
            _context.PrevisoesClimaticas.Add(previsao);
            _context.SaveChanges();

            // Act
            var resultado = _previsaoRepository.ObterPorId(previsao.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(previsao.Id, resultado.Id);
            Assert.Equal(previsao.Cidade, resultado.Cidade);
        }

        [Fact]
        public void ObterTodas_DeveRetornarListaDePrevisoesClimaticas_QuandoExistiremPrevisoes()
        {
            // Arrange
            var previsoes = new List<PrevisaoClimaEntity>
            {
                new PrevisaoClimaEntity { Cidade = "Porto Alegre", Temperatura = 22, Condicao = "Ventania" },
                new PrevisaoClimaEntity { Cidade = "Curitiba", Temperatura = 18, Condicao = "Chuvoso" }
            };
            _context.PrevisoesClimaticas.AddRange(previsoes);
            _context.SaveChanges();

            // Act
            var resultado = _previsaoRepository.ObterTodas();

            // Assert
            Assert.Equal(previsoes.Count, resultado.Count());
            Assert.Equal(previsoes[0].Cidade, resultado.First().Cidade);
            Assert.Equal(previsoes[1].Cidade, resultado.Last().Cidade);
        }

        [Fact]
        public void Remover_DeveRemoverPrevisaoClimaERetornarPrevisaoClimaEntity_QuandoPrevisaoExiste()
        {
            // Arrange
            var previsao = new PrevisaoClimaEntity { Cidade = "Florianópolis", Temperatura = 24, Condicao = "Nevoeiro" };
            _context.PrevisoesClimaticas.Add(previsao);
            _context.SaveChanges();

            // Act
            var resultado = _previsaoRepository.Remover(previsao.Id);

            // Assert
            var previsaoNoDb = _context.PrevisoesClimaticas.FirstOrDefault(p => p.Id == previsao.Id);

            Assert.Null(previsaoNoDb);
            Assert.Equal(previsao, resultado);
        }
    }
}
