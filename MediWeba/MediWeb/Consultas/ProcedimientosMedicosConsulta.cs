using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace MediWeb.Consultas
{
    public class ProcedimientosMedicosConsulta
    {


        public List<ProcedimientosMedicosModel> Obtener(Int32 idDoctor)
        {

            var olista = new List<ProcedimientosMedicosModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ProcedimientosMedicosGet", conexion);
                cmd.Parameters.AddWithValue("id", idDoctor);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {


                        olista.Add(new ProcedimientosMedicosModel
                        {
                            id = Convert.ToInt32(dataRead["id"]),
                            EspecialidadId = Convert.ToInt32(dataRead["EspecialidadId"]),

                            Descripcion = dataRead["Descripcion"].ToString(),

                            //EspecialidadMedica = new EspecialidadMedicaModel
                            //{
                            //    Nombre = dataRead["Nombre"].ToString(),

                            //}


                        });
                    }
                }



            }

            return olista;

        }



        public ProcedimientosMedicosModel ObtenerId(Int32 idEnfermera)
        {

            var enfermeraById = new ProcedimientosMedicosModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ProcedimientosMedicosGetById", conexion);
                cmd.Parameters.AddWithValue("id", idEnfermera);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {

                        enfermeraById.id = Convert.ToInt32(dataRead["id"]);
                        enfermeraById.EspecialidadId = Convert.ToInt32(dataRead["EspecialidadId"]);

                        enfermeraById.Descripcion = (dataRead["Descripcion"]).ToString();

                    }
                }



            }

            return enfermeraById;

        }


        public bool Guardar(ProcedimientosMedicosModel Model)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("ProcedimientosMedicosAdd", conexion);
                    cmd.Parameters.AddWithValue("EspecialidadId", Model.EspecialidadId);
                    cmd.Parameters.AddWithValue("Descripcion", Model.Descripcion);


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
                    SqlCommand cmd = new SqlCommand("ProcedimientosMedicosDelete", conexion);
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
