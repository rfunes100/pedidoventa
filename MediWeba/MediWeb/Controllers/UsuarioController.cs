using MediWeb.Consultas;
using MediWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

namespace MediWeb.Controllers
{
    public class UsuarioController : Controller
    {

        UsuarioConsultas UsuarioConsulta = new UsuarioConsultas();
         UsuarioConsultas usuarioconsulta = new UsuarioConsultas();
        RolConsulta RolConsultas = new RolConsulta();


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UsuarioModel doctormodel)
        {

          
            if ((doctormodel.Clave == "" ) || doctormodel.Userid == "")
            {
                return View();
            }


            var respuesta = UsuarioConsulta.Loguear(doctormodel);
            if (Convert.ToInt32(respuesta) != 0)
            { 
                    string usuarioid = UsuarioConsulta.UsuarioId(doctormodel);
                HttpContext.Session.SetString("usuarioid", usuarioid);

                //    _cache.Set("usuarioid", "usuarioid");
                 
                return RedirectToAction("Index", "Home");
            }
            else
                TempData["Error"] = "La clave usuario o correo es incorrecta.";
            return View();
        }


        public IActionResult Lista(Int32 id, string buscar, int pagina = 1)
        {


            string usuario = usuarioconsulta.usuarioLogueado(HttpContext);
            string rol = usuarioconsulta.UsuarioRol(usuario);

            if (rol != "ADMIN")
            {
                return RedirectToAction("PedirPermiso", "Home");

            }
            var cantidadRegistros = 10;
            ViewBag.ClasificacionExamen = id;

            var enfermeraLista = usuarioconsulta.Lista();


            if (!string.IsNullOrEmpty(buscar))
            {
                //  enfermeraLista = enfermeraLista.Where(examen => examen.Nombre.Contains(buscar));
                enfermeraLista = enfermeraLista.Where(s => (s.Nombre ?? "").Contains(buscar) || (s.Userid ?? "").Contains(buscar)
                || (s.RolId?.ToString() ).Contains(buscar) || (s.Correo ?? "").Contains(buscar)
                || (s.Roles?.Nombre ?? "").Contains(buscar) ).ToList();

            }
            var enfermerasLista = enfermeraLista.OrderBy(x => x.id)
                .Skip((pagina - 1) * cantidadRegistros)
                .Take(cantidadRegistros).ToList();

            var totalRegistros = enfermeraLista.Count();

            var modelo = new VistasPaginacion<UsuarioModel>();
            modelo.ExamenMedico = enfermerasLista;
            modelo.PaginaActual = pagina;
            modelo.TOtalRegistros = totalRegistros;
            modelo.RegistrosPagina = cantidadRegistros;

            return View(modelo);
            //  return View(enfermeraLista);
        }



        public IActionResult Guardar()
        {


            CargarRoles();
            return View();
        }


        [HttpPost]
        public IActionResult Guardar(UsuarioModel doctormodel)
        {

            if ((!doctormodel.Clave.Equals(doctormodel.estado) ))
            {
                TempData["Error"] = "Las claves no son iguales";
                CargarRoles();
                return View();
            }
 
            if ((string.IsNullOrEmpty(doctormodel.Clave) ) || string.IsNullOrEmpty(doctormodel.Nombre)  ||
                string.IsNullOrEmpty(doctormodel.Userid)|| string.IsNullOrEmpty(doctormodel.Apellido)
                 || doctormodel.Correo == "" || doctormodel.RolId == 0 )
            {
                TempData["Error"] = "debe llenar todos los campos son obligatorios";
                CargarRoles();
                return View();
            }
            doctormodel.estado = "REGISTRADO";
            doctormodel.Huellabimoetrica = "test";
            doctormodel.fechaCreacion = DateTime.Now;


            var respuesta = usuarioconsulta.Guardar(doctormodel);
            if (Convert.ToInt32(respuesta) != 0)

                return RedirectToAction("Lista", "Usuario");

            else
                return View();
        }



        public IActionResult Eliminar(Int32 idEnfermedad)
        {

            var enfermedadId = usuarioconsulta.ObtenerId(idEnfermedad);
         //   ViewBag.Expediente = enfermedadId.id;
            return View(enfermedadId);

        }


        [HttpPost]
        public IActionResult Eliminar(UsuarioModel enfermedadmodel)
        {
            enfermedadmodel.estado = "DESACTIVO";


            var respuesta = usuarioconsulta.Eliminar(enfermedadmodel);
            if (respuesta)
                return RedirectToAction("Lista");
            else
                return View();




        }



        public IActionResult Editar(Int32 idEnfermedad)
        {
            CargarRoles();

            var enfermedadId = usuarioconsulta.ObtenerId(idEnfermedad); ;
           // ViewBag.Expediente = enfermedadId.idClasificacionExamen;
            return View(enfermedadId);

        }


        [HttpPost]
        public IActionResult Editar(UsuarioModel enfermeramodel)
        {


            if ( string.IsNullOrEmpty(enfermeramodel.Nombre) ||
      string.IsNullOrEmpty(enfermeramodel.Userid) || string.IsNullOrEmpty(enfermeramodel.Apellido)
       || enfermeramodel.Correo == "" || enfermeramodel.RolId == 0)
            {
              //  TempData["Error"] = "debe llenar todos los campos son obligatorios";
                CargarRoles();
                return View();
            }


            var respuesta = usuarioconsulta.Editar(enfermeramodel);
            if (respuesta)
                return RedirectToAction("Lista", "Usuario");
            else
                return View();

        }




        [NonAction]
        private void CargarRoles()
        {
            var requerimiento = RolConsultas.Listar();


            //    var enfermeraLista = EnfermedadesConsultas.Listar();
            ViewBag.Roles = new SelectList(requerimiento, "id", "Nombre");

        }


    }
}
