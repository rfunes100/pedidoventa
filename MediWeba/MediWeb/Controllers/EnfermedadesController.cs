using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediWeb.Data;
using MediWeb.Models;
using MediWeb.Consultas;
using StackExchange.Redis;

namespace MediWeb.Controllers
{
    public class EnfermedadesController : Controller
    {

        EnfermedadesConsulta EnfermedadesConsultas = new EnfermedadesConsulta();
        ClasificacionEnfermedadesConsultas ClasificacionEnfermedadesConsulta = new ClasificacionEnfermedadesConsultas();


        EspecialidadMedicaConsulta EspecialidadMedicaConsultas = new EspecialidadMedicaConsulta();


        public IActionResult Listar()
        {
            var enfermeraLista = EnfermedadesConsultas.Listar();
            return View(enfermeraLista);
        }




        public IActionResult Guardar()
        {

            CargarEspecialidadMedica();

            CargarClasificacionEnfermedades();
            return View();
        }



        [HttpPost]
        public IActionResult Guardar( EnfermedadesModel enfermedadmodel)
        {
            enfermedadmodel.estado = "A";
           // ModelState.Remove("Clasificaciones");
           // ModelState.Remove("Id");

            if (string.IsNullOrEmpty(enfermedadmodel.DescripcionLarga) || string.IsNullOrEmpty(enfermedadmodel.DescripcionGrupo)
                || (enfermedadmodel.clasificacionId.HasValue && enfermedadmodel.clasificacionId.Value == 0 )
                )
            { 
                return View();
            }


            var respuesta = EnfermedadesConsultas.Guardar(enfermedadmodel);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }


        public IActionResult Editar(Int32 idEnfermedad)
        {

            CargarClasificacionEnfermedades();
            var enfermedadid = EnfermedadesConsultas.Obtener(idEnfermedad);
          
            return View(enfermedadid);

        }


        [HttpPost]
        public IActionResult Editar(EnfermedadesModel enfermedadmodel)
        {
            enfermedadmodel.estado = "A";

            if (string.IsNullOrEmpty(enfermedadmodel.DescripcionLarga) || string.IsNullOrEmpty(enfermedadmodel.DescripcionGrupo)
                 || (enfermedadmodel.clasificacionId.HasValue && enfermedadmodel.clasificacionId.Value == 0)
                 )
            {
                return View();
            }


            var respuesta = EnfermedadesConsultas.Editar(enfermedadmodel);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();

        }




        public IActionResult Eliminar(Int32 idEnfermedad)
        {

            var enfermedadId = EnfermedadesConsultas.Obtener(idEnfermedad);
            return View(enfermedadId);

        }


        [HttpPost]
        public IActionResult Eliminar(EnfermedadesModel enfermedadmodel)
        {


            var respuesta = EnfermedadesConsultas.Eliminar(enfermedadmodel.Id);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();




        }

        [NonAction]
        private void CargarClasificacionEnfermedades()
        {
            var categories = ClasificacionEnfermedadesConsulta.Listar();


            //    var enfermeraLista = EnfermedadesConsultas.Listar();
            ViewBag.Categories = new SelectList(categories, "Id", "Descripcion");
           
        }

        [NonAction]
        private void CargarEspecialidadMedica()
        {
            var categories = EspecialidadMedicaConsultas.Listar();


            //    var enfermeraLista = EnfermedadesConsultas.Listar();
            ViewBag.Especialidades = new SelectList(categories, "id", "Nombre");

        }


  

    }
}
