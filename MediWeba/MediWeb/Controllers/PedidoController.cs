using MediWeb.Consultas;
using MediWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;


namespace MediWeb.Controllers
{
    public class PedidoController : Controller
    {


        MedicamentoConsulta MedicamentoConsultas = new MedicamentoConsulta();
        PacienteConsulta PacienteConsultas = new PacienteConsulta();
        PedidoConsultas PedidoConsulta = new PedidoConsultas();





        public IActionResult StockProducto()
        {
            // Obtener los datos del procedimiento almacenado
            List<StockProductoModel> stocks = PedidoConsulta.ObtenerStocks();

            return View(stocks);
        }

        public IActionResult Reporte()
        {

            CargarClientes();

            return View();
        }

     
        [HttpPost]
        public IActionResult Reporte(DateTime FechaInicio, DateTime FechaFin, long? ClienteId, string estado )
        {
            // Filtrar las compras por rango de fechas
            var compras = PedidoConsulta.ObtenerPedidos(FechaInicio, FechaFin, ClienteId, "FACTURADO");


            ViewBag.FechaInicio = FechaInicio.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = FechaFin.ToString("yyyy-MM-dd");
            ViewBag.ClienteId = ClienteId;

            return View(compras);
        }


        [HttpGet]
        public IActionResult CrearPedido()
        {

            CargarClientes();
            CargarCategorias();

            return View();
        }



