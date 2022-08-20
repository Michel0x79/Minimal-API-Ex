using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MinhaApi.Models
{
    public class AnimalsModel
    {
        // public AnimalsModel()
        // {
        //     // this.Id = Guid.NewGuid();

        // }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Name obrigatório!")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Campo RACA obrigatório!")]
        public int RacaId {get; set;}
        [JsonIgnore]
        public RacaModel? Raca { get; set; }

    }
}