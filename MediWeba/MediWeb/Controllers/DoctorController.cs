using Humanizer;
using MediWeb.Consultas;
using MediWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

namespace MediWeb.Controllers
{
    public class DoctorController : Controller
    {



        DoctorConsulta DoctorConsultas = new DoctorConsulta();
        EspecialidadMedicaConsulta EspecialidadMedicaConsultas = new EspecialidadMedicaConsulta();
        GeneroConsulta GeneroConsultas = new GeneroConsulta();



        public IActionResult Listar()
        {
            var enfermeraLista = DoctorConsultas.Listar();
            return View(enfermeraLista);
        }


        public IActionResult Guardar()
        {

          
            CargarGenero();
            return View();
        }


        [HttpPost]
        public IActionResult Guardar(DoctorModel doctormodel)
        {
            doctormodel.estado = "A";
            doctormodel.especialidadid = 1;
            doctormodel.idProcedimientoDoctor = 1;
    

            if (string.IsNullOrEmpty(doctormodel.Nombre) || string.IsNullOrEmpty(doctormodel.Apellido)
                || string.IsNullOrEmpty(doctormodel.Telefono) || string.IsNullOrEmpty(doctormodel.TelefonoTrabajo)
                  || string.IsNullOrEmpty(doctormodel.Correo) || (doctormodel.Sexo == 0 )
            //   || (enfermedadmodel.clasificacionId.HasValue && enfermedadmodel.clasificacionId.Value == 0)
                )
            {
                return View();
            }


            var respuesta = DoctorConsultas.Guardar(doctormodel);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }


        public IActionResult Editar(Int32 idEnfermedad)
        {

             CargarEspecialidadMedica();
            var enfermedadid = DoctorConsultas.Obtener(idEnfermedad);

            ViewBag.doctor = idEnfermedad;
            return View(enfermedadid);

        }




        public IActionResult Eliminar(Int32 idEnfermedad)
        {

            var enfermedadId = DoctorConsultas.Obtener(idEnfermedad);
            return View(enfermedadId);

        }


        public IActionResult Detalle(Int32 idEnfermedad)
        {

            // CargarEspecialidadMedica();
            var enfermedadid = DoctorConsultas.Obtener(idEnfermedad);

            return View(enfermedadid);

        }


        [NonAction]
        private void CargarEspecialidadMedica()
        {
            var categories = EspecialidadMedicaConsultas.Listar();


            //    var enfermeraLista = EnfermedadesConsultas.Listar();
            ViewBag.Categories = new SelectList(categories, "Id", "Nombre");

        }


        [NonAction]
        private void CargarGenero()
        {
            var generos = GeneroConsultas.Listar();


            //    var enfermeraLista = EnfermedadesConsultas.Listar();
            ViewBag.Generos = new SelectList(generos, "id", "Descripcion");

        }


    }
}
