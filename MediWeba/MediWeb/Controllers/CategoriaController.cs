using MediWeb.Consultas;
using MediWeb.Datos;
using MediWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediWeb.Controllers
{
    public class CategoriaController : Controller
    {

        CategoriaConsulta CategoriaConsultas = new CategoriaConsulta();

       
        public IActionResult Listar()
        {
            var enfermeraLista = CategoriaConsultas.Listar();
            return View(enfermeraLista);
        }


        public IActionResult Guardar()
        {


            return View();
        }

        [HttpPost]
        public IActionResult Guardar(CategoriaModel enfermeramodel)
        {

            enfermeramodel.estado = "A";
            enfermeramodel.Id = 1;


            if (string.IsNullOrEmpty(enfermeramodel.estado) || enfermeramodel.estado.Length > 10)
            {
                ModelState.AddModelError("Estado", "El campo Estado es inválido.");
            }

            if (string.IsNullOrEmpty(enfermeramodel.Descripcion) || enfermeramodel.Descripcion.Length > 2000)
            {
                ModelState.AddModelError("Descripcion", "El campo Descripcion es inválido.");
            }

            if (enfermeramodel.Id <= 0)
            {
                ModelState.AddModelError("Id", "El campo Id debe ser mayor a 0.");
            }

           


            var respuesta = CategoriaConsultas.Guardar(enfermeramodel);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Editar(Int32 Id)
        {

            var enfermeraId = CategoriaConsultas.Obtener(Id);
            return View(enfermeraId);

        }


        [HttpPost]
        public IActionResult Editar(CategoriaModel enfermeramodel)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}


            var respuesta = CategoriaConsultas.Editar(enfermeramodel);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();

        }




        public IActionResult Eliminar(Int32 idEnfermera)
        {

            var enfermeraId = CategoriaConsultas.Obtener(idEnfermera);
            return View(enfermeraId);

        }


        [HttpPost]
        public IActionResult Eliminar(EnfermeraModel enfermeramodel)
        {


            var respuesta = CategoriaConsultas.Eliminar(enfermeramodel.id);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();




        }







    }
}
