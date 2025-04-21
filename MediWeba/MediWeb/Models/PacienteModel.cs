using System.ComponentModel.DataAnnotations;

namespace MediWeb.Models
{
    public class PacienteModel
    {

        [Key]
        public Int32 id { get; set; }
        [Required(ErrorMessage = "el campo DescripcionGrupo es obligatorio")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "el campo DescripcionLarga es obligatorio")]
        public string? Apellido { get; set; }
      

        [Required(ErrorMessage = "el campo estado es obligatorio")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "el campo estado es obligatorio")]
        public string? DNI { get; set; }

        [Required(ErrorMessage = "el campo estado es obligatorio")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "el campo Direccion es obligatorio")]
        public string? Direccion { get; set; }

     //   [Required(ErrorMessage = "el campo ExpedienteId es obligatorio")]
        public Int32 ExpedienteId { get; set; }

 
        [Required(ErrorMessage = "el campo FechaCreacion es obligatorio")]
        public DateTime? FechaCreacion { get; set; }

  
        [Required(ErrorMessage = "el campo FechaNacimiento es obligatorio")]
        public DateTime? FechaNacimiento { get; set; }


        [Required(ErrorMessage = "el campo estado es obligatorio")]
        public Int32? Sexo { get; set; }

        [Required(ErrorMessage = "el campo estado es obligatorio")]
        public string? estado { get; set; }

        [Required(ErrorMessage = "el campo estado es obligatorio")]
        public Decimal? Peso { get; set; }
        [Required(ErrorMessage = "el campo estado es obligatorio")]
        public Decimal? altura { get; set; }
    

        public virtual GeneroModel? Generos { get; set; }
    }
}
