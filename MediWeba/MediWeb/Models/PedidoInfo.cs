namespace MediWeb.Models
{
    public class PedidoInfo
    {

         public DateTime? FechaCreacion { get; set; }
    public string? NombrePaciente { get; set; }
    public string? NombreMedicamento { get; set; }
    public decimal? Precio { get; set; }
    public int? Cantidad { get; set; }
    public decimal? Total { get; set; }

    public long? productid { get; set; }

        
    }
}
