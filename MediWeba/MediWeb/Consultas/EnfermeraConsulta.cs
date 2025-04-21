
using MediWeb.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using MediWeb.Datos;
using System.Numerics;

namespace MediWeb.Consultas
{
    public class EnfermeraConsulta
    {
        public List<EnfermeraModel> Listar()
        {

            var olista = new List<EnfermeraModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion. Open();
                SqlCommand cmd = new SqlCommand("EnfermerasGet", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {
                        olista.Add(new EnfermeraModel { 
                            id = Convert.ToInt32( dataRead["id"] ) ,
                            Nombre = dataRead["Nombre"].ToString(),
                            Telefono = dataRead["Telefono"].ToString(),
                            DNI = dataRead["DNI"].ToString(),
                            Direccion = dataRead["Direccion"].ToString()

                        });
                    }
                }

                

            }

            return olista;

        }


        public EnfermeraModel Obtener(Int32 idEnfermera)
        {

            var enfermeraById = new EnfermeraModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("EnfermeraGetId", conexion);
                cmd.Parameters.AddWithValue("id", idEnfermera);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {

                        enfermeraById.id = Convert.ToInt32(dataRead["id"]);
                        enfermeraById.Nombre = dataRead["Nombre"].ToString();
                        enfermeraById.Telefono = dataRead["Telefono"].ToString();
                        enfermeraById.DNI = dataRead["DNI"].ToString();
                        enfermeraById.Direccion = dataRead["Direccion"].ToString();

                       
                    }
                }



            }

            return enfermeraById;

        }

        public bool Guardar( EnfermeraModel enfermeraModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("EnfermeraAdd", conexion);
                    cmd.Parameters.AddWithValue("Nombre", enfermeraModel.Nombre);
                    cmd.Parameters.AddWithValue("Telefono", enfermeraModel.Telefono);
                    cmd.Parameters.AddWithValue("DNI", enfermeraModel.DNI);
                    cmd.Parameters.AddWithValue("Direccion", enfermeraModel.Direccion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

               
                }
                respuesta = true;
            }
            catch ( Exception e )
            {
                string error = e.Message;
                respuesta = false;
            }

            return respuesta;
        }



        public bool Editar(EnfermeraModel enfermeraModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("EnfermeraEdit", conexion);
                    cmd.Parameters.AddWithValue("id", enfermeraModel.id);
                    cmd.Parameters.AddWithValue("Nombre", enfermeraModel.Nombre);
                    cmd.Parameters.AddWithValue("Telefono", enfermeraModel.Telefono);
                    cmd.Parameters.AddWithValue("DNI", enfermeraModel.DNI);
                    cmd.Parameters.AddWithValue("Direccion", enfermeraModel.Direccion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();


                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }

            return respuesta;
        }


        public bool Eliminar(Int32 idEnfermera)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("EnfermeraDelete", conexion);
                    cmd.Parameters.AddWithValue("id", idEnfermera);
              
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();


                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }

            return respuesta;
        }


    }
}
