using MediWeb.Consultas;
using MediWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediWeb.Controllers
{
    public class LesionesExpController : Controller
    {

        LesionesExpConsulta LesionesExpConsultas = new LesionesExpConsulta();
        LesionesConsulta LesionesConsultas = new LesionesConsulta();


        public IActionResult Lista(Int32 id)
        {

            ViewBag.Expediente = id;
            var enfermeraLista = LesionesExpConsultas.Lista(id);
          
            return View(enfermeraLista);
        }

        public IActionResult Guardar(Int32 id)
        {

            ViewBag.Expediente = id;
            CargarLesiones();

            return View();
        }

        [HttpPost]
        public IActionResult Guardar(LesionesExpModel doctormodel)
        {
         



            if ((doctormodel.LesionesId == 0) || doctormodel.ExpedenteId == 0
                )
            {
                return View();
            }


            var respuesta = LesionesExpConsultas.Guardar(doctormodel);
            if (Convert.ToInt32(respuesta) != 0)

                return RedirectToAction("Lista", "LesionesExp", new { id = doctormodel.ExpedenteId });

            else
                return View();
        }


        public IActionResult Eliminar(Int32 idEnfermedad)
        {

            var enfermedadId = LesionesExpConsultas.Obtener(idEnfermedad);
            ViewBag.Expediente = enfermedadId.ExpedenteId;
            return View(enfermedadId);

        }


        [HttpPost]
        public IActionResult Eliminar(LesionesExpModel enfermedadmodel)
        {


            var respuesta = LesionesExpConsultas.Eliminar(enfermedadmodel.id);
            if (respuesta)
                return RedirectToAction("Lista", new { id = enfermedadmodel.ExpedenteId });
            else
                return View();




        }





        [NonAction]
        private void CargarLesiones()
        {
            var generos = LesionesConsultas.Listar();


            //    var enfermeraLista = EnfermedadesConsultas.Listar();
            ViewBag.Lesiones = new SelectList(generos, "id", "Descripcion");

        }

    }
}
