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
    [Authorize(Roles = "Admin")]
    public class Estado_QRController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Estado_QR
        public ActionResult Index()
        {
            return View(db.Estado_QRs.ToList());
        }

        // GET: Estado_QR/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado_QR estado_QR = db.Estado_QRs.Find(id);
            if (estado_QR == null)
            {
                return HttpNotFound();
            }
            return View(estado_QR);
        }

        // GET: Estado_QR/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estado_QR/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstadoID,Descripcion")] Estado_QR estado_QR)
        {
            if (ModelState.IsValid)
            {
                db.Estado_QRs.Add(estado_QR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estado_QR);
        }

        // GET: Estado_QR/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado_QR estado_QR = db.Estado_QRs.Find(id);
            if (estado_QR == null)
            {
                return HttpNotFound();
            }
            return View(estado_QR);
        }

        // POST: Estado_QR/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstadoID,Descripcion")] Estado_QR estado_QR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estado_QR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estado_QR);
        }

        // GET: Estado_QR/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado_QR estado_QR = db.Estado_QRs.Find(id);
            if (estado_QR == null)
            {
                return HttpNotFound();
            }
            return View(estado_QR);
        }

        // POST: Estado_QR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estado_QR estado_QR = db.Estado_QRs.Find(id);
            db.Estado_QRs.Remove(estado_QR);
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
