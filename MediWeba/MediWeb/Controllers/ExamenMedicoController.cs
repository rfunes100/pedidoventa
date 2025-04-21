using MediWeb.Consultas;
using MediWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediWeb.Controllers
{
    public class ExamenMedicoController : Controller
    {

        ExamenMedicoConsulta ExamenMedicoConsultas = new ExamenMedicoConsulta();
        requerimientoConsulta requerimientoConsultas = new requerimientoConsulta();
        DiaExamenConsulta DiaExamenConsultas = new DiaExamenConsulta();
        TiempoEntregaConsulta TiempoEntregaConsultas = new TiempoEntregaConsulta();
        MetodologiaConsulta MetodologiaConsultaa = new MetodologiaConsulta();
        UsuarioConsultas usuarioconsulta = new UsuarioConsultas();






        public IActionResult Lista(Int32 id, string buscar , int pagina = 1 )
        {


            string usuario = usuarioconsulta.usuarioLogueado(HttpContext);
            string rol = usuarioconsulta.UsuarioRol(usuario);

            if(rol != "ADMIN")
            {
                return RedirectToAction("PedirPermiso", "Home");

            }
            var cantidadRegistros = 10;
            ViewBag.ClasificacionExamen = id;
            
            var enfermeraLista = ExamenMedicoConsultas.Lista(id);


            if (!string.IsNullOrEmpty(buscar))
            {
              //  enfermeraLista = enfermeraLista.Where(examen => examen.Nombre.Contains(buscar));
                enfermeraLista = enfermeraLista.Where(s => (s.Nombre ?? "").Contains(buscar) || (s.MuestraAlterna ?? "").Contains(buscar)
                || (s.MuestraPrimaria ?? "").Contains(buscar) || ( s.Sinonimo ?? "").Contains(buscar)
                || (s.Requerimientos?.Descripcion ?? "").Contains(buscar) || (s.DiasExamen?.Descripcion ?? "").Contains(buscar)
                || (s.TiemposEntrega?.Descripcion ?? "").Contains(buscar) || (s.Metodologias?.Descripcion ?? "").Contains(buscar) ).ToList();

            }
            var enfermerasLista = enfermeraLista.OrderBy(x => x.Id )
                .Skip((pagina - 1 ) * cantidadRegistros)
                .Take(cantidadRegistros).ToList();

            var totalRegistros = enfermeraLista.Count();

            var modelo = new VistasPaginacion<ExamenMedicoModel>();
            modelo.ExamenMedico = enfermerasLista;
            modelo.PaginaActual = pagina;
            modelo.TOtalRegistros = totalRegistros;
            modelo.RegistrosPagina = cantidadRegistros;

            return View(modelo);
            //  return View(enfermeraLista);
        }


        public IActionResult Guardar(Int32 id)
        {

            ViewBag.ClasificacionExamen = id;
            Cargarrequerimiento();
            CargarDiaExamen();
            CargarMetodologia();
            CargarTiempoEntrega();


            return View();
        }


        [HttpPost]
        public IActionResult Guardar(ExamenMedicoModel doctormodel)
        {

            doctormodel.estado = "REGISTRADO";
        

            if ((doctormodel.requerimientoId == 0) || doctormodel.DiaExamenId == 0)
            {
                return View();
            }


            var respuesta = ExamenMedicoConsultas.Guardar(doctormodel);
            if (Convert.ToInt32(respuesta) != 0)

                return RedirectToAction("Lista", "ExamenMedico", new { id = doctormodel.idClasificacionExamen });

            else
                return View();
        }


        public IActionResult Eliminar(Int32 idEnfermedad)
        {

            var enfermedadId = ExamenMedicoConsultas.Obtener(idEnfermedad);
            ViewBag.Expediente = enfermedadId.idClasificacionExamen;
            return View(enfermedadId);

        }


        [HttpPost]
        public IActionResult Eliminar(ExamenMedicoModel enfermedadmodel)
        {


            var respuesta = ExamenMedicoConsultas.Eliminar(enfermedadmodel.Id);
            if (respuesta)
                return RedirectToAction("Lista", "ExamenMedico", new { id = enfermedadmodel.idClasificacionExamen });
            else
                return View();




        }


        public IActionResult Editar(Int32 idEnfermedad)
        {
            Cargarrequerimiento();
            CargarDiaExamen();
            CargarMetodologia();
            CargarTiempoEntrega();

        

            var enfermedadId = ExamenMedicoConsultas.Obtener(idEnfermedad);;
            ViewBag.Expediente = enfermedadId.idClasificacionExamen;
            return View(enfermedadId);

        }


        [HttpPost]
        public IActionResult Editar(ExamenMedicoModel enfermeramodel)
        {


            if ((enfermeramodel.requerimientoId == 0) || enfermeramodel.DiaExamenId == 0)
            {
                return View();
            }


            var respuesta = ExamenMedicoConsultas.Editar(enfermeramodel);
            if (respuesta)
                return RedirectToAction("Lista", "ExamenMedico", new { id = enfermeramodel.idClasificacionExamen });
            else
                return View();

        }





        [NonAction]
        private void Cargarrequerimiento()
        {
            var requerimiento = requerimientoConsultas.Listar();


            //    var enfermeraLista = EnfermedadesConsultas.Listar();
            ViewBag.requerimientos = new SelectList(requerimiento, "Id", "Descripcion");

        }

        [NonAction]
        private void CargarDiaExamen()
        {
            var requerimiento = DiaExamenConsultas.Listar();


            //    var enfermeraLista = EnfermedadesConsultas.Listar();
            ViewBag.DiaExamenes = new SelectList(requerimiento, "Id", "Descripcion");

        }

        [NonAction]
        private void CargarTiempoEntrega()
        {
            var requerimiento = TiempoEntregaConsultas.Listar();


            //    var enfermeraLista = EnfermedadesConsultas.Listar();
            ViewBag.TiemposEntrega = new SelectList(requerimiento, "Id", "Descripcion");

        }

        [NonAction]
        private void CargarMetodologia()
        {
            var requerimiento = MetodologiaConsultaa.Listar();


            //    var enfermeraLista = EnfermedadesConsultas.Listar();
            ViewBag.Metodologias = new SelectList(requerimiento, "Id", "Descripcion");

        }




    }
}
