using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Numerics;
using System.ComponentModel.DataAnnotations;

namespace MediWeb.Models
{
    public class EnfermeraModel
    {


        public Int32 id { get; set; }
        [Required(ErrorMessage = "el campo Nombre es obligatorio")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "el campo Telefono es obligatorio")]
        public string? Telefono { get; set; }
        [Required(ErrorMessage = "el campo DNI es obligatorio")]
        public string? DNI { get; set; }
        [Required(ErrorMessage = "el campo Direccion es obligatorio")]
        public string? Direccion { get; set; }

    }
}
