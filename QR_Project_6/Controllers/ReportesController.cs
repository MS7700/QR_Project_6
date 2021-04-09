using QR_Project_6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;

namespace QR_Project_6.Controllers
{
    [Authorize(Roles = "Admin, Empleado")]
    public class ReportesController : Controller
    {
        private QR_Model db = new QR_Model();
        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }
        // GET: Reportes/Satisfaccion
        public ActionResult Satisfaccion()
        {
            SatisfaccionViewModel satisfaccions = InitializarSatisfaccionViewModel();

            return View(satisfaccions);
        }

        private SatisfaccionViewModel InitializarSatisfaccionViewModel()
        {
            SatisfaccionViewModel satisfaccions = new SatisfaccionViewModel();
            satisfaccions.Valoracions = new List<ValoracionViewModel>();
            List<Valoracion> valoracions = db.Valoracions.ToList();
            decimal puntuacion = 0;

            int total = db.Respuesta_Clientes.Count();
            foreach (var valoracion in valoracions)
            {
                ValoracionViewModel satisfaccion = new ValoracionViewModel();
                satisfaccion.Valoracion = valoracion.Descripcion;
                satisfaccion.Cantidad = db.Respuesta_Clientes.Count(r => r.ValoracionID == valoracion.ValoracionID);
                satisfaccion.Porcentaje = getPorcentaje((decimal)satisfaccion.Cantidad, (decimal)total);

                puntuacion += satisfaccion.Cantidad * valoracion.Valor;

                satisfaccions.Valoracions.Add(satisfaccion);
            }

            satisfaccions.GradoSatisfaccion = getGradoPonderado(puntuacion, valoracions.Max(v => v.Valor), total);
            return satisfaccions;
        }



