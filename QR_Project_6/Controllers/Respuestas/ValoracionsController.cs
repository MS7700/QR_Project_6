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
    [Authorize(Roles = "Admin")]
    public class ValoracionsController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Valoracions
        public ActionResult Index()
        {
            return View(db.Valoracions.ToList());
        }

        // GET: Valoracions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Valoracion valoracion = db.Valoracions.Find(id);
            if (valoracion == null)
            {
                return HttpNotFound();
            }
            return View(valoracion);
        }

        // GET: Valoracions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Valoracions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ValoracionID,Descripcion,Valor")] Valoracion valoracion)
        {
            if(db.Valoracions.Any(v => v.Valor == valoracion.Valor))
            {
                ModelState.AddModelError("", "Valor repetido, agregue otro diferente");
                return View(valoracion);
            }
            if (ModelState.IsValid)
            {
                db.Valoracions.Add(valoracion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(valoracion);
        }

        // GET: Valoracions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Valoracion valoracion = db.Valoracions.Find(id);
            if (valoracion == null)
            {
                return HttpNotFound();
            }
            return View(valoracion);
        }

        // POST: Valoracions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ValoracionID,Descripcion,Valor")] Valoracion valoracion)
        {
            if (db.Valoracions.Any(v => v.Valor == valoracion.Valor))
            {
                ModelState.AddModelError("", "Valor repetido, agregue otro diferente");
                return View(valoracion);
            }
            if (ModelState.IsValid)
            {
                db.Entry(valoracion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(valoracion);
        }

        // GET: Valoracions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Valoracion valoracion = db.Valoracions.Find(id);
            if (valoracion == null)
            {
                return HttpNotFound();
            }
            return View(valoracion);
        }

        // POST: Valoracions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Valoracion valoracion = db.Valoracions.Find(id);
            db.Valoracions.Remove(valoracion);
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
