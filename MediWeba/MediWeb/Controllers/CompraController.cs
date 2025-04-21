using MediWeb.Consultas;
using MediWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.IO;


namespace MediWeb.Controllers
{
    public class CompraController : Controller
    {
        CompraConsulta CategoriaConsultas = new CompraConsulta();

        MedicamentoConsulta MedicamentoConsultas = new MedicamentoConsulta();



        public IActionResult Listar()
        {
            var enfermeraLista = CategoriaConsultas.Listar();
            //  return View(enfermeraLista);
            return View();
        }



        public IActionResult Guardar()
        {
            CargarCategorias();

            return View();
        }

        [HttpPost]
        public IActionResult Guardar(CompraModel enfermeramodel)
        {

          

          



            if (enfermeramodel.ArticuloId <= 0)
            {
                ModelState.AddModelError("ArticuloId", "El campo ArticuloId debe ser mayor a 0.");
            }


            if (enfermeramodel.Cantidad <= 0)
            {
                ModelState.AddModelError("Cantidad", "El campo Cantidad debe ser mayor a 0.");
            }

           




            var respuesta = CategoriaConsultas.Guardar(enfermeramodel);
            if (respuesta)
                return RedirectToAction("Reporte");
            else
                return View();
        }



        [NonAction]
        private void CargarCategorias()
        {
            var generos = MedicamentoConsultas.Listar();


            //    var enfermeraLista = EnfermedadesConsultas.Listar();
            ViewBag.Categorias = new SelectList(generos, "Id", "nombre");

        }


        public IActionResult Reporte()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Reporte(DateTime FechaInicio, DateTime FechaFin)
        {
            // Filtrar las compras por rango de fechas
            var compras = CategoriaConsultas.Listar()
                .Where(c => c.Fecha >= FechaInicio && c.Fecha <= FechaFin)
                .ToList();

            ViewBag.FechaInicio = FechaInicio.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = FechaFin.ToString("yyyy-MM-dd");

            return View(compras);
        }


        [HttpPost]
        public IActionResult ExportarExcel(DateTime FechaInicio, DateTime FechaFin)
        {
            // Filtrar las compras por rango de fechas
            var compras = CategoriaConsultas.Listar()
                .Where(c => c.Fecha >= FechaInicio && c.Fecha <= FechaFin)
                .ToList();

            var stream = new MemoryStream();

            using (var workbook = new ClosedXML.Excel.XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reporte de Compras");

                // Encabezados
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Artículo";
                worksheet.Cell(1, 3).Value = "Cantidad";
                worksheet.Cell(1, 4).Value = "Fecha";

                // Datos
                for (int i = 0; i < compras.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = compras[i].Id;
                    worksheet.Cell(i + 2, 2).Value = compras[i].ArticuloId; // Cambia esto si tienes un nombre de artículo
                    worksheet.Cell(i + 2, 3).Value = compras[i].Cantidad;
                    worksheet.Cell(i + 2, 4).Value = compras[i].Fecha.ToString("yyyy-MM-dd");
                }

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


    }
}
