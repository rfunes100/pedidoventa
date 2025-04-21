

using System.ComponentModel.DataAnnotations;

namespace MediWeb.Models
{
    public class MedicamentoModel
    {

        [Key]
        public Int32 Id { get; set; }

        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public string? nombre { get; set; }

        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public string? imagen { get; set; }

        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public Decimal? Precio { get; set; }

        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public int? Cantidad { get; set; }

        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public Int32? CategoriaID { get; set; }



        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public string? estado { get; set; }


    }
}
