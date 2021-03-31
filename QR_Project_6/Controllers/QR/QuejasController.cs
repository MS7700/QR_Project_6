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
    public class QuejasController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Quejas
        public ActionResult Index()
        {
            return View(db.Quejas.ToList());
        }

        // GET: Quejas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Queja queja = db.Quejas.Find(id);
            if (queja == null)
            {
                return HttpNotFound();
            }
            return View(queja);
        }

        // GET: Quejas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quejas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QRID,UserNameID,Fecha,Comentario")] Queja queja)
        {
            if (ModelState.IsValid)
            {
                db.Quejas.Add(queja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(queja);
        }

        // GET: Quejas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Queja queja = db.Quejas.Find(id);
            if (queja == null)
            {
                return HttpNotFound();
            }
            return View(queja);
        }

        // POST: Quejas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QRID,UserNameID,Fecha,Comentario")] Queja queja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(queja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(queja);
        }

        // GET: Quejas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Queja queja = db.Quejas.Find(id);
            if (queja == null)
            {
                return HttpNotFound();
            }
            return View(queja);
        }

        // POST: Quejas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Queja queja = db.Quejas.Find(id);
            db.Quejas.Remove(queja);
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
