using Adapter.TmdbAdapter;
using AutoMapper;
using Domain.Models;
using Dtos;

namespace baseMap
{
    public static class AutoMapperStartup
    {
        public static void AddAutoMapperCustomizado(this IServiceCollection servicos)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AllowNullDestinationValues = true;
                mc.AllowNullCollections = true;
                mc.AddProfile(new MappingProfile());
                mc.AddProfile(new TmdbMapperProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            servicos.AddSingleton(mapper);
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Filme, FilmesGetResult>();
                CreateMap<FilmesGet, Pesquisa>();
            }
        }
    }
}
