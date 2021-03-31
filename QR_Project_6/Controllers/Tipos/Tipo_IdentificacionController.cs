using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QR_Project_6.Models;

namespace QR_Project_6.Controllers.Tipos
{
    public class Tipo_IdentificacionController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Tipo_Identificacion
        public ActionResult Index()
        {
            return View(db.Tipo_Identificacions.ToList());
        }

        // GET: Tipo_Identificacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Identificacion tipo_Identificacion = db.Tipo_Identificacions.Find(id);
            if (tipo_Identificacion == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Identificacion);
        }

        // GET: Tipo_Identificacion/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Tipo_Identificacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoID,Descripcion")] Tipo_Identificacion tipo_Identificacion)
        {
            if (ModelState.IsValid)
            {
                db.Tipo_Identificacions.Add(tipo_Identificacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_Identificacion);
        }

        // GET: Tipo_Identificacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Identificacion tipo_Identificacion = db.Tipo_Identificacions.Find(id);
            if (tipo_Identificacion == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Identificacion);
        }

        // POST: Tipo_Identificacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoID,Descripcion")] Tipo_Identificacion tipo_Identificacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_Identificacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_Identificacion);
        }

        // GET: Tipo_Identificacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Identificacion tipo_Identificacion = db.Tipo_Identificacions.Find(id);
            if (tipo_Identificacion == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Identificacion);
        }

        // POST: Tipo_Identificacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tipo_Identificacion tipo_Identificacion = db.Tipo_Identificacions.Find(id);
            db.Tipo_Identificacions.Remove(tipo_Identificacion);
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
