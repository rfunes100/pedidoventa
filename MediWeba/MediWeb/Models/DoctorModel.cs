
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediWeb.Models
{
    public class DoctorModel
    {
        [Key]
        public Int32 id { get; set; }
        [Required(ErrorMessage = "el campo DescripcionGrupo es obligatorio")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "el campo DescripcionLarga es obligatorio")]
        public string? Apellido { get; set; }
        [Required(ErrorMessage = "el campo estado es obligatorio")]

        public Int32 especialidadid { get; set; }
        [Required(ErrorMessage = "el campo estado es obligatorio")]
        public DateTime fecha_Ingreso { get; set; }

        [Required(ErrorMessage = "el campo estado es obligatorio")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "el campo estado es obligatorio")]
        public string? TelefonoTrabajo { get; set; }

        [Required(ErrorMessage = "el campo estado es obligatorio")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "el campo estado es obligatorio")]
        public string? estado { get; set; }

        [Required(ErrorMessage = "el campo estado es obligatorio")]
        public Int32 idProcedimientoDoctor { get; set; }

        [Required(ErrorMessage = "el campo estado es obligatorio")]
        public Int32? Sexo { get; set; }

        // [Required(ErrorMessage = "el campo clasificacionId es obligatorio")]
        // public Int32? clasificacionId { get; set; }


       //  Relación con la tabla Clasificacion
      //  public virtual EspecialidadMedicaModel? Especialidades { get; set; }
      public virtual GeneroModel? Generos { get; set; }

    }
}
