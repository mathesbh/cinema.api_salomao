using Domain.Adapters;
using Domain.Models;
using Domain.Services;

namespace Application
{
    public class FilmesService : IFilmesService
    {
        private readonly ITmdbAdapter tmdbAdapter;

        public FilmesService(ITmdbAdapter tmdbAdapter)
        {
            this.tmdbAdapter = tmdbAdapter ??
                throw new ArgumentNullException(nameof(tmdbAdapter));
        }

        public async Task<IEnumerable<Filme>> ObterFilmesAsync(
            Pesquisa pesquisa)
        {
            if (pesquisa == null || string.IsNullOrWhiteSpace(pesquisa.TermoPesquisa))
            {
                throw new Exception("Critérios de pesquisa não são validos.");
            }

            IEnumerable<Filme> resultado = await tmdbAdapter
                .GetFilmesAsync(pesquisa);

            return resultado;
        }
    }
}
