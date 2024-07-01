using System.ComponentModel.DataAnnotations;

namespace Adapter.TmdbAdapter
{
    public class TmdbAdapterConfiguration
    {
        [Required]
        public string TmdbApiUrlBase { get; set; }
        [Required]
        public string TmdbApiKey { get; set; }
        [Required]
        public string Idioma { get; set; }
    }
}
