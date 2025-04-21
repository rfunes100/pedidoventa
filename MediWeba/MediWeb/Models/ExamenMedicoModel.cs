using System.ComponentModel.DataAnnotations;

namespace MediWeb.Models
{
    public class ExamenMedicoModel
    {

        [Key]
        public Int32 Id { get; set; }
        public Int32 idClasificacionExamen { get; set; }
        [Required(ErrorMessage = "el campo Descripcion es obligatorio")]
        public string? Nombre { get; set; }
        public string? Sinonimo { get; set; }
        public string? MuestraPrimaria { get; set; }
        public string? MuestraAlterna { get; set; }
        public string? VolumenMinimo { get; set; }


        public Int32 requerimientoId { get; set; }
        public Int32 DiaExamenId { get; set; }
        public Int32 TiempoEntregaId { get; set; }
        public Int32 MetodologiaId { get; set; }

        [Required(ErrorMessage = "el campo estado es obligatorio")]
        public string? estado { get; set; }

        public virtual requerimientoModel? Requerimientos { get; set; }
        public virtual TiempoEntregaModel? TiemposEntrega { get; set; }
        public virtual DiaExamenModel? DiasExamen { get; set; }
        public virtual MetodologiaModel? Metodologias { get; set; }
    }
}
