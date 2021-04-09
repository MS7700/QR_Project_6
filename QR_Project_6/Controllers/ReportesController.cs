using QR_Project_6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QR_Project_6.Controllers
{
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
                satisfaccion.Porcentaje = getPorcentaje( (decimal)satisfaccion.Cantidad, (decimal)total);

                puntuacion += satisfaccion.Cantidad * valoracion.Valor;

                satisfaccions.Valoracions.Add(satisfaccion);
            }

            satisfaccions.GradoSatisfaccion = getGradoPonderado(puntuacion, valoracions.Max(v => v.Valor), total);

            return View(satisfaccions);
        }

        // GET: Reportes/Export
        public ActionResult ExportSatisfaccion()
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

            return View(satisfaccions);
        }

        private decimal getGradoPonderado(decimal puntuacionTotal, decimal valorMaximo, int cantidad)
        {
            return puntuacionTotal / (valorMaximo * cantidad) * 100;
        }
        private decimal getPorcentaje(decimal cantidad, decimal total)
        {
            return (cantidad / total) * 100;
        }
        



        // GET: Reportes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reportes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reportes/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reportes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reportes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reportes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reportes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
