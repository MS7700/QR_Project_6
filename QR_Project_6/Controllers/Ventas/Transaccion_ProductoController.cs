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
    public class Transaccion_ProductoController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Transaccion_Producto
        public ActionResult Index()
        {
            var transaccion_Productos = db.Transaccion_Productos.Include(t => t.Producto).Include(t => t.Transaccion);
            return View(transaccion_Productos.ToList());
        }

        // GET: Transaccion_Producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccion_Producto transaccion_Producto = db.Transaccion_Productos.Find(id);
            if (transaccion_Producto == null)
            {
                return HttpNotFound();
            }
            return View(transaccion_Producto);
        }

        // GET: Transaccion_Producto/Create
        public ActionResult Create()
        {
            ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "Nombre");
            ViewBag.TransaccionID = new SelectList(db.Transaccions, "TransaccionID", "TransaccionID");
            return View();
        }

        // POST: Transaccion_Producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransaccionID,ProductoID,Cantidad_Producto")] Transaccion_Producto transaccion_Producto)
        {
            if (ModelState.IsValid)
            {
                db.Transaccion_Productos.Add(transaccion_Producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "Nombre", transaccion_Producto.ProductoID);
            ViewBag.TransaccionID = new SelectList(db.Transaccions, "TransaccionID", "TransaccionID", transaccion_Producto.TransaccionID);
            return View(transaccion_Producto);
        }

        // GET: Transaccion_Producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccion_Producto transaccion_Producto = db.Transaccion_Productos.Find(id);
            if (transaccion_Producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "Nombre", transaccion_Producto.ProductoID);
            ViewBag.TransaccionID = new SelectList(db.Transaccions, "TransaccionID", "TransaccionID", transaccion_Producto.TransaccionID);
            return View(transaccion_Producto);
        }

        // POST: Transaccion_Producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransaccionID,ProductoID,Cantidad_Producto")] Transaccion_Producto transaccion_Producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaccion_Producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "Nombre", transaccion_Producto.ProductoID);
            ViewBag.TransaccionID = new SelectList(db.Transaccions, "TransaccionID", "TransaccionID", transaccion_Producto.TransaccionID);
            return View(transaccion_Producto);
        }

        // GET: Transaccion_Producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccion_Producto transaccion_Producto = db.Transaccion_Productos.Find(id);
            if (transaccion_Producto == null)
            {
                return HttpNotFound();
            }
            return View(transaccion_Producto);
        }

        // POST: Transaccion_Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaccion_Producto transaccion_Producto = db.Transaccion_Productos.Find(id);
            db.Transaccion_Productos.Remove(transaccion_Producto);
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
