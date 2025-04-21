using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace MediWeb.Consultas
{
    public class MedicamentoConsulta
    {


        public bool ActualizarPrecio(long id, decimal nuevoPrecio)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("MedicamentoUpdatePrecio", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@NuevoPrecio", nuevoPrecio);

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


        public List<MedicamentoModel> Listar()
        {

            var olista = new List<MedicamentoModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("MedicamentoGet", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {


                        olista.Add(new MedicamentoModel
                        {
                            Id = Convert.ToInt32(dataRead["Id"]),
                            nombre = dataRead["nombre"].ToString(),
                            Descripcion = dataRead["Descripcion"].ToString(),
                            imagen = dataRead["imagen"].ToString(),
                            Precio = Convert.ToDecimal( dataRead["Precio"]),
                            Cantidad = Convert.ToInt16( dataRead["Cantidad"]) ,
                            CategoriaID = Convert.ToInt16( dataRead["CategoriaID"]),
                            


                        });
                    }
                }



            }

            return olista;

        }



        public bool Guardar(MedicamentoModel doctorModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("MedicamentoAdd", conexion);
                    cmd.Parameters.AddWithValue("nombre", doctorModel.nombre);
                    cmd.Parameters.AddWithValue("Descripcion", doctorModel.Descripcion);
                    cmd.Parameters.AddWithValue("imagen", doctorModel.imagen);
                    cmd.Parameters.AddWithValue("Precio", doctorModel.Precio);
                    cmd.Parameters.AddWithValue("Cantidad", doctorModel.Cantidad);
                    cmd.Parameters.AddWithValue("CategoriaID", doctorModel.CategoriaID);
               

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
