using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MinhaApi.Models
{
    public class RacaModel
    {
        [Key]
        public int Id{ get; set; }
        public string? RacaName {get; set; }
        public ICollection<AnimalsModel>? Animais {get; set;}

    }
}