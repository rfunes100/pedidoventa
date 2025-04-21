using System.ComponentModel.DataAnnotations;

namespace MediWeb.Models
{
    public class ProcedimientoDoctorModel
    {
        [Key]
        public Int32 id { get; set; }
        [Required(ErrorMessage = "el campo DescripcionGrupo es obligatorio")]
        public Int32? Doctorid { get; set; }
        [Required(ErrorMessage = "el campo DescripcionLarga es obligatorio")]
        public Int32? ProcedimientosMedicos { get; set; }
        


    }
}
