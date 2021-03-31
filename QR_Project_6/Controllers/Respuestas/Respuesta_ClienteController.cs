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
    public class Respuesta_ClienteController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Respuesta_Cliente
        public ActionResult Index()
        {
            return View(db.Respuesta_Clientes.ToList());
        }

        // GET: Respuesta_Cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta_Cliente respuesta_Cliente = db.Respuesta_Clientes.Find(id);
            if (respuesta_Cliente == null)
            {
                return HttpNotFound();
            }
            return View(respuesta_Cliente);
        }

        // GET: Respuesta_Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Respuesta_Cliente/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RespuestaID,Valoracion,Fecha,Detalle")] Respuesta_Cliente respuesta_Cliente)
        {
            if (ModelState.IsValid)
            {
                db.Respuesta_Clientes.Add(respuesta_Cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(respuesta_Cliente);
        }

        // GET: Respuesta_Cliente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta_Cliente respuesta_Cliente = db.Respuesta_Clientes.Find(id);
            if (respuesta_Cliente == null)
            {
                return HttpNotFound();
            }
            return View(respuesta_Cliente);
        }

        // POST: Respuesta_Cliente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RespuestaID,Valoracion,Fecha,Detalle")] Respuesta_Cliente respuesta_Cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(respuesta_Cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(respuesta_Cliente);
        }

        // GET: Respuesta_Cliente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta_Cliente respuesta_Cliente = db.Respuesta_Clientes.Find(id);
            if (respuesta_Cliente == null)
            {
                return HttpNotFound();
            }
            return View(respuesta_Cliente);
        }

        // POST: Respuesta_Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Respuesta_Cliente respuesta_Cliente = db.Respuesta_Clientes.Find(id);
            db.Respuesta_Clientes.Remove(respuesta_Cliente);
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
