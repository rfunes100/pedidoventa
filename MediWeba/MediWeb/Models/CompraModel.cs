using System.ComponentModel.DataAnnotations;

namespace MediWeb.Models
{
    public class CompraModel
    {
        [Key]
        public Int32 Id { get; set; }
        public int ArticuloId { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }

        public string? NombreArticulo { get; set; }
        

    }
}
