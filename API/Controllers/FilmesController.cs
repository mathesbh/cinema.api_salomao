using AutoMapper;
using Domain.Models;
using Domain.Services;
using Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CriptoMoeda.Api.Controllers
{
    [Route("[controller]")]
    public class FilmesController : ControllerBase
    {
        private readonly IFilmesService filmesService;
        private readonly IMapper mapper;

        public FilmesController(IFilmesService filmesService, IMapper mapper)
        {
            this.filmesService = filmesService ??
                throw new ArgumentNullException(nameof(filmesService));

            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Pesquisa por filmes.
        /// </summary>
        /// <param name="filmesGet">
        ///     Criterios de pesquisa na base de filmes.
        /// </param>
        /// <response code="200">Lista de resultados.</response>
        /// <response code="400">
        ///     Parametros incorretos ou limite de utilização excedido.
        /// </response>
        /// <response code="500">Erro interno.</response>
        [HttpGet, AllowAnonymous]
        [ProducesResponseType(typeof(FilmesGetResult), 200)]
        /// <response code="400"> Dados inválidos</response>
        /// <response code="500">Erro interno.</response>
        public async Task<IActionResult> GetFilmesAsync(
            [FromQuery] FilmesGet filmesGet)
        {
            Pesquisa pesquisa = mapper.Map<FilmesGet, Pesquisa>(filmesGet);
            IEnumerable<Filme> filmes = await filmesService
                .ObterFilmesAsync(pesquisa);
            IEnumerable<FilmesGetResult> filmesGetResults =
                mapper.Map<IEnumerable<FilmesGetResult>>(filmes);

            return Ok(filmesGetResults);
        }
    }
}