        [HttpPost]
        public IActionResult ExportarExcel(DateTime FechaInicio, DateTime FechaFin, long? clienteid, string estado)
        {

            var compras = PedidoConsulta.ObtenerPedidos(FechaInicio, FechaFin, clienteid, "FACTURADO");


            ViewBag.FechaInicio = FechaInicio.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = FechaFin.ToString("yyyy-MM-dd");


            var stream = new MemoryStream();

            using (var workbook = new ClosedXML.Excel.XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reporte de pedidos facturados");


          

                // Encabezados
                worksheet.Cell(1, 1).Value = "Cliente";
                worksheet.Cell(1, 2).Value = "Artículo";
                worksheet.Cell(1, 3).Value = "Cantidad";
                worksheet.Cell(1, 4).Value = "Precio";
                worksheet.Cell(1, 5).Value = "Total";
                worksheet.Cell(1, 6).Value = "Fecha";

                // Datos
                for (int i = 0; i < compras.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = compras[i].NombrePaciente;
                    worksheet.Cell(i + 2, 2).Value = compras[i].NombreMedicamento; // Cambia esto si tienes un nombre de artículo
                    worksheet.Cell(i + 2, 3).Value = compras[i].Cantidad;
                    worksheet.Cell(i + 2, 4).Value = compras[i].Precio;
                    worksheet.Cell(i + 2, 5).Value = compras[i].Total;
                    worksheet.Cell(i + 2, 6).Value = compras[i].FechaCreacion.HasValue? compras[i].FechaCreacion.Value.ToString("yyyy-MM-dd"): "N/A";
                }

                // Calcular totales
                int totalRow = compras.Count + 2; // Fila después de los datos
                worksheet.Cell(totalRow, 2).Value = "Totales:";
                worksheet.Cell(totalRow, 3).Value = compras.Sum(c => c.Cantidad); // Total de cantidad vendida
                worksheet.Cell(totalRow, 5).Value = compras.Sum(c => c.Total); // Total de ventas



                // Ajustar columnas automáticamente
                worksheet.Columns().AdjustToContents();

                // Guardar en el stream
                workbook.SaveAs(stream);
            }

            // Reiniciar la posición del stream para que se lea desde el principio
            stream.Position = 0;

            string excelName = $"Reporte_Compras_{FechaInicio:yyyyMMdd}_a_{FechaFin:yyyyMMdd}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }



        [HttpPost]
        public IActionResult ImprimirFactura(PedidoEncModel pedido, List<PedidoDetalleModel> detalles)
        {
            try
            {


                decimal total = detalles.Sum(d => d.Total); // Calcular el total
                                                            // Generar el PDF
                string nombrePaciente = "marcos";

                // Retornar el PDF como archivo descargable
                string nombreArchivo = $"Factura_{nombrePaciente}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
              
                // Guardar el encabezado del pedido y obtener el ID generado
              

              
               

                   
                    // Enviar un mensaje de éxito a la vista
                    //  TempData["SuccessMessage"] = "Factura generada exitosamente.";


                    // Generar el PDF
                    var pdfResult = GenerarPDF(pedido, detalles, nombrePaciente, total);
                    // Enviar un mensaje de éxito a la vista
                    TempData["SuccessMessage"] = "Factura generada exitosamente.";
                    // Retornar el PDF como archivo descargable
                    return File(pdfResult, "application/pdf", $"Factura_{nombrePaciente}_{DateTime.Now:yyyyMMddHHmmss}.pdf");


                    // Generar el PDF
                    return GenerarYDescargarPDF(pedido, detalles, nombrePaciente, total);
                    TempData["SuccessMessage"] = "Pedido guardado correctamente.";
                    return RedirectToAction("CrearPedido");


                    return View();
                    //  return Json(new { success = true, message = "Pedido guardado correctamente." });
             
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al generar la factura: {ex.Message}" });

            }
        }


        [HttpPost]
        public IActionResult GuardarPedido(PedidoEncModel pedido, List<PedidoDetalleModel> detalles)
        {
            try
            {


                foreach (var detalle in detalles)
                {
                    var producto = MedicamentoConsultas.Listar().FirstOrDefault(p => p.Id == detalle.ProductoID);

                    if (detalle.Cantidad <= 0)
                    {
                        return Json(new { success = false, message = $"La cantidad del producto {producto?.nombre} no puede ser 0 o menor." });
                    }

                    if (producto != null && detalle.Cantidad > producto.Cantidad)
                    {
                        return Json(new { success = false, message = $"La cantidad del producto {producto.nombre} supera el stock disponible ({producto.Cantidad})." });
                    }
                }

                // Guardar el encabezado del pedido y obtener el ID generado
                long pedidoId = GuardarPedidoEncabezado(pedido);

                decimal total = 0;


                if (pedidoId > 0)
                {
                    // Recorrer los detalles y guardarlos
                    foreach (var detalle in detalles)
                    {
                        GuardarPedidoDetalle(detalle, pedidoId);
                        total = total + detalle.Total;
                    }

                    string nombrePaciente = "marcos";

                    // Enviar un mensaje de éxito a la vista
                    //  TempData["SuccessMessage"] = "Factura generada exitosamente.";


                    // Generar el PDF
                    //     var pdfResult = GenerarPDF(pedido, detalles, nombrePaciente, total);
                    // Enviar un mensaje de éxito a la vista
                    //   TempData["SuccessMessage"] = "Factura generada exitosamente.";
                    // Retornar el PDF como archivo descargable
                    //                    return File(pdfResult, "application/pdf", $"Factura_{nombrePaciente}_{DateTime.Now:yyyyMMddHHmmss}.pdf");


                    // Generar el PDF
                    //  return GenerarYDescargarPDF(pedido, detalles, nombrePaciente, total);}}      return Json(new { success = false, message = $"La cantidad del producto {producto?.nombre} no puede ser 0 o menor." });
                    return Json(new { success = true, message = $"se agrego pedido exitosamente" });

                    TempData["SuccessMessage"] = "Pedido guardado correctamente.";
                    return RedirectToAction("CrearPedido");


                    return View();
                  //  return Json(new { success = true, message = "Pedido guardado correctamente." });
                }
                else
                {
                    return Json(new { success = false, message = "Error al guardar el encabezado del pedido." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        private long GuardarPedidoEncabezado(PedidoEncModel pedido)
        {

            long pedidoId = PedidoConsulta.Guardar(pedido);

            if (pedidoId > 0)
            {
                Console.WriteLine($"Pedido guardado con éxito. ID: {pedidoId}");
            }
            else
            {
                Console.WriteLine("Error al guardar el pedido.");
            }

            return pedidoId;
       
        }

        private void GuardarPedidoDetalle(PedidoDetalleModel detalle, long pedidoId)
        {
            // Lógica para llamar al procedimiento almacenado pedidoDetAdd
            // Asignar el ID del encabezado al detalle
            detalle.PedidoEncabezadoId = pedidoId;

            // Llamar al método GuardarDetalle para guardar el detalle en la base de datos
            var resultado = PedidoConsulta.GuardarDetalle(detalle);

            if (!resultado)
            {
                throw new Exception("Error al guardar el detalle del pedido.");
            }
        }

        public IActionResult ExportarPDF(long id)
        {
            // Lógica para generar el PDF del pedido
            return File(GenerarPDF(id), "application/pdf", "Pedido.pdf");
        }

        private byte[] GenerarPDF(long id)
        {
            // Lógica para generar el PDF usando iTextSharp o cualquier otra biblioteca
            return new byte[0]; // Simulación
        }


        [HttpGet]
        public JsonResult ObtenerPrecioProducto(long productoId)
        {
            // Simulación: Obtén el producto desde la base de datos
            var producto = MedicamentoConsultas.Listar().FirstOrDefault(p => p.Id == productoId);

            if (producto != null)
            {
                return Json(new { precio = producto.Precio, nombre = producto.nombre  });
            }

            return Json(new { precio = 0 }); // Si no se encuentra el producto, devuelve 0
        }



        [NonAction]
        private void CargarCategorias()
        {
            var generos = MedicamentoConsultas.Listar();


            //    var enfermeraLista = EnfermedadesConsultas.Listar();
            ViewBag.Categorias = new SelectList(generos, "Id", "nombre");

        }

        [NonAction]
        private void CargarClientes()
        {

            var generos = PacienteConsultas.Listar() ?? new List<PacienteModel>(); // Manejar null
            ViewBag.clientes = new SelectList(generos, "id", "Nombre");

         //   var generos = PacienteConsultas.Listar();
            //    var enfermeraLista = EnfermedadesConsultas.Listar();
           // ViewBag.clientes = new SelectList(generos, "id", "Nombre");

        }


        private byte[] GenerarPDF(PedidoEncModel pedido, List<PedidoDetalleModel> detalles, string nombrePaciente, decimal total)
        {
            using (var stream = new MemoryStream())
            {
                var pdfDocument = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.DefaultTextStyle(x => x.FontSize(12));
                        page.Header().Text("Factura de Pedido").FontSize(20).Bold().AlignCenter();
                        page.Content().Column(column =>
                        {
                            // Información del paciente
                            column.Item().Text($"Paciente: {nombrePaciente}").FontSize(14);
                            column.Item().Text($"RTN: {pedido.Rtn}").FontSize(14);
                            column.Item().Text($"Fecha: {DateTime.Now:yyyy-MM-dd HH:mm:ss}").FontSize(14);

                            column.Item().Text("\n"); // Espacio

                            // Tabla de detalles
                            column.Item().Table(table =>
                            {
                                // Definir columnas
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(1); // Cantidad
                                    columns.RelativeColumn(3); // Producto
                                    columns.RelativeColumn(2); // Precio
                                    columns.RelativeColumn(2); // Total
                                });

                                // Encabezados de la tabla
                                table.Header(header =>
                                {
                                    header.Cell().Text("Cantidad").Bold();
                                    header.Cell().Text("Producto").Bold();
                                    header.Cell().Text("Precio").Bold();
                                    header.Cell().Text("Total").Bold();
                                });

                                // Agregar filas con los detalles
                                foreach (var detalle in detalles)
                                {
                                    table.Cell().Text(detalle.Cantidad.ToString());
                                    table.Cell().Text(detalle.nombre); // Cambia esto si tienes el nombre del producto
                                    table.Cell().Text(detalle.Precio.ToString("C"));
                                    table.Cell().Text(detalle.Total.ToString("C"));
                                }
                            });

                            column.Item().Text("\n"); // Espacio

                            // Total en el pie
                            column.Item().AlignRight().Text($"Total: {total.ToString("C")}").FontSize(14).Bold();
                        });
                    });
                });

                pdfDocument.GeneratePdf(stream);
                return stream.ToArray();
            }
        }




