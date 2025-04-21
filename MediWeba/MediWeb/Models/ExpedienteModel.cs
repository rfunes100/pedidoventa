using System.ComponentModel.DataAnnotations;

namespace MediWeb.Models
{
    public class ExpedienteModel
    {
        [Key]
        public Int32 id { get; set; }
        [Required(ErrorMessage = "el campo DescripcionGrupo es obligatorio")]
        public Int32? EnfermedadesID { get; set; }
        [Required(ErrorMessage = "el campo DescripcionLarga es obligatorio")]
        public Int32? LesionesId { get; set; }

        public Int32 CirugiasID { get; set; }
        [Required(ErrorMessage = "el campo DescripcionGrupo es obligatorio")]
        public Int32? HospitalizacionId { get; set; }
        [Required(ErrorMessage = "el campo DescripcionLarga es obligatorio")]
        public Int32? MedicosID { get; set; }

        public Int32 AlergiasId { get; set; }
        [Required(ErrorMessage = "el campo DescripcionGrupo es obligatorio")]
        public Int32? MedicamentoId { get; set; }
        [Required(ErrorMessage = "el campo DescripcionLarga es obligatorio")]
        public Int32? ProblemasCronicosId { get; set; }

        public Int32 VacunaId { get; set; }
        [Required(ErrorMessage = "el campo DescripcionGrupo es obligatorio")]
        public Int32? AntecendenteFamiliarId { get; set; }
        [Required(ErrorMessage = "el campo DescripcionLarga es obligatorio")]
        public string? estado { get; set; }

    }
}
