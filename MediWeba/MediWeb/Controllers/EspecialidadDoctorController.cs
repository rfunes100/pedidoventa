using MediWeb.Consultas;
using MediWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediWeb.Controllers
{
    public class EspecialidadDoctorController : Controller
    {

        DoctorConsulta DoctorConsultas = new DoctorConsulta();
        EspecialidadMedicaConsulta EspecialidadMedicaConsultas = new EspecialidadMedicaConsulta();
        EspecialidadDoctorConsulta EspecialidadDoctorConsultas = new EspecialidadDoctorConsulta();



        public IActionResult ListaDoctor(Int32 id)
        {
            var enfermeraLista = EspecialidadDoctorConsultas.Obtener(id);
            ViewBag.Id = id;
            return View(enfermeraLista);
        }


        public IActionResult Guardar(Int32 id)
        {
            CargarEspecialidadMedica();
            ViewBag.Id = id;
            CargarEspecialidadMedica();
            return View();
        }


        [HttpPost]
        public IActionResult Guardar(EspecialidadDoctorModel doctormodel)
        {
       

            if ( (doctormodel.idDoctor == 0  ) || doctormodel.idEspecailidad ==0 
            //   || (enfermedadmodel.clasificacionId.HasValue && enfermedadmodel.clasificacionId.Value == 0)
                )
            {
                return View();
            }


            var respuesta = EspecialidadDoctorConsultas.Guardar(doctormodel);
            if (respuesta)

                return RedirectToAction("ListaDoctor", new { id = doctormodel.id } );
            else
                return View();
        }



        public IActionResult Eliminar(Int32 id)
        {

            var enfermedadId = EspecialidadDoctorConsultas.ObtenerId(id);
            return View(enfermedadId);

        }


        [HttpPost]
        public IActionResult Eliminar(EspecialidadDoctorModel enfermedadmodel)
        {


            var respuesta = EspecialidadDoctorConsultas.Eliminar(enfermedadmodel.id);
            if (respuesta)
                return RedirectToAction("ListaDoctor", new { id = enfermedadmodel.idDoctor });
            else
                return View();




        }



        public IActionResult Index()
        {
            return View();
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
