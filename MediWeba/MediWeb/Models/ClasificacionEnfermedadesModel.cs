using System.ComponentModel.DataAnnotations;

namespace MediWeb.Models
{
    public class ClasificacionEnfermedadesModel
    {

        [Key]
        public Int32 Id { get; set; }
     
        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public string? Descripcion { get; set; }

       

    }
}
