using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace MediWeb.Consultas
{
    public class TiempoEntregaConsulta
    {

        public List<TiempoEntregaModel> Listar()
        {

            var olista = new List<TiempoEntregaModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("TiempoEntregaGet", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {
                        olista.Add(new TiempoEntregaModel
                        {
                            Id = Convert.ToInt32(dataRead["id"]),
                            Descripcion = dataRead["Descripcion"].ToString(),
                        
                            estado = dataRead["estado"].ToString()

                        });
                    }
                }



            }

            return olista;

        }
    }
}
