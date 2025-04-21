using MediWeb.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using MediWeb.Datos;


namespace MediWeb.Consultas
{
    public class ClasificacionEnfermedadesConsultas
    {



        public List<ClasificacionEnfermedadesModel> Listar()
        {

            var olista = new List<ClasificacionEnfermedadesModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ClasificacionEnfermedadesGet", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {


                        olista.Add(new ClasificacionEnfermedadesModel
                        {
                            Id = Convert.ToInt32(dataRead["Id"]),
                            Descripcion = dataRead["Descripcion"].ToString()


                        });
                    }
                }



            }

            return olista;

        }


    }
}
