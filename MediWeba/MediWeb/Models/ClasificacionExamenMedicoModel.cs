using System.ComponentModel.DataAnnotations;

namespace MediWeb.Models
{
    public class ClasificacionExamenMedicoModel
    {
        [Key]
        public Int32 id { get; set; }

        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "el campo estado es obligatorio")]
        public string? estado { get; set; }

    }
}
