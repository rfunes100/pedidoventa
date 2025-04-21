using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace MediWeb.Consultas
{
    public class PacienteConsulta
    {

        public List<PacienteModel> Listar()
        {

            var olista = new List<PacienteModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("pacienteGet", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {


                        olista.Add(new PacienteModel
                        {
                            id = Convert.ToInt32(dataRead["id"]),
                            Nombre = dataRead["Nombre"].ToString(),
                            Apellido = dataRead["Apellido"].ToString(),
                            Telefono = dataRead["Telefono"].ToString(),
                            DNI = dataRead["DNI"].ToString(),
                            Correo = dataRead["Correo"].ToString(),
                            Direccion = dataRead["Direccion"].ToString(),
                            ExpedienteId = Convert.ToInt32(dataRead["ExpedienteId"]),
                            FechaCreacion = Convert.ToDateTime(dataRead["FechaCreacion"]),
                            FechaNacimiento = Convert.ToDateTime(dataRead["FechaNacimiento"]),

                            Peso = Convert.ToDecimal(dataRead["Peso"]),
                            altura = Convert.ToDecimal(dataRead["altura"]),

                            estado = dataRead["estado"].ToString(),
                            
                            Sexo = Convert.ToInt32(dataRead["Sexo"])


                        });
                    }
                }



            }

            return olista;

        }

        public PacienteModel Obtener(Int32 idDoctor)
        {

            var enfermeraById = new PacienteModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("pacienteById", conexion);
                cmd.Parameters.AddWithValue("id", idDoctor);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {

                        enfermeraById.id = Convert.ToInt32(dataRead["id"]);
                        enfermeraById.Nombre = dataRead["Nombre"].ToString();
                        enfermeraById.Apellido = dataRead["Apellido"].ToString();
                        enfermeraById.Telefono = dataRead["Telefono"].ToString();
                        enfermeraById.DNI = dataRead["DNI"].ToString();
                        enfermeraById.Correo = dataRead["Correo"].ToString();
                        enfermeraById.Direccion = dataRead["Direccion"].ToString();
                        enfermeraById.ExpedienteId = Convert.ToInt32(dataRead["ExpedienteId"]);
                        enfermeraById.FechaCreacion = Convert.ToDateTime(dataRead["FechaCreacion"]);
                        enfermeraById.FechaNacimiento = Convert.ToDateTime(dataRead["FechaNacimiento"]);
                        enfermeraById.estado = dataRead["estado"].ToString();
                        enfermeraById.Sexo = Convert.ToInt32(dataRead["Sexo"]);
                        enfermeraById.Peso = Convert.ToDecimal(dataRead["Peso"]);                  
                        enfermeraById.altura = Convert.ToInt32(dataRead["altura"]);


                    }
                }



            }

            return enfermeraById;

        }

        public bool Guardar(PacienteModel doctorModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("pacienteAdd", conexion);
                    cmd.Parameters.AddWithValue("Nombre", doctorModel.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", doctorModel.Apellido);
                    cmd.Parameters.AddWithValue("Telefono", doctorModel.Telefono);
                    cmd.Parameters.AddWithValue("DNI", doctorModel.Telefono);
                    cmd.Parameters.AddWithValue("Correo", doctorModel.Correo);
                    cmd.Parameters.AddWithValue("Direccion", doctorModel.Direccion);

                    cmd.Parameters.AddWithValue("ExpedienteId", doctorModel.ExpedienteId);
                    cmd.Parameters.AddWithValue("FechaCreacion", doctorModel.FechaCreacion);
                    cmd.Parameters.AddWithValue("FechaNacimiento", doctorModel.FechaNacimiento);
                    cmd.Parameters.AddWithValue("estado", doctorModel.estado);
                    cmd.Parameters.AddWithValue("Sexo", doctorModel.Sexo);

                    cmd.Parameters.AddWithValue("Peso ", doctorModel.Peso);               
                    cmd.Parameters.AddWithValue("altura", doctorModel.altura);
           
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



        public bool Editar(PacienteModel doctorModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("pacienteEdit", conexion);
                    cmd.Parameters.AddWithValue("id", doctorModel.id);
                    cmd.Parameters.AddWithValue("Nombre", doctorModel.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", doctorModel.Apellido);
                    cmd.Parameters.AddWithValue("Telefono", doctorModel.Telefono);
                    cmd.Parameters.AddWithValue("DNI", doctorModel.Telefono);
                    cmd.Parameters.AddWithValue("Correo", doctorModel.Correo);
                    cmd.Parameters.AddWithValue("Direccion", doctorModel.Direccion);

                    cmd.Parameters.AddWithValue("ExpedienteId", doctorModel.ExpedienteId);
                    cmd.Parameters.AddWithValue("FechaCreacion", doctorModel.FechaCreacion);
                    cmd.Parameters.AddWithValue("FechaNacimiento", doctorModel.FechaNacimiento);
                    cmd.Parameters.AddWithValue("estado", doctorModel.estado);
                    cmd.Parameters.AddWithValue("Sexo", doctorModel.Sexo);

                    cmd.Parameters.AddWithValue("Peso ", doctorModel.Peso);
                    cmd.Parameters.AddWithValue("altura", doctorModel.altura);

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


        public bool Eliminar(Int32 idDoctor)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("pacienteDelete", conexion);
                    cmd.Parameters.AddWithValue("id", idDoctor);

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
