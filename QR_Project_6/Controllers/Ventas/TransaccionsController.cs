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
    public class TransaccionsController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Transaccions
        public ActionResult Index()
        {
            var transaccions = db.Transaccions.Include(t => t.Cliente).Include(t => t.Empleado).Include(t => t.Estado_Transaccion);
            return View(transaccions.ToList());
        }

        // GET: Transaccions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccion transaccion = db.Transaccions.Find(id);
            if (transaccion == null)
            {
                return HttpNotFound();
            }
            return View(transaccion);
        }

        // GET: Transaccions/Create
        public ActionResult Create()
        {
            ViewBag.Cliente_PersonaID = new SelectList(db.Clientes, "PersonaID", "Identificacion");
            ViewBag.Empleado_PersonaID = new SelectList(db.Empleados, "PersonaID", "Identificacion");
            ViewBag.Estado_Transaccion_EstadoID = new SelectList(db.Estado_Transaccions, "EstadoID", "Descripcion");
            return View();
        }

        // POST: Transaccions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransaccionID,Fecha,Monto,Cliente_PersonaID,Empleado_PersonaID,Estado_Transaccion_EstadoID")] Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                db.Transaccions.Add(transaccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cliente_PersonaID = new SelectList(db.Clientes, "PersonaID", "Identificacion", transaccion.Cliente_PersonaID);
            ViewBag.Empleado_PersonaID = new SelectList(db.Empleados, "PersonaID", "Identificacion", transaccion.Empleado_PersonaID);
            ViewBag.Estado_Transaccion_EstadoID = new SelectList(db.Estado_Transaccions, "EstadoID", "Descripcion", transaccion.Estado_Transaccion_EstadoID);
            return View(transaccion);
        }

        // GET: Transaccions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccion transaccion = db.Transaccions.Find(id);
            if (transaccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cliente_PersonaID = new SelectList(db.Clientes, "PersonaID", "Identificacion", transaccion.Cliente_PersonaID);
            ViewBag.Empleado_PersonaID = new SelectList(db.Empleados, "PersonaID", "Identificacion", transaccion.Empleado_PersonaID);
            ViewBag.Estado_Transaccion_EstadoID = new SelectList(db.Estado_Transaccions, "EstadoID", "Descripcion", transaccion.Estado_Transaccion_EstadoID);
            return View(transaccion);
        }

        // POST: Transaccions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransaccionID,Fecha,Monto,Cliente_PersonaID,Empleado_PersonaID,Estado_Transaccion_EstadoID")] Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cliente_PersonaID = new SelectList(db.Clientes, "PersonaID", "Identificacion", transaccion.Cliente_PersonaID);
            ViewBag.Empleado_PersonaID = new SelectList(db.Empleados, "PersonaID", "Identificacion", transaccion.Empleado_PersonaID);
            ViewBag.Estado_Transaccion_EstadoID = new SelectList(db.Estado_Transaccions, "EstadoID", "Descripcion", transaccion.Estado_Transaccion_EstadoID);
            return View(transaccion);
        }

        // GET: Transaccions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccion transaccion = db.Transaccions.Find(id);
            if (transaccion == null)
            {
                return HttpNotFound();
            }
            return View(transaccion);
        }

        // POST: Transaccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaccion transaccion = db.Transaccions.Find(id);
            db.Transaccions.Remove(transaccion);
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
