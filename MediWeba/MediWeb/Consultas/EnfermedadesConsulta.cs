using MediWeb.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using MediWeb.Datos;


namespace MediWeb.Consultas
{
    public class EnfermedadesConsulta
    {


        public List<EnfermedadesModel> Listar()
        {

            var olista = new List<EnfermedadesModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("EnfermedadesGet", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {
                    
                    
                        olista.Add(new EnfermedadesModel
                        {
                            Id = Convert.ToInt32(dataRead["Id"]),
                            DescripcionGrupo = dataRead["DescripcionGrupo"].ToString(),
                            DescripcionLarga = dataRead["DescripcionLarga"].ToString(),
                          
                            Clasificaciones = new ClasificacionEnfermedadesModel
                            {
                                Descripcion = dataRead["Descripcion"].ToString(),
                                
                            }
                        

                        });
                    }
                }



            }

            return olista;

        }


        public EnfermedadesModel Obtener(Int32 idEnfermedad)
        {

            var enfermeraById = new EnfermedadesModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("EnfermedadesById", conexion);
                cmd.Parameters.AddWithValue("Id", idEnfermedad);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {

                        enfermeraById.Id = Convert.ToInt32(dataRead["Id"]);
                        enfermeraById.DescripcionGrupo = dataRead["DescripcionGrupo"].ToString();
                        enfermeraById.DescripcionLarga = dataRead["DescripcionLarga"].ToString();
                        enfermeraById.clasificacionId = Convert.ToInt32(dataRead["clasificacionId"]) ;

                        
                    }
                }



            }

            return enfermeraById;

        }

        public bool Guardar(EnfermedadesModel enfermedadesModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("EnfermedadesAdd", conexion);
                    cmd.Parameters.AddWithValue("DescripcionGrupo", enfermedadesModel.DescripcionGrupo);
                    cmd.Parameters.AddWithValue("DescripcionLarga", enfermedadesModel.DescripcionLarga);
                    cmd.Parameters.AddWithValue("estado", enfermedadesModel.estado);
                    cmd.Parameters.AddWithValue("clasificacionId ", enfermedadesModel.clasificacionId);
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



        public bool Editar(EnfermedadesModel enfermedadesModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("EnfermedadesEdit", conexion);
                    cmd.Parameters.AddWithValue("id", enfermedadesModel.Id);
                    cmd.Parameters.AddWithValue("DescripcionGrupo ", enfermedadesModel.DescripcionGrupo);
                    cmd.Parameters.AddWithValue("DescripcionLarga ", enfermedadesModel.DescripcionLarga);
                    cmd.Parameters.AddWithValue("estado", enfermedadesModel.estado);
                    cmd.Parameters.AddWithValue("clasificacionId", enfermedadesModel.clasificacionId);
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


        public bool Eliminar(Int32 idEnfermedad)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("EnfermedadesDelete", conexion);
                    cmd.Parameters.AddWithValue("id", idEnfermedad);

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

    }
}
