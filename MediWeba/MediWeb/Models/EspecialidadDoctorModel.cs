using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediWeb.Models
{
    public class EspecialidadDoctorModel
    {
        
        [Key]
        public Int32 id { get; set; }
        [Required(ErrorMessage = "el campo DescripcionGrupo es obligatorio")]
        public Int32? idDoctor { get; set; }
        [Required(ErrorMessage = "el campo DescripcionLarga es obligatorio")]
        public Int32? idEspecailidad { get; set; }

        // Navigation property
      //  [ForeignKey("idEspecailidad")]
        public virtual  EspecialidadMedicaModel? EspecialidadMedica { get; set; }


      

    }
}
