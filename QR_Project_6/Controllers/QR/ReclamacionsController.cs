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
    public class ReclamacionsController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Reclamacions
        public ActionResult Index()
        {
            var reclamacions = db.Reclamacions.Include(r => r.Cliente).Include(r => r.Departamento).Include(r => r.Empleado).Include(r => r.Estado_QR).Include(r => r.Sucursal).Include(r => r.Tipo_Reclamacion);
            return View(reclamacions.ToList());
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
            ViewBag.Cliente_ClienteID = new SelectList(db.Clientes, "PersonaID", "Identificacion");
            ViewBag.Departamento_DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre");
            ViewBag.Empleado_EmpleadoID = new SelectList(db.Empleados, "PersonaID", "Identificacion");
            ViewBag.Estado_QR_EstadoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion");
            ViewBag.Sucursal_SucursalID = new SelectList(db.Sucursals, "SucursalID", "Nombre");
            ViewBag.Tipo_Reclamacion_TipoID = new SelectList(db.Tipo_Reclamacions, "TipoID", "Descripcion");
            return View();
        }

        // POST: Reclamacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QRID,Tipo_Reclamacion_TipoID,UserNameID,Fecha,Comentario,Cliente_ClienteID,Departamento_DepartamentoID,Empleado_EmpleadoID,Estado_QR_EstadoID,Sucursal_SucursalID")] Reclamacion reclamacion)
        {
            if (ModelState.IsValid)
            {
                db.Reclamacions.Add(reclamacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cliente_ClienteID = new SelectList(db.Clientes, "PersonaID", "Identificacion", reclamacion.Cliente_ClienteID);
            ViewBag.Departamento_DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", reclamacion.Departamento_DepartamentoID);
            ViewBag.Empleado_EmpleadoID = new SelectList(db.Empleados, "PersonaID", "Identificacion", reclamacion.Empleado_EmpleadoID);
            ViewBag.Estado_QR_EstadoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", reclamacion.Estado_QR_EstadoID);
            ViewBag.Sucursal_SucursalID = new SelectList(db.Sucursals, "SucursalID", "Nombre", reclamacion.Sucursal_SucursalID);
            ViewBag.Tipo_Reclamacion_TipoID = new SelectList(db.Tipo_Reclamacions, "TipoID", "Descripcion", reclamacion.Tipo_Reclamacion_TipoID);
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
            ViewBag.Cliente_ClienteID = new SelectList(db.Clientes, "PersonaID", "Identificacion", reclamacion.Cliente_ClienteID);
            ViewBag.Departamento_DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", reclamacion.Departamento_DepartamentoID);
            ViewBag.Empleado_EmpleadoID = new SelectList(db.Empleados, "PersonaID", "Identificacion", reclamacion.Empleado_EmpleadoID);
            ViewBag.Estado_QR_EstadoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", reclamacion.Estado_QR_EstadoID);
            ViewBag.Sucursal_SucursalID = new SelectList(db.Sucursals, "SucursalID", "Nombre", reclamacion.Sucursal_SucursalID);
            ViewBag.Tipo_Reclamacion_TipoID = new SelectList(db.Tipo_Reclamacions, "TipoID", "Descripcion", reclamacion.Tipo_Reclamacion_TipoID);
            return View(reclamacion);
        }

        // POST: Reclamacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QRID,Tipo_Reclamacion_TipoID,UserNameID,Fecha,Comentario,Cliente_ClienteID,Departamento_DepartamentoID,Empleado_EmpleadoID,Estado_QR_EstadoID,Sucursal_SucursalID")] Reclamacion reclamacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reclamacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cliente_ClienteID = new SelectList(db.Clientes, "PersonaID", "Identificacion", reclamacion.Cliente_ClienteID);
            ViewBag.Departamento_DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", reclamacion.Departamento_DepartamentoID);
            ViewBag.Empleado_EmpleadoID = new SelectList(db.Empleados, "PersonaID", "Identificacion", reclamacion.Empleado_EmpleadoID);
            ViewBag.Estado_QR_EstadoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", reclamacion.Estado_QR_EstadoID);
            ViewBag.Sucursal_SucursalID = new SelectList(db.Sucursals, "SucursalID", "Nombre", reclamacion.Sucursal_SucursalID);
            ViewBag.Tipo_Reclamacion_TipoID = new SelectList(db.Tipo_Reclamacions, "TipoID", "Descripcion", reclamacion.Tipo_Reclamacion_TipoID);
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
