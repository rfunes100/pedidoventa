using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace MediWeb.Consultas
{
    public class ClasificacionExamenMedicoConsulta
    {


        public List<ClasificacionExamenMedicoModel> Listar()
        {

            var olista = new List<ClasificacionExamenMedicoModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ClasificacionExamenMedicoGet", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {


                        olista.Add(new ClasificacionExamenMedicoModel
                        {
                            id = Convert.ToInt32(dataRead["id"]),
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
