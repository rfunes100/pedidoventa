using System.ComponentModel.DataAnnotations;

namespace MediWeb.Models
{
    public class LesionesExpModel
    {

        [Key]
        public Int32 id { get; set; }
        [Required(ErrorMessage = "el campo DescripcionGrupo es obligatorio")]
        public Int32? ExpedenteId { get; set; }
        [Required(ErrorMessage = "el campo DescripcionLarga es obligatorio")]
        public Int32? LesionesId { get; set; }

        [Required(ErrorMessage = "el campo DescripcionLarga es obligatorio")]
        public DateTime? fechaLesion { get; set; }

        public virtual LesionesModel? Lesiones { get; set; }




    }
}
