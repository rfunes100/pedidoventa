using MediWeb.Consultas;
using MediWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediWeb.Controllers
{
    public class PacienteController : Controller
    {

        PacienteConsulta PacienteConsultas = new PacienteConsulta();
        GeneroConsulta GeneroConsultas = new GeneroConsulta();
        ExpedienteConsulta ExpedienteConsultas = new ExpedienteConsulta();


        public IActionResult Listar( )
        {
            var enfermeraLista = PacienteConsultas.Listar();
          
            return View(enfermeraLista);
        }

        public IActionResult Guardar(Int32 id)
        {

            ViewBag.Expediente = id;
            CargarGenero();

            return View();
        }


        [HttpPost]
        public IActionResult Guardar(PacienteModel doctormodel)
        {
            doctormodel.estado = "REGISTRADO";
            doctormodel.FechaCreacion = DateTime.Now;
            doctormodel.ExpedienteId = 0;
            doctormodel.Peso = 10;
            doctormodel.altura = 10;




            if ((doctormodel.Apellido == "" ) || doctormodel.Nombre == ""
                || (doctormodel.Telefono == "" ) || doctormodel.DNI == ""
                || (doctormodel.Correo == "" ) || doctormodel.Direccion == ""
              //   || (doctormodel.FechaNacimiento == "" ) || doctormodel.FechaCreacion == ""
                  || (doctormodel.Peso == 0) || doctormodel.altura == 0
                //   || (enfermedadmodel.clasificacionId.HasValue && enfermedadmodel.clasificacionId.Value == 0)
                )
            {
                return View();
            }


            var respuesta = PacienteConsultas.Guardar(doctormodel);
            if (respuesta)

                return RedirectToAction("Listar");
            else
                return View();
        }


        public IActionResult Eliminar(Int32 idEnfermedad)
        {

            var enfermedadId = PacienteConsultas.Obtener(idEnfermedad);
            ViewBag.Expediente = enfermedadId.ExpedienteId;
            return View(enfermedadId);

        }


        [HttpPost]
        public IActionResult Eliminar(PacienteModel enfermedadmodel)
        {


            var respuesta = ExpedienteConsultas.Eliminar(enfermedadmodel.ExpedienteId);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();




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