        // GET: Reportes/Export
        public void ExportSatisfaccion()
        {
            SatisfaccionViewModel model = InitializarSatisfaccionViewModel();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "Valoración";
            Sheet.Cells["B1"].Value = "Cantidad";
            Sheet.Cells["C1"].Value = "Porcentaje";
            int row = 2;
            foreach (var item in model.Valoracions)
            {

                Sheet.Cells[string.Format("A{0}", row)].Value = item.Valoracion;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.Cantidad;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.Porcentaje;
                row++;
            }

            row++;
            Sheet.Cells[string.Format("A{0}", row)].Value = "Grado de satisfacción: ";
            Sheet.Cells[string.Format("B{0}", row)].Value = string.Format("{0}%", model.GradoSatisfaccion);

            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        private decimal getGradoPonderado(decimal puntuacionTotal, decimal valorMaximo, int cantidad)
        {
            return puntuacionTotal / (valorMaximo * cantidad) * 100;
        }
        private decimal getPorcentaje(decimal cantidad, decimal total)
        {
            return (cantidad / total) * 100;
        }

        public ActionResult Quejas()
        {
            QuejasReportViewModel quejasReport = InitializarQuejasReportViewModel();

            return View(quejasReport);
        }

        private QuejasReportViewModel InitializarQuejasReportViewModel()
        {
            QuejasReportViewModel quejasReport = new QuejasReportViewModel();
            InitializarQuejas(quejasReport);

            InitializarEstadosQuejas(quejasReport);

            return quejasReport;
        }

        private void InitializarEstadosQuejas(QuejasReportViewModel quejasReport)
        {
            int totalQR = db.Quejas.Count();
            List<EstadoViewModel> estados = new List<EstadoViewModel>();
            db.Estado_QRs.ToList().ForEach(
                e =>
                {
                    EstadoViewModel tempEstado = new EstadoViewModel();
                    tempEstado.Estado = e.Descripcion;
                    tempEstado.Cantidad = db.Quejas.Count(q => q.Estado_QR_EstadoID == e.EstadoID);
                    tempEstado.Porcentaje = getPorcentaje((decimal)tempEstado.Cantidad, (decimal)totalQR);

                    estados.Add(tempEstado);
                }
                );

            quejasReport.Estados = estados;
        }

        private void InitializarQuejas(QuejasReportViewModel quejasReport)
        {
            List<QuejaReportViewModel> quejas = new List<QuejaReportViewModel>();
            db.Quejas
                .Include(q => q.Cliente)
                .Include(q => q.Empleado)
                .Include(q => q.Sucursal)
                .Include(q => q.Departamento)
                .Include(q => q.Tipo_Queja)
                .Include(q => q.Estado_QR)
                .ToList().ForEach(
                q =>
                {
                    QuejaReportViewModel tempQueja = new QuejaReportViewModel();
                    tempQueja.Numero = q.QRID.GetValueOrDefault();
                    tempQueja.Fecha = q.Fecha.GetValueOrDefault();
                    tempQueja.Cliente = q.Cliente.Nombre + " " + q.Cliente.Apellido;
                    tempQueja.Departamento = q.Departamento.Nombre;
                    tempQueja.Sucursal = q.Sucursal.Nombre;
                    tempQueja.Empleado = q.Empleado.Nombre + " " + q.Empleado.Apellido;
                    tempQueja.Estado = q.Estado_QR.Descripcion;
                    tempQueja.Tipo = q.Tipo_Queja.Descripcion;
                    tempQueja.Comentario = q.Comentario;

                    quejas.Add(tempQueja);
                }
                );
            quejasReport.Quejas = quejas;
        }

        public void ExportQuejas()
        {
            QuejasReportViewModel quejasReport = InitializarQuejasReportViewModel();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "Número de queja";
            Sheet.Cells["B1"].Value = "Fecha";
            Sheet.Cells["C1"].Value = "Cliente";
            Sheet.Cells["D1"].Value = "Departamento";
            Sheet.Cells["E1"].Value = "Sucursal";
            Sheet.Cells["F1"].Value = "Empleado";
            Sheet.Cells["G1"].Value = "Estado";
            Sheet.Cells["H1"].Value = "Tipo de queja";
            Sheet.Cells["I1"].Value = "Comentario";
            int row = 2;
            foreach (var item in quejasReport.Quejas)
            {

                Sheet.Cells[string.Format("A{0}", row)].Value = item.Numero;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.Fecha;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.Cliente;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.Departamento;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.Sucursal;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.Empleado;
                Sheet.Cells[string.Format("G{0}", row)].Value = item.Estado;
                Sheet.Cells[string.Format("H{0}", row)].Value = item.Tipo;
                Sheet.Cells[string.Format("I{0}", row)].Value = item.Comentario;
                row++;
            }

            row++;

            Sheet.Cells[string.Format("A{0}", row)].Value = "Estado";
            Sheet.Cells[string.Format("B{0}", row)].Value = "Cantidad";
            Sheet.Cells[string.Format("C{0}", row)].Value = "Porcentaje";
            
            foreach (var item in quejasReport.Estados)
            {

                Sheet.Cells[string.Format("A{0}", row)].Value = item.Estado;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.Cantidad;
                Sheet.Cells[string.Format("C{0}", row)].Value = string.Format("{0}%", item.Porcentaje);
                row++;
            }

            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }


        public ActionResult Reclamaciones()
        {
            ReclamacionesReportViewModel reclamacionesReport = InitializarReclamacionesReportViewModel();

            return View(reclamacionesReport);
        }

        private ReclamacionesReportViewModel InitializarReclamacionesReportViewModel()
        {
            ReclamacionesReportViewModel reclamacionesReport = new ReclamacionesReportViewModel();
            InitializarReclamaciones(reclamacionesReport);

            InitializarEstadosReclamaciones(reclamacionesReport);

            return reclamacionesReport;
        }

        private void InitializarEstadosReclamaciones(ReclamacionesReportViewModel reclamacionesReport)
        {
            int totalQR = db.Reclamacions.Count();
            List<EstadoViewModel> estados = new List<EstadoViewModel>();
            db.Estado_QRs.ToList().ForEach(
                e =>
                {
                    EstadoViewModel tempEstado = new EstadoViewModel();
                    tempEstado.Estado = e.Descripcion;
                    tempEstado.Cantidad = db.Reclamacions.Count(q => q.Estado_QR_EstadoID == e.EstadoID);
                    tempEstado.Porcentaje = getPorcentaje((decimal)tempEstado.Cantidad, (decimal)totalQR);

                    estados.Add(tempEstado);
                }
                );

            reclamacionesReport.Estados = estados;
        }

        private void InitializarReclamaciones(ReclamacionesReportViewModel reclamacionesReport)
        {
            List<ReclamacionReportViewModel> reclamaciones = new List<ReclamacionReportViewModel>();
            db.Reclamacions
                .Include(q => q.Cliente)
                .Include(q => q.Empleado)
                .Include(q => q.Sucursal)
                .Include(q => q.Departamento)
                .Include(q => q.Tipo_Reclamacion)
                .Include(q => q.Estado_QR)
                .ToList().ForEach(
                q =>
                {
                    ReclamacionReportViewModel tempReclamacion = new ReclamacionReportViewModel();
                    tempReclamacion.Numero = q.QRID.GetValueOrDefault();
                    tempReclamacion.Fecha = q.Fecha.GetValueOrDefault();
                    tempReclamacion.Cliente = q.Cliente.Nombre + " " + q.Cliente.Apellido;
                    tempReclamacion.Departamento = q.Departamento.Nombre;
                    tempReclamacion.Sucursal = q.Sucursal.Nombre;
                    tempReclamacion.Empleado = q.Empleado.Nombre + " " + q.Empleado.Apellido;
                    tempReclamacion.Estado = q.Estado_QR.Descripcion;
                    tempReclamacion.Tipo = q.Tipo_Reclamacion.Descripcion;
                    tempReclamacion.Comentario = q.Comentario;

                    reclamaciones.Add(tempReclamacion);
                }
                );
            reclamacionesReport.Reclamaciones = reclamaciones;
        }

        public void ExportReclamaciones()
        {
            ReclamacionesReportViewModel reclamacionesReport = InitializarReclamacionesReportViewModel();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "Número de reclamacion";
            Sheet.Cells["B1"].Value = "Fecha";
            Sheet.Cells["C1"].Value = "Cliente";
            Sheet.Cells["D1"].Value = "Departamento";
            Sheet.Cells["E1"].Value = "Sucursal";
            Sheet.Cells["F1"].Value = "Empleado";
            Sheet.Cells["G1"].Value = "Estado";
            Sheet.Cells["H1"].Value = "Tipo de reclamacion";
            Sheet.Cells["I1"].Value = "Comentario";
            int row = 2;
            foreach (var item in reclamacionesReport.Reclamaciones)
            {

                Sheet.Cells[string.Format("A{0}", row)].Value = item.Numero;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.Fecha;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.Cliente;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.Departamento;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.Sucursal;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.Empleado;
                Sheet.Cells[string.Format("G{0}", row)].Value = item.Estado;
                Sheet.Cells[string.Format("H{0}", row)].Value = item.Tipo;
                Sheet.Cells[string.Format("I{0}", row)].Value = item.Comentario;
                row++;
            }

            row++;

            Sheet.Cells[string.Format("A{0}", row)].Value = "Estado";
            Sheet.Cells[string.Format("B{0}", row)].Value = "Cantidad";
            Sheet.Cells[string.Format("C{0}", row)].Value = "Porcentaje";

            foreach (var item in reclamacionesReport.Estados)
            {

                Sheet.Cells[string.Format("A{0}", row)].Value = item.Estado;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.Cantidad;
                Sheet.Cells[string.Format("C{0}", row)].Value = string.Format("{0}%", item.Porcentaje);
                row++;
            }

            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }
        
    }
}
