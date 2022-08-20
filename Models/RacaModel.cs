using System.ComponentModel.DataAnnotations;

namespace MinhaApi.Models
{
    public class RacaModel
    {
        [Key]
        public Guid Id{ get; set; }
        public string? RacaName {get; set; }

    }
}