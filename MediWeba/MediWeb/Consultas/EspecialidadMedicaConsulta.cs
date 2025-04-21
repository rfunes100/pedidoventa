using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace MediWeb.Consultas
{
    public class EspecialidadMedicaConsulta
    {

        public List<EspecialidadMedicaModel> Listar()
        {

            var olista = new List<EspecialidadMedicaModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("EspecialidadMedicaGet", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {


                        olista.Add(new EspecialidadMedicaModel
                        {
                            id = Convert.ToInt32(dataRead["id"]),
                            Nombre = dataRead["Nombre"].ToString(),
                           Descripcion = dataRead["Descripcion"].ToString()



                        });
                    }
                }



            }

            return olista;

        }





    }
}
