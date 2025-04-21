using Microsoft.AspNetCore.Mvc;

using MediWeb.Consultas;
using MediWeb.Models;


namespace MediWeb.Controllers
{
    public class EnfermeraController : Controller
    {

        EnfermeraConsulta enfermeraConsultas = new EnfermeraConsulta();

        public IActionResult Listar()
        {
            var enfermeraLista = enfermeraConsultas.Listar();
            return View(enfermeraLista);
        }


        public IActionResult Guardar()
        {

           
            return View();
        }


        [HttpPost]
        public IActionResult Guardar( EnfermeraModel enfermeramodel)
        {

            if(!ModelState.IsValid)
            {
                return View();
            }


            var respuesta = enfermeraConsultas.Guardar(enfermeramodel);
            if(respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }


        public IActionResult Editar( Int32 idEnfermera)
        {

            var enfermeraId = enfermeraConsultas.Obtener(idEnfermera);
            return View(enfermeraId);
  
        }


        [HttpPost]
        public IActionResult Editar(EnfermeraModel enfermeramodel )
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            var respuesta = enfermeraConsultas.Editar(enfermeramodel);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();

        }




        public IActionResult Eliminar(Int32 idEnfermera)
        {

            var enfermeraId = enfermeraConsultas.Obtener(idEnfermera);
            return View(enfermeraId);

        }


        [HttpPost]
        public IActionResult Eliminar(EnfermeraModel enfermeramodel)
        {
          

            var respuesta = enfermeraConsultas.Eliminar(enfermeramodel.id);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();




        }



    }
}
