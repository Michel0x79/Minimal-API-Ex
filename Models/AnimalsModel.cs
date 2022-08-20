using System.ComponentModel.DataAnnotations;

namespace MinhaApi.Models
{
    public class AnimalsModel
    {
        public AnimalsModel()
        {
            this.Id = Guid.NewGuid();

        }
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Campo Name obrigatório!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Campo RACA obrigatório!")]
        public string Raca { get; set; }

    }
}