using System.ComponentModel.DataAnnotations;


namespace MediWeb.Models
{
    public class GeneroModel
    {
        [Key]
        public Int32 id { get; set; }
        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public string? Descripcion { get; set; }

    }
}
