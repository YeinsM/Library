using System.ComponentModel.DataAnnotations;

namespace Library.WebApi.DTOs.Author
{
    public class CreateAuthorDto
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "La nacionalidad es requerida.")]
        public string Nationality { get; set; }
    }
}
