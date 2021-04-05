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
    public class QuejasController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Quejas
        public ActionResult Index()
        {
            var quejas = db.Quejas.Include(q => q.Cliente).Include(q => q.Departamento).Include(q => q.Empleado).Include(q => q.Estado_QR).Include(q => q.Sucursal).Include(q => q.Tipo_Queja);
            return View(quejas.ToList());
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
            ViewBag.Cliente_ClienteID = new SelectList(db.Clientes, "PersonaID", "Identificacion");
            ViewBag.Departamento_DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre");
            ViewBag.Empleado_EmpleadoID = new SelectList(db.Empleados, "PersonaID", "Identificacion");
            ViewBag.Estado_QR_EstadoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion");
            ViewBag.Sucursal_SucursalID = new SelectList(db.Sucursals, "SucursalID", "Nombre");
            ViewBag.Tipo_Queja_TipoID = new SelectList(db.Tipo_Quejas, "TipoID", "Descripcion");
            return View();
        }

        // POST: Quejas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QRID,Tipo_Queja_TipoID,UserNameID,Fecha,Comentario,Cliente_ClienteID,Departamento_DepartamentoID,Empleado_EmpleadoID,Estado_QR_EstadoID,Sucursal_SucursalID")] Queja queja)
        {
            if (ModelState.IsValid)
            {
                db.Quejas.Add(queja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cliente_ClienteID = new SelectList(db.Clientes, "PersonaID", "Identificacion", queja.Cliente_ClienteID);
            ViewBag.Departamento_DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", queja.Departamento_DepartamentoID);
            ViewBag.Empleado_EmpleadoID = new SelectList(db.Empleados, "PersonaID", "Identificacion", queja.Empleado_EmpleadoID);
            ViewBag.Estado_QR_EstadoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", queja.Estado_QR_EstadoID);
            ViewBag.Sucursal_SucursalID = new SelectList(db.Sucursals, "SucursalID", "Nombre", queja.Sucursal_SucursalID);
            ViewBag.Tipo_Queja_TipoID = new SelectList(db.Tipo_Quejas, "TipoID", "Descripcion", queja.Tipo_Queja_TipoID);
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
            ViewBag.Cliente_ClienteID = new SelectList(db.Clientes, "PersonaID", "Identificacion", queja.Cliente_ClienteID);
            ViewBag.Departamento_DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", queja.Departamento_DepartamentoID);
            ViewBag.Empleado_EmpleadoID = new SelectList(db.Empleados, "PersonaID", "Identificacion", queja.Empleado_EmpleadoID);
            ViewBag.Estado_QR_EstadoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", queja.Estado_QR_EstadoID);
            ViewBag.Sucursal_SucursalID = new SelectList(db.Sucursals, "SucursalID", "Nombre", queja.Sucursal_SucursalID);
            ViewBag.Tipo_Queja_TipoID = new SelectList(db.Tipo_Quejas, "TipoID", "Descripcion", queja.Tipo_Queja_TipoID);
            return View(queja);
        }

        // POST: Quejas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QRID,Tipo_Queja_TipoID,UserNameID,Fecha,Comentario,Cliente_ClienteID,Departamento_DepartamentoID,Empleado_EmpleadoID,Estado_QR_EstadoID,Sucursal_SucursalID")] Queja queja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(queja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cliente_ClienteID = new SelectList(db.Clientes, "PersonaID", "Identificacion", queja.Cliente_ClienteID);
            ViewBag.Departamento_DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", queja.Departamento_DepartamentoID);
            ViewBag.Empleado_EmpleadoID = new SelectList(db.Empleados, "PersonaID", "Identificacion", queja.Empleado_EmpleadoID);
            ViewBag.Estado_QR_EstadoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", queja.Estado_QR_EstadoID);
            ViewBag.Sucursal_SucursalID = new SelectList(db.Sucursals, "SucursalID", "Nombre", queja.Sucursal_SucursalID);
            ViewBag.Tipo_Queja_TipoID = new SelectList(db.Tipo_Quejas, "TipoID", "Descripcion", queja.Tipo_Queja_TipoID);
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
