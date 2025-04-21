using System.ComponentModel.DataAnnotations;

namespace MediWeb.Models
{
    public class ProcedimientosMedicosModel
    {
        [Key]
        public Int32 id { get; set; }
        [Required(ErrorMessage = "el campo DescripcionGrupo es obligatorio")]
        public Int32? EspecialidadId { get; set; }
        [Required(ErrorMessage = "el campo DescripcionLarga es obligatorio")]
        public string? Descripcion { get; set; }

        public virtual EspecialidadMedicaModel? EspecialidadMedica { get; set; }


    }
}
