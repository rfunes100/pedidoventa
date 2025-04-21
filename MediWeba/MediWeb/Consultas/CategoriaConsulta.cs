using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace MediWeb.Consultas
{
    public class CategoriaConsulta
    {


        public List<CategoriaModel> Listar()
        {

            var olista = new List<CategoriaModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("categoriaGet", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {


                        olista.Add(new CategoriaModel
                        {
                            Id = Convert.ToInt32(dataRead["Id"]),
                            Descripcion = dataRead["Descripcion"].ToString()


                        });
                    }
                }



            }

            return olista;

        }


        public bool Guardar(CategoriaModel enfermeraModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("categoriaAdd", conexion);
                    cmd.Parameters.AddWithValue("Descripcion", enfermeraModel.Descripcion);
                    cmd.Parameters.AddWithValue("estado", "A");

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



        public bool Editar(CategoriaModel enfermeraModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("CategoriaEdit", conexion);
                    cmd.Parameters.AddWithValue("Id", enfermeraModel.Id);
                    cmd.Parameters.AddWithValue("Descripcion", enfermeraModel.Descripcion);
                    cmd.Parameters.AddWithValue("estado", enfermeraModel.estado);


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


        public bool Eliminar(Int32 idEnfermera)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("Categoriadel", conexion);
                    cmd.Parameters.AddWithValue("Id", idEnfermera);

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


        public CategoriaModel Obtener(Int32 idEnfermera)
        {

            var enfermeraById = new CategoriaModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Categoriagetid", conexion);
                cmd.Parameters.AddWithValue("Id", idEnfermera);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {

                        enfermeraById.Id = Convert.ToInt32(dataRead["Id"]);
                        enfermeraById.Descripcion = dataRead["Descripcion"].ToString();
                        enfermeraById.estado = dataRead["estado"].ToString();
                    


                    }
                }



            }

            return enfermeraById;

        }



    }
}
