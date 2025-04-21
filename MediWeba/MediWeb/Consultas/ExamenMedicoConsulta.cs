using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace MediWeb.Consultas
{
    public class ExamenMedicoConsulta
    {
        public List<ExamenMedicoModel> Lista(Int32 idDoctor)
        {

            var olista = new List<ExamenMedicoModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ExamenMedicoGet", conexion);
                cmd.Parameters.AddWithValue("idClasificacionExamen", idDoctor);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {


                        olista.Add(new ExamenMedicoModel
                        {
                            Id = Convert.ToInt32(dataRead["id"]),
                            idClasificacionExamen = Convert.ToInt32(dataRead["idClasificacionExamen"]),
                            Nombre = (dataRead["Nombre"]).ToString() ,
                            Sinonimo =(dataRead["Sinonimo"]).ToString(),
                            MuestraPrimaria = (dataRead["MuestraPrimaria"]).ToString(),
                            MuestraAlterna = (dataRead["MuestraAlterna"]).ToString(),
                            VolumenMinimo = (dataRead["VolumenMinimo"]).ToString(),
                            requerimientoId = Convert.ToInt32(dataRead["requerimientoId"]),
                            DiaExamenId = Convert.ToInt32(dataRead["DiaExamenId"]),
                            TiempoEntregaId = Convert.ToInt32(dataRead["TiempoEntregaId"]),
                            MetodologiaId = Convert.ToInt32(dataRead["MetodologiaId"]),

                            estado = (dataRead["estado"]).ToString(),

                            Requerimientos = new requerimientoModel
                            {
                                Descripcion = dataRead["requerimiento"].ToString(),

                            },
                            TiemposEntrega = new TiempoEntregaModel
                            {
                                Descripcion = dataRead["tiempoentrega"].ToString(),

                            },
                            DiasExamen = new DiaExamenModel
                            {
                                Descripcion = dataRead["diaexamen"].ToString(),

                            },
                            Metodologias = new MetodologiaModel
                            {
                                Descripcion = dataRead["metodologia"].ToString(),

                            },


                        });
                    }
                }



            }

            return olista;

        }


        public bool Guardar(ExamenMedicoModel Model)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("ExamenMedicoAdd", conexion);
                    cmd.Parameters.AddWithValue("idClasificacionExamen", Model.idClasificacionExamen);
                    cmd.Parameters.AddWithValue("Nombre", Model.Nombre);
                    cmd.Parameters.AddWithValue("Sinonimo", Model.Sinonimo);
                    cmd.Parameters.AddWithValue("MuestraPrimaria", Model.MuestraPrimaria);
                    cmd.Parameters.AddWithValue("MuestraAlterna", Model.MuestraAlterna);
                    cmd.Parameters.AddWithValue("VolumenMinimo", Model.VolumenMinimo);
                    cmd.Parameters.AddWithValue("requerimientoId", Model.requerimientoId);
                    cmd.Parameters.AddWithValue("DiaExamenId", Model.DiaExamenId);
                    cmd.Parameters.AddWithValue("TiempoEntregaId", Model.TiempoEntregaId);
                    cmd.Parameters.AddWithValue("MetodologiaId", Model.MetodologiaId);
                    cmd.Parameters.AddWithValue("estado", Model.estado);


                    cmd.CommandType = CommandType.StoredProcedure;
                  //  respuesta = (string)cmd.ExecuteScalar();
                         cmd.ExecuteNonQuery();


                }
                respuesta = true; 
                return respuesta;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }

            return respuesta;
        }


        public ExamenMedicoModel Obtener(Int32 idDoctor)
        {

            var enfermeraById = new ExamenMedicoModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ExamenMedicoGetById", conexion);
                cmd.Parameters.AddWithValue("id", idDoctor);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {

                        enfermeraById.Id = Convert.ToInt32(dataRead["id"]);
                        enfermeraById.idClasificacionExamen = Convert.ToInt32(dataRead["idClasificacionExamen"]);
                        enfermeraById.Nombre = dataRead["Nombre"].ToString();
                        enfermeraById.Sinonimo = dataRead["Sinonimo"].ToString();
                        enfermeraById.MuestraPrimaria = dataRead["MuestraPrimaria"].ToString();
                        enfermeraById.MuestraAlterna = dataRead["MuestraAlterna"].ToString();
                        enfermeraById.VolumenMinimo = dataRead["VolumenMinimo"].ToString();                
                        enfermeraById.requerimientoId = Convert.ToInt32(dataRead["requerimientoId"]);
                        enfermeraById.DiaExamenId = Convert.ToInt32(dataRead["DiaExamenId"]);
                        enfermeraById.TiempoEntregaId = Convert.ToInt32(dataRead["TiempoEntregaId"]);
                        enfermeraById.MetodologiaId = Convert.ToInt32(dataRead["MetodologiaId"]);                
                        enfermeraById.estado = dataRead["estado"].ToString();
                    


                    }
                }



            }

            return enfermeraById;

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
                    SqlCommand cmd = new SqlCommand("ExamenMedicoDlete", conexion);
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


        public bool Editar(ExamenMedicoModel doctorModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("ExamenMedicoEdit", conexion);
                    cmd.Parameters.AddWithValue("id", doctorModel.Id);
                    cmd.Parameters.AddWithValue("idClasificacionExamen", doctorModel.idClasificacionExamen);
                    cmd.Parameters.AddWithValue("Nombre", doctorModel.Nombre);
                    
                    cmd.Parameters.AddWithValue("Sinonimo", doctorModel.Sinonimo);
                    cmd.Parameters.AddWithValue("MuestraPrimaria", doctorModel.MuestraPrimaria);
                    cmd.Parameters.AddWithValue("MuestraAlterna", doctorModel.MuestraAlterna);
                    cmd.Parameters.AddWithValue("VolumenMinimo", doctorModel.VolumenMinimo);
                    cmd.Parameters.AddWithValue("requerimientoId", doctorModel.requerimientoId);

                    cmd.Parameters.AddWithValue("DiaExamenId", doctorModel.DiaExamenId);
                    cmd.Parameters.AddWithValue("TiempoEntregaId", doctorModel.TiempoEntregaId);
                    cmd.Parameters.AddWithValue("MetodologiaId", doctorModel.MetodologiaId);
                    cmd.Parameters.AddWithValue("estado", doctorModel.estado);
              
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
