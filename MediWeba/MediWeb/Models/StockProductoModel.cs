namespace MediWeb.Models
{
    public class StockProductoModel
    {

        public long MedicamentoID { get; set; }
        public string? NombreMedicamento { get; set; }
        public int TotalComprado { get; set; }
        public int TotalVendido { get; set; }
        public int StockActual { get; set; }
        public int Diferencia { get; set; }
        public int DiferenciaReal { get; set; }
    }
}
