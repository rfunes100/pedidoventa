using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace MediWeb.Consultas
{
    public class ExpedienteConsulta
    {


        public List<ExpedienteModel> Obtener(Int32 idDoctor)
        {

            var olista = new List<ExpedienteModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ExpedienteGetById", conexion);
                cmd.Parameters.AddWithValue("id", idDoctor);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {


                        olista.Add(new ExpedienteModel
                        {
                            id = Convert.ToInt32(dataRead["id"]),
                            EnfermedadesID = Convert.ToInt32(dataRead["EnfermedadesID"]),
                            LesionesId = Convert.ToInt32(dataRead["LesionesId"]),
                            CirugiasID = Convert.ToInt32(dataRead["CirugiasID"]),
                            HospitalizacionId = Convert.ToInt32(dataRead["HospitalizacionId"]),
                            MedicosID = Convert.ToInt32(dataRead["MedicosID"]),
                            AlergiasId = Convert.ToInt32(dataRead["AlergiasId"]),
                            MedicamentoId = Convert.ToInt32(dataRead["MedicamentoId"]),
                            ProblemasCronicosId = Convert.ToInt32(dataRead["ProblemasCronicosId"]),
                            VacunaId = Convert.ToInt32(dataRead["VacunaId"]),
                            AntecendenteFamiliarId = Convert.ToInt32(dataRead["AntecendenteFamiliarId"]),                    
                            estado = dataRead["estado"].ToString(),

                            //EspecialidadMedica = new EspecialidadMedicaModel
                            //{
                            //    Nombre = dataRead["Nombre"].ToString(),
                           // }


                        });
                    }
                }



            }

            return olista;

        }



        public ExpedienteModel ObtenerId(Int32 idEnfermera)
        {

            var enfermeraById = new ExpedienteModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ExpedienteGetById", conexion);
                cmd.Parameters.AddWithValue("id", idEnfermera);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {
                        enfermeraById.id = Convert.ToInt32(dataRead["id"]);
                             enfermeraById.EnfermedadesID = Convert.ToInt32(dataRead["EnfermedadesID"]);
                        enfermeraById.LesionesId = Convert.ToInt32(dataRead["LesionesId"]);
                        enfermeraById.CirugiasID = Convert.ToInt32(dataRead["CirugiasID"]);
                        enfermeraById.HospitalizacionId = Convert.ToInt32(dataRead["HospitalizacionId"]);
                        enfermeraById.MedicosID = Convert.ToInt32(dataRead["MedicosID"]);
                        enfermeraById.AlergiasId = Convert.ToInt32(dataRead["AlergiasId"]);
                        enfermeraById.MedicamentoId = Convert.ToInt32(dataRead["MedicamentoId"]);
                        enfermeraById.ProblemasCronicosId = Convert.ToInt32(dataRead["ProblemasCronicosId"]);
                        enfermeraById.VacunaId = Convert.ToInt32(dataRead["VacunaId"]);
                        enfermeraById.AntecendenteFamiliarId = Convert.ToInt32(dataRead["AntecendenteFamiliarId"]);
                        enfermeraById.estado = dataRead["estado"].ToString();


                    }
                }



            }

            return enfermeraById;

        }


        public string Guardar(ExpedienteModel Model)
        {
            string respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("ExpedienteAdd", conexion);
                    cmd.Parameters.AddWithValue("EnfermedadesID", Model.EnfermedadesID);
                    cmd.Parameters.AddWithValue("LesionesId", Model.LesionesId);
                    cmd.Parameters.AddWithValue("CirugiasID", Model.CirugiasID);
                    cmd.Parameters.AddWithValue("HospitalizacionId", Model.HospitalizacionId);
                    cmd.Parameters.AddWithValue("MedicosID", Model.MedicosID);
                    cmd.Parameters.AddWithValue("AlergiasId", Model.AlergiasId);
                    cmd.Parameters.AddWithValue("MedicamentoId", Model.MedicamentoId);
                    cmd.Parameters.AddWithValue("ProblemasCronicosId", Model.ProblemasCronicosId);
                    cmd.Parameters.AddWithValue("VacunaId", Model.VacunaId);
                    cmd.Parameters.AddWithValue("AntecendenteFamiliarId", Model.AntecendenteFamiliarId);
                    cmd.Parameters.AddWithValue("estado", Model.estado);
                  

                    cmd.CommandType = CommandType.StoredProcedure;
                    respuesta = (string)cmd.ExecuteScalar();
                    //     cmd.ExecuteNonQuery();


                }
                return respuesta; 
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = "";
            }

            return respuesta;
        }



        public bool Eliminar(Int32 id)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("ExpedenteDelete", conexion);
                    cmd.Parameters.AddWithValue("id", id);

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
