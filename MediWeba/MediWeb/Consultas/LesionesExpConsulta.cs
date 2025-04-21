using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace MediWeb.Consultas
{
    public class LesionesExpConsulta
    {

        public List<LesionesExpModel> Lista(Int32 idDoctor)
        {

            var olista = new List<LesionesExpModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("LesionesExpByExp", conexion);
                cmd.Parameters.AddWithValue("ExpedenteId", idDoctor);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {


                        olista.Add(new LesionesExpModel
                        {
                            id = Convert.ToInt32(dataRead["id"]),
                            ExpedenteId = Convert.ToInt32(dataRead["ExpedenteId"]),

                            LesionesId = Convert.ToInt32(dataRead["LesionesId"]),
                            fechaLesion = Convert.ToDateTime(dataRead["fechaLesion"]),


                            Lesiones = new LesionesModel
                            {
                                 Descripcion = dataRead["Descripcion"].ToString(),

                            }


                        });
                    }
                }



            }

            return olista;

        }


        public LesionesExpModel Obtener(Int32 idDoctor)
        {

            var enfermeraById = new LesionesExpModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("LesionesExpGetById", conexion);
                cmd.Parameters.AddWithValue("id", idDoctor);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {

                        enfermeraById.id = Convert.ToInt32(dataRead["id"]);
                        enfermeraById.ExpedenteId = Convert.ToInt32(dataRead["ExpedenteId"]);
                        enfermeraById.LesionesId = Convert.ToInt32(dataRead["LesionesId"]);
                        enfermeraById.fechaLesion = Convert.ToDateTime(dataRead["fechaLesion"]);



                    }
                }



            }

            return enfermeraById;

        }

        public bool Guardar(LesionesExpModel doctorModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("LesionesExpAdd", conexion);
                    cmd.Parameters.AddWithValue("ExpedenteId", doctorModel.ExpedenteId);
                    cmd.Parameters.AddWithValue("LesionesId ", doctorModel.LesionesId);
                    cmd.Parameters.AddWithValue("fechaLesion", doctorModel.fechaLesion);
                 

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


        public bool Eliminar(Int32 idDoctor)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("LesionesExpDelete", conexion);
                    cmd.Parameters.AddWithValue("id", idDoctor);

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
