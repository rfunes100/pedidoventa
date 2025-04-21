using System.ComponentModel.DataAnnotations;

namespace MediWeb.Models
{
    public class EspecialidadMedicaModel
    {

        [Key]
        public Int32 id { get; set; }
        [Required(ErrorMessage = "el campo DescripcionGrupo es obligatorio")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "el campo DescripcionLarga es obligatorio")]
        public string? Descripcion { get; set; }


        // Navigation property
    //    public virtual ICollection<EspecialidadDoctorModel> EspecialidadDoctors { get; set; }

    }
}
