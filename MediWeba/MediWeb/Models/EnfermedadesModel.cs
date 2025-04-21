using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediWeb.Models
{
    public class EnfermedadesModel
    {
        [Key]
        public Int32 Id { get; set; }
        [Required(ErrorMessage = "el campo DescripcionGrupo es obligatorio")]
        public string? DescripcionGrupo { get; set; }
        [Required(ErrorMessage = "el campo DescripcionLarga es obligatorio")]
        public string? DescripcionLarga { get; set; }
        [Required(ErrorMessage = "el campo estado es obligatorio")]
        public string? estado { get; set; }

       // [Required(ErrorMessage = "el campo clasificacionId es obligatorio")]
        public Int32? clasificacionId { get; set; }

        
        // Relación con la tabla Clasificacion
        public virtual  ClasificacionEnfermedadesModel? Clasificaciones { get; set; }



    }
}
