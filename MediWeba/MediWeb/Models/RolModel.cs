using System.ComponentModel.DataAnnotations;

namespace MediWeb.Models
{
    public class RolModel
    {
        [Key]
        public Int32 id { get; set; }

        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public string? Nombre { get; set; }
    }
}
