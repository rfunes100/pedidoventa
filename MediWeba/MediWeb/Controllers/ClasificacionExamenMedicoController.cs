using MediWeb.Consultas;
using Microsoft.AspNetCore.Mvc;

namespace MediWeb.Controllers
{
    public class ClasificacionExamenMedicoController : Controller
    {
        ClasificacionExamenMedicoConsulta ClasificacionExamenMedicoConsultas = new ClasificacionExamenMedicoConsulta  ();



        public IActionResult Listar()
        {
            var enfermeraLista = ClasificacionExamenMedicoConsultas.Listar();
            return View(enfermeraLista);
        }



    }
}