        public IActionResult GenerarYDescargarPDF(PedidoEncModel pedido, List<PedidoDetalleModel> detalles, string nombrePaciente , Decimal total)
        {
            try
            {
                // Crear el documento PDF
                var pdfDocument = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.DefaultTextStyle(x => x.FontSize(12));
                        page.Header().Text("Factura de Pedido").FontSize(20).Bold().AlignCenter();
                        page.Content().Column(column =>
                        {
                            // Información del paciente
                            column.Item().Text($"Paciente: {nombrePaciente}").FontSize(14);
                            column.Item().Text($"RTN: {pedido.Rtn}").FontSize(14);
                            column.Item().Text($"Fecha: {DateTime.Now:yyyy-MM-dd HH:mm:ss}").FontSize(14);

                            column.Item().Text("\n"); // Espacio

                            // Tabla de detalles
                            column.Item().Table(table =>
                            {
                                // Definir columnas
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(1); // Cantidad
                                    columns.RelativeColumn(3); // Producto
                                    columns.RelativeColumn(2); // Precio
                                    columns.RelativeColumn(2); // Total
                                });

                                // Encabezados de la tabla
                                table.Header(header =>
                                {
                                    header.Cell().Text("Cantidad").Bold();
                                    header.Cell().Text("Producto").Bold();
                                    header.Cell().Text("Precio").Bold();
                                    header.Cell().Text("Total").Bold();
                                });

                                // Agregar filas con los detalles
                                foreach (var detalle in detalles)
                                {
                                    table.Cell().Text(detalle.Cantidad.ToString());
                                    table.Cell().Text(detalle.nombre); // Cambia esto si tienes el nombre del producto
                                    table.Cell().Text(detalle.Precio.ToString("C"));
                                    table.Cell().Text(detalle.Total.ToString("C"));
                                }
                            });

                            column.Item().Text("\n"); // Espacio

                            // Total en el pie
                            column.Item().AlignRight().Text($"Total: {total.ToString("C")}").FontSize(14).Bold();
                        });
                    });
                });

                // Generar el PDF en memoria
                using (var stream = new MemoryStream())
                {
                    pdfDocument.GeneratePdf(stream);

                    // Preparar el archivo para la descarga
                    string nombreArchivo = $"Factura_{nombrePaciente}_{DateTime.Now:yyyyMMddHHmmss}.pdf";

                   // return stream.ToArray();
                   return File(stream.ToArray(), "application/pdf", nombreArchivo);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al generar el PDF: {ex.Message}";
                return RedirectToAction("CrearPedido");
            }
        }




    }
}
