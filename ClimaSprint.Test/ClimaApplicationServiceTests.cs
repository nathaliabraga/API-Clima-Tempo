using ClimaSprint.Application.Services;
using ClimaSprint.Domain.Entities;
using ClimaSprint.Domain.Interfaces;
using ClimaSprint.Domain.Interfaces.Dtos;
using Moq;
using Xunit;

namespace ClimaSprint.Test
{
    public class PrevisaoClimaApplicationServiceTests
    {
        private readonly Mock<IClimaService> _climaServiceMock;
        private readonly Mock<IPrevisaoClimaRepository> _repositoryMock;
        private readonly PrevisaoClimaApplicationService _previsaoService;

        public PrevisaoClimaApplicationServiceTests()
        {
            _repositoryMock = new Mock<IPrevisaoClimaRepository>();
            _climaServiceMock = new Mock<IClimaService>();

            _previsaoService = new PrevisaoClimaApplicationService(_repositoryMock.Object, _climaServiceMock.Object);
        }

        [Fact]
        public void AdicionarPrevisao_DeveRetornarPrevisaoClimaEntity_QuandoAdicionarComSucesso()
        {
            // Arrange
            var previsaoDtoMock = new Mock<IPrevisaoClimaDto>();
            previsaoDtoMock.Setup(p => p.Cidade).Returns("São Paulo");
            previsaoDtoMock.Setup(p => p.Temperatura).Returns(25.5);
            previsaoDtoMock.Setup(p => p.Condicao).Returns("Ensolarado");
            previsaoDtoMock.Setup(p => p.Data).Returns(DateTime.Now);

            var previsaoEsperada = new PrevisaoClimaEntity
            {
                Cidade = "São Paulo",
                Temperatura = 25.5,
                Condicao = "Ensolarado",
                Data = previsaoDtoMock.Object.Data
            };
            _repositoryMock.Setup(r => r.Adicionar(It.IsAny<PrevisaoClimaEntity>())).Returns(previsaoEsperada);

            // Act
            var resultado = _previsaoService.AdicionarPrevisao(previsaoDtoMock.Object);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(previsaoEsperada.Cidade, resultado.Cidade);
            Assert.Equal(previsaoEsperada.Temperatura, resultado.Temperatura);
            Assert.Equal(previsaoEsperada.Condicao, resultado.Condicao);
            Assert.Equal(previsaoEsperada.Data, resultado.Data);
        }

        [Fact]
        public void EditarPrevisao_DeveRetornarPrevisaoClimaEntity_QuandoEditarComSucesso()
        {
            // Arrange
            var previsaoDtoMock = new Mock<IPrevisaoClimaDto>();
            previsaoDtoMock.Setup(p => p.Cidade).Returns("Rio de Janeiro");
            previsaoDtoMock.Setup(p => p.Temperatura).Returns(30.0);
            previsaoDtoMock.Setup(p => p.Condicao).Returns("Nublado");
            previsaoDtoMock.Setup(p => p.Data).Returns(DateTime.Now);

            var previsaoEsperada = new PrevisaoClimaEntity
            {
                Id = 1,
                Cidade = "Rio de Janeiro",
                Temperatura = 30.0,
                Condicao = "Nublado",
                Data = previsaoDtoMock.Object.Data
            };
            _repositoryMock.Setup(r => r.Editar(It.IsAny<PrevisaoClimaEntity>())).Returns(previsaoEsperada);

            // Act
            var resultado = _previsaoService.EditarPrevisao(1, previsaoDtoMock.Object);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(previsaoEsperada.Id, resultado.Id);
            Assert.Equal(previsaoEsperada.Cidade, resultado.Cidade);
            Assert.Equal(previsaoEsperada.Temperatura, resultado.Temperatura);
            Assert.Equal(previsaoEsperada.Condicao, resultado.Condicao);
            Assert.Equal(previsaoEsperada.Data, resultado.Data);
        }

        [Fact]
        public void ObterPrevisaoPorId_DeveRetornarPrevisaoClimaEntity_QuandoPrevisaoExiste()
        {
            // Arrange
            var previsaoEsperada = new PrevisaoClimaEntity
            {
                Id = 1,
                Cidade = "Curitiba",
                Temperatura = 15.0,
                Condicao = "Chuvoso",
                Data = DateTime.Now
            };
            _repositoryMock.Setup(r => r.ObterPorId(1)).Returns(previsaoEsperada);

            // Act
            var resultado = _previsaoService.ObterPrevisaoPorId(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(previsaoEsperada.Id, resultado.Id);
            Assert.Equal(previsaoEsperada.Cidade, resultado.Cidade);
            Assert.Equal(previsaoEsperada.Temperatura, resultado.Temperatura);
            Assert.Equal(previsaoEsperada.Condicao, resultado.Condicao);
            Assert.Equal(previsaoEsperada.Data, resultado.Data);
        }

        [Fact]
        public void ObterTodasPrevisoes_DeveRetornarListaDePrevisoes_QuandoExistiremPrevisoes()
        {
            // Arrange
            var previsoesEsperadas = new List<PrevisaoClimaEntity>
            {
                new PrevisaoClimaEntity { Id = 1, Cidade = "Belo Horizonte", Temperatura = 20.0, Condicao = "Parcialmente Nublado", Data = DateTime.Now },
                new PrevisaoClimaEntity { Id = 2, Cidade = "Porto Alegre", Temperatura = 18.5, Condicao = "Chuvoso", Data = DateTime.Now }
            };
            _repositoryMock.Setup(r => r.ObterTodos()).Returns(previsoesEsperadas);

            // Act
            var resultado = _previsaoService.ObterTodasPrevisoes();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            Assert.Equal(previsoesEsperadas.First().Cidade, resultado.First().Cidade);
        }

        [Fact]
        public void RemoverPrevisao_DeveRetornarPrevisaoClimaEntity_QuandoRemoverComSucesso()
        {
            // Arrange
            var previsaoEsperada = new PrevisaoClimaEntity
            {
                Id = 1,
                Cidade = "Salvador",
                Temperatura = 27.0,
                Condicao = "Ensolarado",
                Data = DateTime.Now
            };
            _repositoryMock.Setup(r => r.Remover(1)).Returns(previsaoEsperada);

            // Act
            var resultado = _previsaoService.RemoverPrevisao(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(previsaoEsperada.Id, resultado.Id);
            Assert.Equal(previsaoEsperada.Cidade, resultado.Cidade);
            Assert.Equal(previsaoEsperada.Temperatura, resultado.Temperatura);
            Assert.Equal(previsaoEsperada.Condicao, resultado.Condicao);
            Assert.Equal(previsaoEsperada.Data, resultado.Data);
        }
    }
}
