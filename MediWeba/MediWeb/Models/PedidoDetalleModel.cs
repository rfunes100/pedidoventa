namespace MediWeb.Models
{
    public class PedidoDetalleModel
    {

        public long ProductoID { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        public decimal? Totalisv { get; set; }
        public decimal? ISV { get; set; }
        public string? nombre { get; set; } // Agregar esta propiedad

        public long? PedidoEncabezadoId { get; set; }

    }
}
