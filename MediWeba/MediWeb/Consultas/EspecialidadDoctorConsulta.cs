using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace MediWeb.Consultas
{
    public class EspecialidadDoctorConsulta
    {


        public List<EspecialidadDoctorModel> Obtener(Int32 idDoctor)
        {

            var olista = new List<EspecialidadDoctorModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("EspecialidadDoctorgetByDoctor", conexion);
                cmd.Parameters.AddWithValue("id", idDoctor);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {


                        olista.Add(new EspecialidadDoctorModel
                        {
                            id = Convert.ToInt32(dataRead["id"]),
                            idDoctor = Convert.ToInt32(dataRead["idDoctor"]) ,
                         
                            idEspecailidad = Convert.ToInt32(dataRead["idEspecailidad"]),

                            EspecialidadMedica = new EspecialidadMedicaModel
                            {
                                Nombre = dataRead["Nombre"].ToString(),

                            }


                        });
                    }
                }



            }

            return olista;

        }



        public EspecialidadDoctorModel ObtenerId(Int32 idEnfermera)
        {

            var enfermeraById = new EspecialidadDoctorModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("EspecialidadByd", conexion);
                cmd.Parameters.AddWithValue("id", idEnfermera);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {

                        enfermeraById.id = Convert.ToInt32(dataRead["id"]);
                        enfermeraById.idDoctor = Convert.ToInt32(dataRead["idDoctor"]);

                        enfermeraById.idEspecailidad = Convert.ToInt32(dataRead["idEspecailidad"]);

                    }
                }



            }

            return enfermeraById;

        }


        public bool Guardar(EspecialidadDoctorModel Model)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("EspecialidadDoctorAdd", conexion);
                    cmd.Parameters.AddWithValue("idDoctor", Model.idDoctor);
                    cmd.Parameters.AddWithValue("idEspecailidad", Model.idEspecailidad);
                

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



        public bool Eliminar(Int32 id)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("EspecialidadDoctorDelete", conexion);
                    cmd.Parameters.AddWithValue("id", id);
                   
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
