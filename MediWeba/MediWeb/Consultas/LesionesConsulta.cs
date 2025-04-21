using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace MediWeb.Consultas
{
    public class LesionesConsulta
    {

        public List<LesionesModel> Listar()
        {

            var olista = new List<LesionesModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("LesionesGet", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {


                        olista.Add(new LesionesModel
                        {
                            id = Convert.ToInt32(dataRead["id"]),
                            Parte = dataRead["Parte"].ToString(),
                            Descripcion = dataRead["Descripcion"].ToString(),
                            FechaLesion = Convert.ToDateTime( dataRead["FechaLesion"] ),                      
                            Estado = dataRead["Estado"].ToString(),
                        });
                    }
                }



            }

            return olista;

        }


    }
}
