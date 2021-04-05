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
    public class Tipo_ProductoController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Tipo_Producto
        public ActionResult Index()
        {
            return View(db.Tipo_Productos.ToList());
        }

        // GET: Tipo_Producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Producto tipo_Producto = db.Tipo_Productos.Find(id);
            if (tipo_Producto == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Producto);
        }

        // GET: Tipo_Producto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tipo_Producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoID,Descripcion")] Tipo_Producto tipo_Producto)
        {
            if (ModelState.IsValid)
            {
                db.Tipo_Productos.Add(tipo_Producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_Producto);
        }

        // GET: Tipo_Producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Producto tipo_Producto = db.Tipo_Productos.Find(id);
            if (tipo_Producto == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Producto);
        }

        // POST: Tipo_Producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoID,Descripcion")] Tipo_Producto tipo_Producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_Producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_Producto);
        }

        // GET: Tipo_Producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Producto tipo_Producto = db.Tipo_Productos.Find(id);
            if (tipo_Producto == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Producto);
        }

        // POST: Tipo_Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tipo_Producto tipo_Producto = db.Tipo_Productos.Find(id);
            db.Tipo_Productos.Remove(tipo_Producto);
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
