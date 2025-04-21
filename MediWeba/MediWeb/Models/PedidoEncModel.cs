namespace MediWeb.Models
{
    public class PedidoEncModel
    {

        public string Rtn { get; set; }
        public string CAI { get; set; }
        public long PacienteId { get; set; }
        public decimal Total { get; set; }
        public decimal ISV { get; set; }
        public decimal TotalIsv { get; set; }

   
        public List<PedidoDetalleModel> Detalles { get; set; }

    }
}
