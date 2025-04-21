using MediWeb.Datos;
using MediWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace MediWeb.Consultas
{
    public class PedidoConsultas
    {


        public long Guardar(PedidoEncModel pedidoModel)
        {
            long pedidoId = 0;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("PedidoEncaAdd", conexion);

                    // Agregar los parámetros requeridos por el procedimiento almacenado
                    cmd.Parameters.AddWithValue("Rtn", pedidoModel.Rtn);
                    cmd.Parameters.AddWithValue("CAI", pedidoModel.CAI);
                    cmd.Parameters.AddWithValue("PacienteId", pedidoModel.PacienteId);
                    cmd.Parameters.AddWithValue("Total", pedidoModel.Total);
              

                    // Parámetros de salida para ISV, TotalIsv y el ID generado
                    cmd.Parameters.Add("ISV", SqlDbType.Decimal).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("TotalIsv", SqlDbType.Decimal).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("InsertedId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    // Ejecutar el procedimiento almacenado
                    cmd.ExecuteNonQuery();

                    // Leer los valores de salida
                    pedidoModel.ISV = Convert.ToDecimal(cmd.Parameters["ISV"].Value);
                    pedidoModel.TotalIsv = Convert.ToDecimal(cmd.Parameters["TotalIsv"].Value);
                    pedidoId = Convert.ToInt64(cmd.Parameters["InsertedId"].Value);
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                pedidoId = 0; // Retornar 0 si ocurre un error
            }

            return pedidoId;
        }




        public bool GuardarDetalle(PedidoDetalleModel detalleModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("pedidoDetAdd", conexion);

                    // Agregar los parámetros requeridos por el procedimiento almacenado
                    cmd.Parameters.AddWithValue("ProductoID", detalleModel.ProductoID);
                    cmd.Parameters.AddWithValue("Precio", detalleModel.Precio);
                    cmd.Parameters.AddWithValue("Cantidad", detalleModel.Cantidad);
                    cmd.Parameters.AddWithValue("Total", detalleModel.Total);
                    cmd.Parameters.AddWithValue("Totalisv", 0);
                    cmd.Parameters.AddWithValue("ISV",0);                  
                    cmd.Parameters.AddWithValue("PedidoencabezadoId", detalleModel.PedidoEncabezadoId);

                    cmd.CommandType = CommandType.StoredProcedure;

                    // Ejecutar el procedimiento almacenado
                    cmd.ExecuteNonQuery();
              

                    respuesta = true;
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }

            return respuesta;
        }


        public List<PedidoInfo> ObtenerPedidos(DateTime fechaInicio, DateTime fechaFin, long? clienteId, string estado)
        {
            var pedidos = new List<PedidoInfo>();
            var cn = new Conexion();


            using (var connection = new SqlConnection(cn.GetCadenaSQL()))
            {
                using (var command = new SqlCommand("pedidosinfo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros
                    command.Parameters.AddWithValue("@fechaini", fechaInicio);
                    command.Parameters.AddWithValue("@fechafin", fechaFin);
                    command.Parameters.AddWithValue("@estado", estado);

                    if (clienteId.HasValue)
                    {
                        command.Parameters.AddWithValue("@cliente", clienteId.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@cliente", DBNull.Value);
                    }

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pedido = new PedidoInfo
                            {
                             
                                NombrePaciente = reader.GetString(reader.GetOrdinal("NombrePaciente")),
                                NombreMedicamento = reader.GetString(reader.GetOrdinal("NombreMedicamento")),
                                Precio = reader.GetDecimal(reader.GetOrdinal("precio")),
                                Cantidad = reader.GetInt32(reader.GetOrdinal("cantidad")),
                                Total = reader.GetDecimal(reader.GetOrdinal("total")),
                                productid = reader.GetInt64(reader.GetOrdinal("productid")),
                                FechaCreacion = Convert.ToDateTime(reader["fechacreacion"].ToString())


                            };

                            pedidos.Add(pedido);
                        }
                    }
                }
            }

            return pedidos;
        }


        public List<StockProductoModel> ObtenerStocks()
        {
            var stocks = new List<StockProductoModel>();
            var cn = new Conexion();

            using (var connection = new SqlConnection(cn.GetCadenaSQL()))
            {
                using (var command = new SqlCommand("stockproductos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stocks.Add(new StockProductoModel
                            {
                                MedicamentoID = reader.GetInt64(reader.GetOrdinal("MedicamentoID")),
                                NombreMedicamento = reader.GetString(reader.GetOrdinal("NombreMedicamento")),
                                TotalComprado = reader.GetInt32(reader.GetOrdinal("TotalComprado")),
                                TotalVendido = reader.GetInt32(reader.GetOrdinal("TotalVendido")),
                                StockActual = reader.GetInt32(reader.GetOrdinal("StockActual")),
                                Diferencia = reader.GetInt32(reader.GetOrdinal("Diferencia")),
                                DiferenciaReal = reader.GetInt32(reader.GetOrdinal("DiferenciaReal"))
                            });
                        }
                    }
                }
            }

            return stocks;
        }



    }
}
