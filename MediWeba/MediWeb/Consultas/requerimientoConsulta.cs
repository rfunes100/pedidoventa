using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace MediWeb.Consultas
{
    public class requerimientoConsulta
    {

        public List<requerimientoModel> Listar()
        {

            var olista = new List<requerimientoModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("requerimientoGet", conexion);
               
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {


                        olista.Add(new requerimientoModel
                        {
                            Id = Convert.ToInt32(dataRead["id"]),
                           
                            Descripcion = dataRead["Descripcion"].ToString(),
                            estado = dataRead["estado"].ToString(),

                            //EspecialidadMedica = new EspecialidadMedicaModel
                            //{
                            //    Nombre = dataRead["Nombre"].ToString(),

                            //}


                        });
                    }
                }



            }

            return olista;

        }

    }
}
