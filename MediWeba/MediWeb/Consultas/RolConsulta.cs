using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace MediWeb.Consultas
{
    public class RolConsulta
    {


        public List<RolModel> Listar()
        {

            var olista = new List<RolModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("RolGet", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {
                        olista.Add(new RolModel
                        {
                            id = Convert.ToInt32(dataRead["id"]),
                            Nombre = dataRead["Nombre"].ToString(),

                        });
                    }
                }



            }

            return olista;

        }

    }
}
