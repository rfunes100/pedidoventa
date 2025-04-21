using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;



namespace MediWeb.Consultas
{
    public class UsuarioConsultas
    {
        public int Loguear(UsuarioModel Model)
        {
            int respuesta = 0 ;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("UsuarioLogin", conexion);
                    cmd.Parameters.AddWithValue("Clave", Model.Clave);
                    cmd.Parameters.AddWithValue("Userid", Model.Userid);
                  

                    cmd.CommandType = CommandType.StoredProcedure;
                    respuesta = (int)cmd.ExecuteScalar();
                    //cmd.ExecuteNonQuery();


                }
                //respuesta = true;
                return respuesta;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = 0;
            }

            return respuesta;
        }

        public string UsuarioId(UsuarioModel Model)
        {
            string respuesta = "";

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("UsuarioUserid", conexion);
                    cmd.Parameters.AddWithValue("Clave", Model.Clave);
                    cmd.Parameters.AddWithValue("Userid", Model.Userid);


                    cmd.CommandType = CommandType.StoredProcedure;
                    respuesta = (string)cmd.ExecuteScalar();
                    //cmd.ExecuteNonQuery();


                }
                //respuesta = true;
                return respuesta;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = "";
            }

            return respuesta;
        }


        public string UsuarioRol(string Userid)
        {
            string respuesta = "";

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("RolUsuario", conexion);             
                    cmd.Parameters.AddWithValue("Userid", Userid);
                    cmd.CommandType = CommandType.StoredProcedure;
                    respuesta = (string)cmd.ExecuteScalar();
                    //cmd.ExecuteNonQuery();


                }
                //respuesta = true;
                return respuesta;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = "";
            }

            return respuesta;
        }

        public string usuarioLogueado(HttpContext httpContext)
        {
            string respuesta = "";

            respuesta =  httpContext.Session.GetString("usuarioid");
            return respuesta;


        }


        public List<UsuarioModel> Lista( )
        {

            var olista = new List<UsuarioModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("UsuarioGet", conexion);
              
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {


                        olista.Add(new UsuarioModel
                        {
                            id = Convert.ToInt32(dataRead["id"]),
                      
                            Nombre = (dataRead["Nombre"]).ToString(),
                            Userid = (dataRead["Userid"]).ToString(),
                            Correo = (dataRead["correo"]).ToString(),
                          

                            estado = (dataRead["estado"]).ToString(),

                            Roles = new RolModel
                            {
                                Nombre = dataRead["Rol"].ToString(),

                            }
                         

                        });
                    }
                }



            }

            return olista;

        }


        public bool Guardar(UsuarioModel doctorModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("usuarioAdd", conexion);
                    cmd.Parameters.AddWithValue("Userid", doctorModel.Userid);
                    cmd.Parameters.AddWithValue("Nombre", doctorModel.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", doctorModel.Apellido);
                    cmd.Parameters.AddWithValue("Correo", doctorModel.Correo);
                    cmd.Parameters.AddWithValue("Estado", doctorModel.estado);
                    cmd.Parameters.AddWithValue("RolId", doctorModel.RolId);

                    cmd.Parameters.AddWithValue("Clave", doctorModel.Clave);
                    cmd.Parameters.AddWithValue("Huellabimoetrica", doctorModel.Huellabimoetrica);
                    cmd.Parameters.AddWithValue("PreguntaRecuperacion", doctorModel.PreguntaRecuperacion);
                    cmd.Parameters.AddWithValue("RespuestaRecuperacion", doctorModel.RespuestaRecuperacion);
                    cmd.Parameters.AddWithValue("fechaCreacion", doctorModel.fechaCreacion);

           
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();


                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }

            return respuesta;
        }




        public bool Editar(UsuarioModel doctorModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("usuarioEdit", conexion);
                    cmd.Parameters.AddWithValue("id", doctorModel.id);
                    cmd.Parameters.AddWithValue("Userid", doctorModel.Userid);
                    cmd.Parameters.AddWithValue("Nombre"  ,   doctorModel.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", doctorModel.Apellido);
                    cmd.Parameters.AddWithValue("Correo", doctorModel.Correo);
                    cmd.Parameters.AddWithValue("Estado", doctorModel.estado);
                    cmd.Parameters.AddWithValue("RolId", doctorModel.RolId);

                    cmd.Parameters.AddWithValue("Huellabimoetrica", doctorModel.Huellabimoetrica);
                    cmd.Parameters.AddWithValue("PreguntaRecuperacion", doctorModel.PreguntaRecuperacion);
                    cmd.Parameters.AddWithValue("RespuestaRecuperacion", doctorModel.RespuestaRecuperacion);       

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();


                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }

            return respuesta;
        }



        public bool Eliminar(UsuarioModel doctorModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("usuarioDelete", conexion);
                    cmd.Parameters.AddWithValue("id", doctorModel.id);
                    cmd.Parameters.AddWithValue("Estado", doctorModel.estado);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();


                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }

            return respuesta;
        }



        public UsuarioModel ObtenerId(Int32 idEnfermera)
        {

            var enfermeraById = new UsuarioModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("UsuarioGetById", conexion);
                cmd.Parameters.AddWithValue("id", idEnfermera);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {

                        enfermeraById.id = Convert.ToInt32(dataRead["id"]);                     
                        enfermeraById.Nombre = (dataRead["Nombre"]).ToString();
                        enfermeraById.Userid = (dataRead["Userid"]).ToString();
                        enfermeraById.Correo = (dataRead["correo"]).ToString();
                        enfermeraById.estado = (dataRead["estado"]).ToString();
                        enfermeraById.Apellido = (dataRead["Apellido"]).ToString();
                        enfermeraById.RolId = Convert.ToInt32((dataRead["RolId"]));

                        enfermeraById.Huellabimoetrica = (dataRead["Huellabimoetrica"]).ToString();
                        enfermeraById.PreguntaRecuperacion = (dataRead["PreguntaRecuperacion"]).ToString();
                        enfermeraById.RespuestaRecuperacion = (dataRead["RespuestaRecuperacion"]).ToString();
                        



                    }
                }



            }

            return enfermeraById;

        }


    }
}
