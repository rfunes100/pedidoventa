using System.ComponentModel.DataAnnotations;

namespace MediWeb.Models
{
    public class LesionesModel
    {

        [Key]
        public Int32 id { get; set; }
        [Required(ErrorMessage = "el campo DescripcionGrupo es obligatorio")]
        public string? Parte { get; set; }
        [Required(ErrorMessage = "el campo DescripcionLarga es obligatorio")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "el campo DescripcionLarga es obligatorio")]
        public DateTime? FechaLesion { get; set; }
        [Required(ErrorMessage = "el campo DescripcionLarga es obligatorio")]
        public string? Estado { get; set; }

    }
}
