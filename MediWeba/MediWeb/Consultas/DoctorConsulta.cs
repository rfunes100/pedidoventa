using MediWeb.Datos;
using MediWeb.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;



namespace MediWeb.Consultas
{

    public class DoctorConsulta
    {



        public List<DoctorModel> Listar()
        {

            var olista = new List<DoctorModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("DoctoresGet", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {


                        olista.Add(new DoctorModel
                        {
                            id = Convert.ToInt32(dataRead["id"]),
                            Nombre = dataRead["Nombre"].ToString(),
                            Apellido = dataRead["Apellido"].ToString(),
                            especialidadid = Convert.ToInt32( dataRead["especialidadid"]) ,
                            fecha_Ingreso = Convert.ToDateTime( dataRead["fecha_Ingreso"]),
                            Telefono = dataRead["Telefono"].ToString(),
                            TelefonoTrabajo = dataRead["TelefonoTrabajo"].ToString(),
                            Correo = dataRead["Correo"].ToString(),
                            estado = dataRead["estado"].ToString(),
                            idProcedimientoDoctor = Convert.ToInt32(dataRead["idProcedimientoDoctor"]) ,
                            Sexo = Convert.ToInt32( dataRead["Sexo"])


                        });
                    }
                }



            }

            return olista;

        }

        public DoctorModel Obtener(Int32 idDoctor)
        {

            var enfermeraById = new DoctorModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("DoctoresGetById", conexion);
                cmd.Parameters.AddWithValue("id", idDoctor);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {

                        enfermeraById.id = Convert.ToInt32(dataRead["id"]);
                            enfermeraById.Nombre = dataRead["Nombre"].ToString();
                        enfermeraById.Apellido = dataRead["Apellido"].ToString();
                        enfermeraById.especialidadid = Convert.ToInt32(dataRead["especialidadid"]);
                        enfermeraById.fecha_Ingreso = Convert.ToDateTime(dataRead["fecha_Ingreso"]);
                        enfermeraById.Telefono = dataRead["Telefono"].ToString();
                        enfermeraById.TelefonoTrabajo = dataRead["TelefonoTrabajo"].ToString();
                        enfermeraById.Correo = dataRead["Correo"].ToString();
                        enfermeraById.estado = dataRead["estado"].ToString();
                        enfermeraById.idProcedimientoDoctor = Convert.ToInt32(dataRead["idProcedimientoDoctor"]);
                        enfermeraById.Sexo = Convert.ToInt32( dataRead["Sexo"] );

                    }
                }



            }

            return enfermeraById;

        }

        public bool Guardar(DoctorModel doctorModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("DoctoresAdd", conexion);
                    cmd.Parameters.AddWithValue("Nombre", doctorModel.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", doctorModel.Apellido);
                    cmd.Parameters.AddWithValue("especialidadid", doctorModel.especialidadid);
                    cmd.Parameters.AddWithValue("fecha_Ingreso", doctorModel.fecha_Ingreso);
                    cmd.Parameters.AddWithValue("Telefono", doctorModel.Telefono);
                    cmd.Parameters.AddWithValue("TelefonoTrabajo ", doctorModel.TelefonoTrabajo);
                    cmd.Parameters.AddWithValue("Correo", doctorModel.Correo);
                    cmd.Parameters.AddWithValue("estado", doctorModel.estado);
                    cmd.Parameters.AddWithValue("idProcedimientoDoctor", doctorModel.idProcedimientoDoctor);
                    cmd.Parameters.AddWithValue("Sexo", doctorModel.Sexo);

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



        public bool Editar(DoctorModel doctorModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("DoctorEdit", conexion);
                    cmd.Parameters.AddWithValue("Nombre", doctorModel.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", doctorModel.Apellido);
                    cmd.Parameters.AddWithValue("especialidadid", doctorModel.estado);
                    cmd.Parameters.AddWithValue("fecha_Ingreso", doctorModel.fecha_Ingreso);
                    cmd.Parameters.AddWithValue("Telefono", doctorModel.Telefono);
                    cmd.Parameters.AddWithValue("TelefonoTrabajo ", doctorModel.TelefonoTrabajo);
                    cmd.Parameters.AddWithValue("Correo", doctorModel.Correo);
                    cmd.Parameters.AddWithValue("estado", doctorModel.estado);
                    cmd.Parameters.AddWithValue("idProcedimientoDoctor", doctorModel.idProcedimientoDoctor);
                    cmd.Parameters.AddWithValue("Sexo", doctorModel.Sexo);

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
                    SqlCommand cmd = new SqlCommand("DoctorDelete", conexion);
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
