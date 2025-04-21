using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace MediWeb.Consultas
{
    public class GeneroConsulta
    {

        public List<GeneroModel> Listar()
        {

            var olista = new List<GeneroModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("GeneroGet", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {


                        olista.Add(new GeneroModel
                        {
                            id = Convert.ToInt32(dataRead["id"]),
                            Descripcion = dataRead["Descripcion"].ToString()
                        });
                    }
                }



            }

            return olista;

        }

    }
}
