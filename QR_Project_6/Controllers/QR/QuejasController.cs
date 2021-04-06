using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using QR_Project_6.Models;
using QR_Project_6.Models.Cuentas;
using QR_Project_6.Models.Estados;

namespace QR_Project_6.Controllers
{
    public class QuejasController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Quejas
        public ActionResult Index()
        {
            

            List<Queja> quejas = new List<Queja>();
            if (User.IsInRole(Roles.Admin))
            {
                AddQuejaAdmin(quejas);
                return View(quejas);
            }
            if (User.IsInRole(Roles.Empleado))
            {
                //TODO: Ordenar por estado y prioridad
                AddQuejasEmpleado(quejas);
                return View(quejas);
            }
            if (User.IsInRole(Roles.Cliente))
            {
                AddQuejasCliente(quejas);
                return View(quejas);
            }
            return View(quejas);
        }

        private void AddQuejaAdmin(List<Queja> quejas)
        {
            var queja = db.Quejas.Include(q => q.Cliente).Include(q => q.Departamento).Include(q => q.Empleado).Include(q => q.Estado_QR).Include(q => q.Sucursal).Include(q => q.Tipo_Queja);
            quejas.AddRange(queja.ToList<Queja>());
        }

        private void AddQuejasCliente(List<Queja> quejas)
        {
            string id = User.Identity.GetUserId();
            var Cliente = db.Clientes.Where(c => c.UserNameID == id).First<Cliente>();
            var queja = db.Quejas.Where(q => q.Cliente_ClienteID == Cliente.PersonaID);
            quejas.AddRange(queja.ToList<Queja>());
        }

        private void AddQuejasEmpleado(List<Queja> quejas)
        {
            string id = User.Identity.GetUserId();
            var Empleado = db.Empleados.Where(e => e.UserNameID == id).First<Empleado>();
            var Departamento_Representante = db.Departamentos.Where(d => d.Empleado_PersonaID == Empleado.PersonaID);
            var queja = db.Quejas.Where(
                q => q.Empleado_EmpleadoID == Empleado.PersonaID
                || (q.Empleado == null && q.Departamento_DepartamentoID == Empleado.Departamento_DepartamentoID && q.Sucursal_SucursalID == Empleado.Sucursal_SucursalID)
                || (q.Empleado == null && q.Departamento_DepartamentoID == null && q.Sucursal_SucursalID == Empleado.Sucursal_SucursalID)
                || (q.Empleado == null && q.Departamento_DepartamentoID == null && q.Sucursal_SucursalID == null)
                || (q.Empleado == null && q.Departamento_DepartamentoID == Empleado.Departamento_DepartamentoID && q.Sucursal_SucursalID == null)
                || q.UserNameID == Empleado.UserNameID
                || Departamento_Representante.Contains(q.Departamento));
            quejas.AddRange(queja.ToList<Queja>());
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
            if (User.IsInRole(Roles.Admin))
            {
                AddCreateViewBagAdmin();
                return View();
            }
            if (User.IsInRole(Roles.Empleado))
            {
                AddCreateViewBagEmpleado();
                return View();
            }
            if (User.IsInRole(Roles.Cliente))
            {
                AddCreateViewBagCliente();
                return View();
            }

            AddCreateViewBagDefault();
            return View();
        }

        private void AddCreateViewBagDefault()
        {
            ViewBag.Cliente_ClienteID = new SelectList(db.Clientes, "PersonaID", "Identificacion");
            ViewBag.Departamento_DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre");
            ViewBag.Empleado_EmpleadoID = new SelectList(db.Empleados, "PersonaID", "Identificacion");
            ViewBag.Sucursal_SucursalID = new SelectList(db.Sucursals, "SucursalID", "Nombre");
            ViewBag.Tipo_Queja_TipoID = new SelectList(db.Tipo_Quejas, "TipoID", "Descripcion");
        }

        private void AddCreateViewBagCliente()
        {
            string id = User.Identity.GetUserId();
            ViewBag.UserNameID = id;
            ViewBag.Cliente_ClienteID = db.Clientes.Where(c => c.UserNameID == id).FirstOrDefault<Cliente>().PersonaID;
            ViewBag.Departamento_DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre");
            ViewBag.Empleado_EmpleadoID = new SelectList(db.Empleados, "PersonaID", "Identificacion");
            ViewBag.Estado_QR_EstadoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion");
            ViewBag.Sucursal_SucursalID = new SelectList(db.Sucursals, "SucursalID", "Nombre");
            ViewBag.Tipo_Queja_TipoID = new SelectList(db.Tipo_Quejas, "TipoID", "Descripcion");
        }

        private void AddCreateViewBagEmpleado()
        {
            ViewBag.UserNameID = User.Identity.GetUserId();
            ViewBag.Cliente_ClienteID = new SelectList(db.Clientes, "PersonaID", "Identificacion");
            ViewBag.Departamento_DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre");
            ViewBag.Empleado_EmpleadoID = new SelectList(db.Empleados, "PersonaID", "Identificacion");
            ViewBag.Sucursal_SucursalID = new SelectList(db.Sucursals, "SucursalID", "Nombre");
            ViewBag.Tipo_Queja_TipoID = new SelectList(db.Tipo_Quejas, "TipoID", "Descripcion");
        }

        private void AddCreateViewBagAdmin()
        {
            ViewBag.UserNameID = User.Identity.GetUserId();
            ViewBag.Cliente_ClienteID = new SelectList(db.Clientes, "PersonaID", "Identificacion");
            ViewBag.Departamento_DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre");
            ViewBag.Empleado_EmpleadoID = new SelectList(db.Empleados, "PersonaID", "Identificacion");
            ViewBag.Estado_QR_EstadoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion");
            ViewBag.Sucursal_SucursalID = new SelectList(db.Sucursals, "SucursalID", "Nombre");
            ViewBag.Tipo_Queja_TipoID = new SelectList(db.Tipo_Quejas, "TipoID", "Descripcion");
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
                if (!User.IsInRole(Roles.Admin))
                {
                    Estado_QR_Helper estado_helper = new Estado_QR_Helper();
                    queja.Estado_QR_EstadoID = estado_helper.GetEstadoByDescripcion(Estado_QR_Helper.ABIERTO).EstadoID;
                }
                
                queja.Fecha = DateTime.Now;
                db.Quejas.Add(queja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserNameID = User.Identity.GetUserId();
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
