using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QR_Project_6.Models;

namespace QR_Project_6.Controllers.Respuestas
{
    public class Respuesta_EmpleadoController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Respuesta_Empleado
        public ActionResult Index()
        {
            return View(db.Respuesta_Empleados.ToList());
        }

        // GET: Respuesta_Empleado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta_Empleado respuesta_Empleado = db.Respuesta_Empleados.Find(id);
            if (respuesta_Empleado == null)
            {
                return HttpNotFound();
            }
            return View(respuesta_Empleado);
        }

        // GET: Respuesta_Empleado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Respuesta_Empleado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RespuestaID,Fecha,Detalle")] Respuesta_Empleado respuesta_Empleado)
        {
            if (ModelState.IsValid)
            {
                db.Respuesta_Empleados.Add(respuesta_Empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(respuesta_Empleado);
        }

        // GET: Respuesta_Empleado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta_Empleado respuesta_Empleado = db.Respuesta_Empleados.Find(id);
            if (respuesta_Empleado == null)
            {
                return HttpNotFound();
            }
            return View(respuesta_Empleado);
        }

        // POST: Respuesta_Empleado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RespuestaID,Fecha,Detalle")] Respuesta_Empleado respuesta_Empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(respuesta_Empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(respuesta_Empleado);
        }

        // GET: Respuesta_Empleado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta_Empleado respuesta_Empleado = db.Respuesta_Empleados.Find(id);
            if (respuesta_Empleado == null)
            {
                return HttpNotFound();
            }
            return View(respuesta_Empleado);
        }

        // POST: Respuesta_Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Respuesta_Empleado respuesta_Empleado = db.Respuesta_Empleados.Find(id);
            db.Respuesta_Empleados.Remove(respuesta_Empleado);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
