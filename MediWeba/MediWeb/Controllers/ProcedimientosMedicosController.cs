using MediWeb.Consultas;
using MediWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediWeb.Controllers
{
    public class ProcedimientosMedicosController : Controller
    {

        ProcedimientosMedicosConsulta ProcedimientosMedicosConsultas = new ProcedimientosMedicosConsulta();
    


        public IActionResult ListaDoctor(Int32 id)
        {
            ViewBag.Id = id;
            var enfermeraLista = ProcedimientosMedicosConsultas.Obtener(id);
            return View(enfermeraLista);
        }

        public IActionResult Guardar(Int32 id)
        {
          
            ViewBag.Id = id;
           
            return View();
        }


        [HttpPost]
        public IActionResult Guardar(ProcedimientosMedicosModel doctormodel)
        {


            if ((doctormodel.EspecialidadId == 0) ||  doctormodel.Descripcion == ""
                //   || (enfermedadmodel.clasificacionId.HasValue && enfermedadmodel.clasificacionId.Value == 0)
                )
            {
                return View();
            }


            var respuesta = ProcedimientosMedicosConsultas.Guardar(doctormodel);
            if (respuesta)

                return RedirectToAction("ListaDoctor", new { id = doctormodel.EspecialidadId });
            else
                return View();
        }



        public IActionResult Eliminar(Int32 id)
        {

            var enfermedadId = ProcedimientosMedicosConsultas.ObtenerId(id);
            ViewBag.Idespeciliadad = enfermedadId.EspecialidadId;

            return View(enfermedadId);

        }



        [HttpPost]
        public IActionResult Eliminar(ProcedimientosMedicosModel enfermedadmodel)
        {


            var respuesta = ProcedimientosMedicosConsultas.Eliminar(enfermedadmodel.id);
            if (respuesta)
                return RedirectToAction("ListaDoctor", new { id = enfermedadmodel.EspecialidadId });
            else
                return View();




        }






    }
}
