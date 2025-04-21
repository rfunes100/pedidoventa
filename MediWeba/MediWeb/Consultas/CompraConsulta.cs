using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace MediWeb.Consultas
{
    public class CompraConsulta
    {

        public List<CompraModel> Listar()
        {

            var olista = new List<CompraModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ComprasMostrar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {


                        olista.Add(new CompraModel
                        {
                            Id = Convert.ToInt32(dataRead["Id"]),
                            ArticuloId = Convert.ToInt32(dataRead["ArticuloId"]),
                            NombreArticulo = dataRead["NombreArticulo"].ToString(),

                            Cantidad = Convert.ToInt32(dataRead["Cantidad"].ToString()),
                            Fecha = Convert.ToDateTime( dataRead["Fecha"].ToString() )



                        });
                    }
                }



            }

            return olista;

        }


        public bool Guardar(CompraModel enfermeraModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("RegistrarCompra", conexion);
                    cmd.Parameters.AddWithValue("ArticuloId", enfermeraModel.ArticuloId);
                    cmd.Parameters.AddWithValue("Cantidad ", enfermeraModel.Cantidad);

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
