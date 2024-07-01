using Refit;

namespace TmdbAdapter.Clients
{
    internal interface ITmdbApi
    {
        [Get("/search/movie")]
        Task<TmdbSearchMoviesGetResult> SearchMovies(
            [Query] TmdbSearchMoviesGet tmdbSearchMoviesGet);
    }
}
