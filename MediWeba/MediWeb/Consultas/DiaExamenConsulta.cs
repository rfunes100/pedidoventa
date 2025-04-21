using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace MediWeb.Consultas
{
    public class DiaExamenConsulta
    {




        public List<DiaExamenModel> Listar()
        {

            var olista = new List<DiaExamenModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("DiaExamenGet", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {
                        olista.Add(new DiaExamenModel
                        {
                            Id = Convert.ToInt32(dataRead["id"]),
                            Descripcion = dataRead["Descripcion"].ToString(),
                            dia = dataRead["dia"].ToString(),
                            estado = dataRead["estado"].ToString()

                        });
                    }
                }



            }

            return olista;

        }
    }
}
