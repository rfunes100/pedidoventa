using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace MediWeb.Consultas
{
    public class MetodologiaConsulta
    {

        public List<MetodologiaModel> Listar()
        {

            var olista = new List<MetodologiaModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("MetodologiaGet", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {
                        olista.Add(new MetodologiaModel
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
