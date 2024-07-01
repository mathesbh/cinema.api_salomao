using Adapter.TmdbAdapter;
using Domain.Adapters;
using Refit;
using System.Diagnostics.CodeAnalysis;
using TmdbAdapter.Clients;

namespace Microsoft.Extensions.DependencyInjection.IMDbAdapter
{
    public static class IMDbAdapterServiceCollectionExtensions
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddIMDbAdapter(
            this IServiceCollection services,
            TmdbAdapterConfiguration tmdbAdapterConfiguration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (tmdbAdapterConfiguration == null)
            {
                throw new ArgumentNullException(nameof(tmdbAdapterConfiguration));
            }

            services.AddSingleton(tmdbAdapterConfiguration);

            services.AddScoped(serviceProvider =>
            {
                var httpClient = new HttpClient();

                httpClient.BaseAddress =
                    new Uri(tmdbAdapterConfiguration.TmdbApiUrlBase);

                return RestService.For<ITmdbApi>(httpClient);
            });

            services.AddScoped<ITmdbAdapter, Adapter.TmdbAdapter.TmdbAdapter>();

            return services;
        }
    }
}
