using AutoMapper;
using Domain.Adapters;
using Domain.Models;
using Refit;
using System.Net;
using TmdbAdapter.Clients;

namespace Adapter.TmdbAdapter
{
    internal class TmdbAdapter : ITmdbAdapter
    {
        private readonly ITmdbApi tmdbApi;
        private readonly TmdbAdapterConfiguration tmdbAdapterConfiguration;
        private readonly IMapper mapper;

        public TmdbAdapter(ITmdbApi tmdbApi,
            TmdbAdapterConfiguration tmdbAdapterConfiguration,
            IMapper mapper)
        {
            this.tmdbApi = tmdbApi ??
                throw new ArgumentNullException(nameof(tmdbApi));

            this.tmdbAdapterConfiguration = tmdbAdapterConfiguration ??
                throw new ArgumentNullException(nameof(tmdbAdapterConfiguration));

            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<Filme>> GetFilmesAsync(
            Pesquisa pesquisa)
        {
            try
            {
                var tmdbSearchMoviesGet =
                    mapper.Map<TmdbSearchMoviesGet>(pesquisa);

                tmdbSearchMoviesGet.ApiKey =
                    tmdbAdapterConfiguration.TmdbApiKey;

                tmdbSearchMoviesGet.Language = tmdbAdapterConfiguration.Idioma;

                var tmdbSearchMoviesGetResult = await tmdbApi
                    .SearchMovies(tmdbSearchMoviesGet);

                return mapper
                    .Map<IEnumerable<Filme>>(tmdbSearchMoviesGetResult.Results);
            }
            catch (ApiException e)
            {
                switch (e.StatusCode)
                {
                    case (HttpStatusCode)429:
                        throw new Exception();
                }

                throw;
            }
        }
    }
}
