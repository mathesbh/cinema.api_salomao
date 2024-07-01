using Domain.Adapters;
using Domain.Models;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace Application.Tests
{
    [TestClass]
    public class FilmesTests
    {
        private readonly IFilmesService filmesService;
        private readonly Mock<ITmdbAdapter> tmdbAdapterMock;

        public FilmesTests()
        {
            tmdbAdapterMock = new Mock<ITmdbAdapter>();
            filmesService = new FilmesService(tmdbAdapterMock.Object);
        }

        [TestMethod]
        [Trait(nameof(IFilmesService.ObterFilmesAsync), "Sucesso")]
        public async Task ObterFilmesAsync_Sucesso()
        {
            // Objeto que sera utilizado para retorno do Mock
            var expected = new List<Filme>()
                {
                    new Filme()
                    {
                        Id = 10447, 
                        Descricao = "descricao_teste",
                        Nome = "nome_teste"
                    }
                };

            tmdbAdapterMock
                .Setup(m => m.GetFilmesAsync(It.IsAny<Pesquisa>()))
                .ReturnsAsync(expected);

            var filmes = await filmesService.ObterFilmesAsync(new Pesquisa()
            {
                TermoPesquisa = "teste"
            });

            var exepectedSingle = expected.Single();

            Assert.Contains(filmes, f =>
                    f.Id == exepectedSingle.Id &&
                    f.Descricao == exepectedSingle.Descricao &&
                    f.Nome == exepectedSingle.Nome);
        }

        [TestMethod]
        [Trait(nameof(IFilmesService.ObterFilmesAsync), "Erro")]
        public async Task ObterFilmesAsync_Erro()
        {
            await Assert.ThrowsAnyAsync<Exception>(async () =>
            {
                await filmesService.ObterFilmesAsync(new Pesquisa());
            });
        }
    }
}
