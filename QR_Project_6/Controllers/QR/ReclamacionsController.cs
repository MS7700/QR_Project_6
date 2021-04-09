using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using QR_Project_6.Extensions;
using QR_Project_6.Models;
using QR_Project_6.Models.Cuentas;
using QR_Project_6.Models.Estados;

namespace QR_Project_6.Controllers
{
    [Authorize]
    public class ReclamacionsController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Reclamacions
        public ActionResult Index()
        {
            List<Reclamacion> reclamaciones = new List<Reclamacion>();
            if (User.IsInRole(Roles.Admin))
            {
                AddReclamacionesAdmin(reclamaciones);
                return View(reclamaciones);
            }
            if (User.IsInRole(Roles.Empleado))
            {
                //TODO: Ordenar por estado y prioridad
                AddReclamacionesEmpleado(reclamaciones);
                return View(reclamaciones);
            }
            if (User.IsInRole(Roles.Cliente))
            {
                AddReclamacionesCliente(reclamaciones);
                return View(reclamaciones);
            }
            return View(reclamaciones);
        }

        private void AddReclamacionesCliente(List<Reclamacion> reclamaciones)
        {
            string id = User.Identity.GetUserId();
            var Cliente = db.Clientes.Where(c => c.UserNameID == id).First<Cliente>();
            var reclamacions = db.Reclamacions.Where(q => q.Cliente_ClienteID == Cliente.PersonaID);
            reclamaciones.AddRange(reclamacions.ToList<Reclamacion>());
        }

        private void AddReclamacionesAdmin(List<Reclamacion> reclamaciones)
        {
            var reclamacions = db.Reclamacions.Include(r => r.Cliente).Include(r => r.Departamento).Include(r => r.Empleado).Include(r => r.Estado_QR).Include(r => r.Sucursal).Include(r => r.Tipo_Reclamacion);
            reclamaciones.AddRange(reclamacions.ToList<Reclamacion>());
        }

        private void AddReclamacionesEmpleado(List<Reclamacion> reclamaciones)
        {
            string id = User.Identity.GetUserId();
            var Empleado = db.Empleados.Where(e => e.UserNameID == id).First<Empleado>();
            var Departamento_Representante = db.Departamentos.Where(d => d.Empleado_PersonaID == Empleado.PersonaID);
            var reclamacions = db.Reclamacions.Where(
                q => q.Empleado_EmpleadoID == Empleado.PersonaID
                || (q.Empleado == null && q.Departamento_DepartamentoID == Empleado.Departamento_DepartamentoID && q.Sucursal_SucursalID == Empleado.Sucursal_SucursalID)
                || (q.Empleado == null && q.Departamento_DepartamentoID == null && q.Sucursal_SucursalID == Empleado.Sucursal_SucursalID)
                || (q.Empleado == null && q.Departamento_DepartamentoID == null && q.Sucursal_SucursalID == null)
                || (q.Empleado == null && q.Departamento_DepartamentoID == Empleado.Departamento_DepartamentoID && q.Sucursal_SucursalID == null)
                || q.UserNameID == Empleado.UserNameID
                || Departamento_Representante.Contains(q.Departamento));
            reclamaciones.AddRange(reclamacions.ToList<Reclamacion>());
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

        [HttpPost]
        public ActionResult _ListRespuestas(int? id)
        {
            List<Respuesta> respuestas = new List<Respuesta>();
            AddListRespuestasReclamaciones(id, respuestas);
            return PartialView(respuestas);
        }

        private void AddListRespuestasReclamaciones(int? id_reclamacion, List<Respuesta> respuestas)
        {

            List<Respuesta_Empleado> respuesta_Empleados = db.Respuesta_Empleados.Where(e => e.Reclamacion_ReclamacionID == id_reclamacion).ToList();
            List<Respuesta_Cliente> respuesta_Clientes = db.Respuesta_Clientes.Where(e => e.Reclamacion_ReclamacionID == id_reclamacion).ToList();
            if (respuestas == null)
            {
                respuestas = new List<Respuesta>();
            }
            respuestas.AddRange(respuesta_Clientes);
            respuestas.AddRange(respuesta_Empleados);
            respuestas.Sort(ModelHelpers.CompareRespuestas);
        }

        // GET: Reclamacions/Create
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
            ViewBag.Tipo_Reclamacion_TipoID = new SelectList(db.Tipo_Reclamacions, "TipoID", "Descripcion");
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
            ViewBag.Tipo_Reclamacion_TipoID = new SelectList(db.Tipo_Reclamacions, "TipoID", "Descripcion");
        }

        private void AddCreateViewBagEmpleado()
        {
            ViewBag.UserNameID = User.Identity.GetUserId();
            ViewBag.Cliente_ClienteID = new SelectList(db.Clientes, "PersonaID", "Identificacion");
            ViewBag.Departamento_DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre");
            ViewBag.Empleado_EmpleadoID = new SelectList(db.Empleados, "PersonaID", "Identificacion");
            ViewBag.Sucursal_SucursalID = new SelectList(db.Sucursals, "SucursalID", "Nombre");
            ViewBag.Tipo_Reclamacion_TipoID = new SelectList(db.Tipo_Reclamacions, "TipoID", "Descripcion");
        }

        private void AddCreateViewBagAdmin()
        {
            ViewBag.UserNameID = User.Identity.GetUserId();
            ViewBag.Cliente_ClienteID = new SelectList(db.Clientes, "PersonaID", "Identificacion");
            ViewBag.Departamento_DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre");
            ViewBag.Empleado_EmpleadoID = new SelectList(db.Empleados, "PersonaID", "Identificacion");
            ViewBag.Estado_QR_EstadoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion");
            ViewBag.Sucursal_SucursalID = new SelectList(db.Sucursals, "SucursalID", "Nombre");
            ViewBag.Tipo_Reclamacion_TipoID = new SelectList(db.Tipo_Reclamacions, "TipoID", "Descripcion");
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
                if (!User.IsInRole(Roles.Admin))
                {
                    Estado_QR_Helper estado_helper = new Estado_QR_Helper();
                    reclamacion.Estado_QR_EstadoID = estado_helper.GetEstadoByDescripcion(Estado_QR_Helper.ABIERTO).EstadoID;
                }

                reclamacion.Fecha = DateTime.Now;
                db.Reclamacions.Add(reclamacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserNameID = User.Identity.GetUserId();
            ViewBag.Cliente_ClienteID = new SelectList(db.Clientes, "PersonaID", "Identificacion", reclamacion.Cliente_ClienteID);
            ViewBag.Departamento_DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", reclamacion.Departamento_DepartamentoID);
            ViewBag.Empleado_EmpleadoID = new SelectList(db.Empleados, "PersonaID", "Identificacion", reclamacion.Empleado_EmpleadoID);
            ViewBag.Estado_QR_EstadoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", reclamacion.Estado_QR_EstadoID);
            ViewBag.Sucursal_SucursalID = new SelectList(db.Sucursals, "SucursalID", "Nombre", reclamacion.Sucursal_SucursalID);
            ViewBag.Tipo_Reclamacion_TipoID = new SelectList(db.Tipo_Reclamacions, "TipoID", "Descripcion", reclamacion.Tipo_Reclamacion_TipoID);
            return View(reclamacion);
        }

        // GET: Reclamacions/Edit/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
