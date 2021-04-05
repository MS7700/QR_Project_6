using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QR_Project_6.Models;

namespace QR_Project_6.Controllers
{
    public class Tipo_ReclamacionController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Tipo_Reclamacion
        public ActionResult Index()
        {
            return View(db.Tipo_Reclamacions.ToList());
        }

        // GET: Tipo_Reclamacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Reclamacion tipo_Reclamacion = db.Tipo_Reclamacions.Find(id);
            if (tipo_Reclamacion == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Reclamacion);
        }

        // GET: Tipo_Reclamacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tipo_Reclamacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoID,Descripcion")] Tipo_Reclamacion tipo_Reclamacion)
        {
            if (ModelState.IsValid)
            {
                db.Tipo_Reclamacions.Add(tipo_Reclamacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_Reclamacion);
        }

        // GET: Tipo_Reclamacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Reclamacion tipo_Reclamacion = db.Tipo_Reclamacions.Find(id);
            if (tipo_Reclamacion == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Reclamacion);
        }

        // POST: Tipo_Reclamacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoID,Descripcion")] Tipo_Reclamacion tipo_Reclamacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_Reclamacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_Reclamacion);
        }

        // GET: Tipo_Reclamacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Reclamacion tipo_Reclamacion = db.Tipo_Reclamacions.Find(id);
            if (tipo_Reclamacion == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Reclamacion);
        }

        // POST: Tipo_Reclamacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tipo_Reclamacion tipo_Reclamacion = db.Tipo_Reclamacions.Find(id);
            db.Tipo_Reclamacions.Remove(tipo_Reclamacion);
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
