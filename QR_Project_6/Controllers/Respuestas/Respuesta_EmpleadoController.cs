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
    public class Respuesta_EmpleadoController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Respuesta_Empleado
        public ActionResult Index()
        {
            var respuesta_Empleados = db.Respuesta_Empleados.Include(r => r.Departamento_Destino).Include(r => r.Departamento_Origen).Include(r => r.Empleado_Destino).Include(r => r.Empleado_Origen).Include(r => r.Estado_Destino).Include(r => r.Estado_Origen).Include(r => r.Queja).Include(r => r.Reclamacion).Include(r => r.Sucursal_Destino).Include(r => r.Sucursal_Origen);
            return View(respuesta_Empleados.ToList());
        }

        // GET: Respuesta_Empleado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta_Empleado respuesta_Empleado = db.Respuesta_Empleados.Find(id);
            if (respuesta_Empleado == null)
            {
                return HttpNotFound();
            }
            return View(respuesta_Empleado);
        }

        // GET: Respuesta_Empleado/Create
        public ActionResult Create()
        {
            ViewBag.Departamento_Departamento_DestinoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre");
            ViewBag.Departamento_Departamento_OrigenID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre");
            ViewBag.Empleado_Empleado_DestinoID = new SelectList(db.Empleados, "PersonaID", "Identificacion");
            ViewBag.Empleado_Empleado_OrigenID = new SelectList(db.Empleados, "PersonaID", "Identificacion");
            ViewBag.Estado_QR_Estado_DestinoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion");
            ViewBag.Estado_QR_Estado_OrigenID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion");
            ViewBag.Queja_QuejaID = new SelectList(db.Quejas, "QRID", "QRID");
            ViewBag.Reclamacion_ReclamacionID = new SelectList(db.Reclamacions, "QRID", "QRID");
            ViewBag.Sucursal_Sucursal_DestinoID = new SelectList(db.Sucursals, "SucursalID", "Nombre");
            ViewBag.Sucursal_Sucursal_OrigenID = new SelectList(db.Sucursals, "SucursalID", "Nombre");
            return View();
        }

        // POST: Respuesta_Empleado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RespuestaID,Departamento_Departamento_OrigenID,Departamento_Departamento_DestinoID,Empleado_Empleado_OrigenID,Empleado_Empleado_DestinoID,Sucursal_Sucursal_OrigenID,Sucursal_Sucursal_DestinoID,Fecha,Detalle,Estado_QR_Estado_OrigenID,Estado_QR_Estado_DestinoID,Queja_QuejaID,Reclamacion_ReclamacionID")] Respuesta_Empleado respuesta_Empleado)
        {
            if (ModelState.IsValid)
            {
                db.Respuesta_Empleados.Add(respuesta_Empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Departamento_Departamento_DestinoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", respuesta_Empleado.Departamento_Departamento_DestinoID);
            ViewBag.Departamento_Departamento_OrigenID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", respuesta_Empleado.Departamento_Departamento_OrigenID);
            ViewBag.Empleado_Empleado_DestinoID = new SelectList(db.Empleados, "PersonaID", "Identificacion", respuesta_Empleado.Empleado_Empleado_DestinoID);
            ViewBag.Empleado_Empleado_OrigenID = new SelectList(db.Empleados, "PersonaID", "Identificacion", respuesta_Empleado.Empleado_Empleado_OrigenID);
            ViewBag.Estado_QR_Estado_DestinoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", respuesta_Empleado.Estado_QR_Estado_DestinoID);
            ViewBag.Estado_QR_Estado_OrigenID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", respuesta_Empleado.Estado_QR_Estado_OrigenID);
            ViewBag.Queja_QuejaID = new SelectList(db.Quejas, "QRID", "QRID", respuesta_Empleado.Queja_QuejaID);
            ViewBag.Reclamacion_ReclamacionID = new SelectList(db.Reclamacions, "QRID", "QRID", respuesta_Empleado.Reclamacion_ReclamacionID);
            ViewBag.Sucursal_Sucursal_DestinoID = new SelectList(db.Sucursals, "SucursalID", "Nombre", respuesta_Empleado.Sucursal_Sucursal_DestinoID);
            ViewBag.Sucursal_Sucursal_OrigenID = new SelectList(db.Sucursals, "SucursalID", "Nombre", respuesta_Empleado.Sucursal_Sucursal_OrigenID);
            return View(respuesta_Empleado);
        }

        // GET: Respuesta_Empleado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta_Empleado respuesta_Empleado = db.Respuesta_Empleados.Find(id);
            if (respuesta_Empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.Departamento_Departamento_DestinoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", respuesta_Empleado.Departamento_Departamento_DestinoID);
            ViewBag.Departamento_Departamento_OrigenID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", respuesta_Empleado.Departamento_Departamento_OrigenID);
            ViewBag.Empleado_Empleado_DestinoID = new SelectList(db.Empleados, "PersonaID", "Identificacion", respuesta_Empleado.Empleado_Empleado_DestinoID);
            ViewBag.Empleado_Empleado_OrigenID = new SelectList(db.Empleados, "PersonaID", "Identificacion", respuesta_Empleado.Empleado_Empleado_OrigenID);
            ViewBag.Estado_QR_Estado_DestinoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", respuesta_Empleado.Estado_QR_Estado_DestinoID);
            ViewBag.Estado_QR_Estado_OrigenID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", respuesta_Empleado.Estado_QR_Estado_OrigenID);
            ViewBag.Queja_QuejaID = new SelectList(db.Quejas, "QRID", "QRID", respuesta_Empleado.Queja_QuejaID);
            ViewBag.Reclamacion_ReclamacionID = new SelectList(db.Reclamacions, "QRID", "QRID", respuesta_Empleado.Reclamacion_ReclamacionID);
            ViewBag.Sucursal_Sucursal_DestinoID = new SelectList(db.Sucursals, "SucursalID", "Nombre", respuesta_Empleado.Sucursal_Sucursal_DestinoID);
            ViewBag.Sucursal_Sucursal_OrigenID = new SelectList(db.Sucursals, "SucursalID", "Nombre", respuesta_Empleado.Sucursal_Sucursal_OrigenID);
            return View(respuesta_Empleado);
        }

        // POST: Respuesta_Empleado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RespuestaID,Departamento_Departamento_OrigenID,Departamento_Departamento_DestinoID,Empleado_Empleado_OrigenID,Empleado_Empleado_DestinoID,Sucursal_Sucursal_OrigenID,Sucursal_Sucursal_DestinoID,Fecha,Detalle,Estado_QR_Estado_OrigenID,Estado_QR_Estado_DestinoID,Queja_QuejaID,Reclamacion_ReclamacionID")] Respuesta_Empleado respuesta_Empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(respuesta_Empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departamento_Departamento_DestinoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", respuesta_Empleado.Departamento_Departamento_DestinoID);
            ViewBag.Departamento_Departamento_OrigenID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", respuesta_Empleado.Departamento_Departamento_OrigenID);
            ViewBag.Empleado_Empleado_DestinoID = new SelectList(db.Empleados, "PersonaID", "Identificacion", respuesta_Empleado.Empleado_Empleado_DestinoID);
            ViewBag.Empleado_Empleado_OrigenID = new SelectList(db.Empleados, "PersonaID", "Identificacion", respuesta_Empleado.Empleado_Empleado_OrigenID);
            ViewBag.Estado_QR_Estado_DestinoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", respuesta_Empleado.Estado_QR_Estado_DestinoID);
            ViewBag.Estado_QR_Estado_OrigenID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", respuesta_Empleado.Estado_QR_Estado_OrigenID);
            ViewBag.Queja_QuejaID = new SelectList(db.Quejas, "QRID", "QRID", respuesta_Empleado.Queja_QuejaID);
            ViewBag.Reclamacion_ReclamacionID = new SelectList(db.Reclamacions, "QRID", "QRID", respuesta_Empleado.Reclamacion_ReclamacionID);
            ViewBag.Sucursal_Sucursal_DestinoID = new SelectList(db.Sucursals, "SucursalID", "Nombre", respuesta_Empleado.Sucursal_Sucursal_DestinoID);
            ViewBag.Sucursal_Sucursal_OrigenID = new SelectList(db.Sucursals, "SucursalID", "Nombre", respuesta_Empleado.Sucursal_Sucursal_OrigenID);
            return View(respuesta_Empleado);
        }

        // GET: Respuesta_Empleado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta_Empleado respuesta_Empleado = db.Respuesta_Empleados.Find(id);
            if (respuesta_Empleado == null)
            {
                return HttpNotFound();
            }
            return View(respuesta_Empleado);
        }

        // POST: Respuesta_Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Respuesta_Empleado respuesta_Empleado = db.Respuesta_Empleados.Find(id);
            db.Respuesta_Empleados.Remove(respuesta_Empleado);
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
