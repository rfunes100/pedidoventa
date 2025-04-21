using MediWeb.Consultas;
using MediWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediWeb.Controllers
{
    public class ExpedienteController : Controller
    {

        ExpedienteConsulta ExpedienteConsultaS = new ExpedienteConsulta();
        LesionesConsulta LesionesConsultas = new LesionesConsulta();
        EnfermedadesConsulta EnfermedadesConsultas = new EnfermedadesConsulta();




        public IActionResult Lista(Int32 id)
        {
            var enfermeraLista = ExpedienteConsultaS.Obtener(id);
            ViewBag.Id = id;
            return View(enfermeraLista);
        }


        public IActionResult Guardar()
        {

            CargarLesiones();
            CargarEnfermedades();


            return View();
        }


        [HttpPost]
        public IActionResult Guardar(ExpedienteModel doctormodel)
        {
            doctormodel.estado = "REGISTRADO";
            doctormodel.AntecendenteFamiliarId = 1;
            doctormodel.CirugiasID=1;
            doctormodel.HospitalizacionId = 1;
            doctormodel.MedicosID=1;
            doctormodel.AlergiasId=1;
            doctormodel.MedicosID = 1;
            doctormodel.ProblemasCronicosId = 1;
            doctormodel.VacunaId=1;
            doctormodel.AntecendenteFamiliarId = 1;
            doctormodel.MedicamentoId=1;
            doctormodel.LesionesId=1;
            




            if (    ( doctormodel.EnfermedadesID == 0) || doctormodel.LesionesId == 0 )
            {
                return View();
            }


            var respuesta = ExpedienteConsultaS.Guardar(doctormodel);
            if ( Convert.ToInt32( respuesta) != 0)

                 return RedirectToAction("Guardar", "LesionesExp", new { id = respuesta });

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


        [NonAction]
        private void CargarEnfermedades()
        {
            var generos = EnfermedadesConsultas.Listar();


            //    var enfermeraLista = EnfermedadesConsultas.Listar();
            ViewBag.Enfermedades = new SelectList(generos, "Id", "DescripcionLarga");

        }


    }
}
