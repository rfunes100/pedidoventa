using System.ComponentModel.DataAnnotations;

namespace MediWeb.Models
{
    public class UsuarioModel
    {
        [Key]
        public Int32 id { get; set; }

        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public string? Userid { get; set; }
        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public string? Apellido { get; set; }
        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public string? Correo { get; set; }


        [Required(ErrorMessage = "el campo estado es obligatorio")]
        public string? estado { get; set; }

        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public Int32? RolId { get; set; }

        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public string? Clave { get; set; }

        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public string? Huellabimoetrica { get; set; }

        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public string? PreguntaRecuperacion { get; set; }
        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public string? RespuestaRecuperacion { get; set; }
        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public DateTime? fechaCreacion { get; set; }


        public virtual RolModel? Roles { get; set; }
    }
}
