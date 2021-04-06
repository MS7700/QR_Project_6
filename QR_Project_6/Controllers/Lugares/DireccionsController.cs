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
    public class DireccionsController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Direccions
        public ActionResult Index()
        {
            var direccions = db.Direccions.Include(d => d.Pais);
            return View(direccions.ToList());
        }

        // GET: Direccions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direccion direccion = db.Direccions.Find(id);
            if (direccion == null)
            {
                return HttpNotFound();
            }
            return View(direccion);
        }

        // GET: Direccions/Create
        public ActionResult Create()
        {
            ViewBag.Pais_PaisID = new SelectList(db.Paises, "PaisID", "Nombre_Pais");
            return View();
        }

        // POST: Direccions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DireccionID,Provincia,Sector,Municipio,Barrio,Direccion_1,Direccion_2,Pais_PaisID")] Direccion direccion)
        {
            if (ModelState.IsValid)
            {
                db.Direccions.Add(direccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Pais_PaisID = new SelectList(db.Paises, "PaisID", "Nombre_Pais", direccion.Pais_PaisID);
            return View(direccion);
        }

        // GET: Direccions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direccion direccion = db.Direccions.Find(id);
            if (direccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pais_PaisID = new SelectList(db.Paises, "PaisID", "Nombre_Pais", direccion.Pais_PaisID);
            return View(direccion);
        }

        // POST: Direccions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DireccionID,Provincia,Sector,Municipio,Barrio,Direccion_1,Direccion_2,Pais_PaisID")] Direccion direccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(direccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Pais_PaisID = new SelectList(db.Paises, "PaisID", "Nombre_Pais", direccion.Pais_PaisID);
            return View(direccion);
        }

        // GET: Direccions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direccion direccion = db.Direccions.Find(id);
            if (direccion == null)
            {
                return HttpNotFound();
            }
            return View(direccion);
        }

        // POST: Direccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Direccion direccion = db.Direccions.Find(id);
            db.Direccions.Remove(direccion);
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
