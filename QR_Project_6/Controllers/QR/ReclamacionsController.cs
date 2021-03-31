using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QR_Project_6.Models;

namespace QR_Project_6.Controllers.QR
{
    public class ReclamacionsController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Reclamacions
        public ActionResult Index()
        {
            return View(db.Reclamacions.ToList());
        }

        // GET: Reclamacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamacion reclamacion = db.Reclamacions.Find(id);
            if (reclamacion == null)
            {
                return HttpNotFound();
            }
            return View(reclamacion);
        }

        // GET: Reclamacions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reclamacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QRID,UserNameID,Fecha,Comentario")] Reclamacion reclamacion)
        {
            if (ModelState.IsValid)
            {
                db.Reclamacions.Add(reclamacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reclamacion);
        }

        // GET: Reclamacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamacion reclamacion = db.Reclamacions.Find(id);
            if (reclamacion == null)
            {
                return HttpNotFound();
            }
            return View(reclamacion);
        }

        // POST: Reclamacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QRID,UserNameID,Fecha,Comentario")] Reclamacion reclamacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reclamacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reclamacion);
        }

        // GET: Reclamacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamacion reclamacion = db.Reclamacions.Find(id);
            if (reclamacion == null)
            {
                return HttpNotFound();
            }
            return View(reclamacion);
        }

        // POST: Reclamacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reclamacion reclamacion = db.Reclamacions.Find(id);
            db.Reclamacions.Remove(reclamacion);
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
