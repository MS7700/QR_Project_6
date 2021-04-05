using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using QR_Project_6.Models;

namespace QR_Project_6.Controllers
{
    public class EmpleadoesController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Empleadoes
        public ActionResult Index()
        {
            var empleados = db.Empleados.Include(e => e.Departamento).Include(e => e.Direccion).Include(e => e.Estado_Cliente).Include(e => e.Sucursal).Include(e => e.Tipo_Identificacion);
            return View(empleados.ToList());
        }

        // GET: Empleadoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleadoes/Create
        public ActionResult Create()
        {
            ViewBag.Departamento_DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre");
            ViewBag.Direccion_DireccionID = new SelectList(db.Direccions, "DireccionID", "DireccionID");
            ViewBag.Estado_Cliente_Estado_ClienteID = new SelectList(db.Estado_Clientes, "EstadoID", "Descripcion");
            ViewBag.Sucursal_SucursalID = new SelectList(db.Sucursals, "SucursalID", "Nombre");
            ViewBag.Tipo_Identificacion_Tipo_IdentificacionID = new SelectList(db.Tipo_Identificacions, "TipoID", "Descripcion");
            return View();
        }

        // POST: Empleadoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonaID,Departamento_DepartamentoID,Sucursal_SucursalID,Identificacion,Nombre,Apellido,Fecha_Ingreso,Telefono,UserNameID,Direccion_DireccionID,Estado_Cliente_Estado_ClienteID,Tipo_Identificacion_Tipo_IdentificacionID")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Departamento_DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", empleado.Departamento_DepartamentoID);
            ViewBag.Direccion_DireccionID = new SelectList(db.Direccions, "DireccionID", "DireccionID", empleado.Direccion_DireccionID);
            ViewBag.Estado_Cliente_Estado_ClienteID = new SelectList(db.Estado_Clientes, "EstadoID", "Descripcion", empleado.Estado_Cliente_Estado_ClienteID);
            ViewBag.Sucursal_SucursalID = new SelectList(db.Sucursals, "SucursalID", "Nombre", empleado.Sucursal_SucursalID);
            ViewBag.Tipo_Identificacion_Tipo_IdentificacionID = new SelectList(db.Tipo_Identificacions, "TipoID", "Descripcion", empleado.Tipo_Identificacion_Tipo_IdentificacionID);
            return View(empleado);
        }

        // GET: Empleadoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.Departamento_DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", empleado.Departamento_DepartamentoID);
            ViewBag.Direccion_DireccionID = new SelectList(db.Direccions, "DireccionID", "DireccionID", empleado.Direccion_DireccionID);
            ViewBag.Estado_Cliente_Estado_ClienteID = new SelectList(db.Estado_Clientes, "EstadoID", "Descripcion", empleado.Estado_Cliente_Estado_ClienteID);
            ViewBag.Sucursal_SucursalID = new SelectList(db.Sucursals, "SucursalID", "Nombre", empleado.Sucursal_SucursalID);
            ViewBag.Tipo_Identificacion_Tipo_IdentificacionID = new SelectList(db.Tipo_Identificacions, "TipoID", "Descripcion", empleado.Tipo_Identificacion_Tipo_IdentificacionID);
            return View(empleado);
        }

        // POST: Empleadoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonaID,Departamento_DepartamentoID,Sucursal_SucursalID,Identificacion,Nombre,Apellido,Fecha_Ingreso,Telefono,UserNameID,Direccion_DireccionID,Estado_Cliente_Estado_ClienteID,Tipo_Identificacion_Tipo_IdentificacionID")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departamento_DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", empleado.Departamento_DepartamentoID);
            ViewBag.Direccion_DireccionID = new SelectList(db.Direccions, "DireccionID", "DireccionID", empleado.Direccion_DireccionID);
            ViewBag.Estado_Cliente_Estado_ClienteID = new SelectList(db.Estado_Clientes, "EstadoID", "Descripcion", empleado.Estado_Cliente_Estado_ClienteID);
            ViewBag.Sucursal_SucursalID = new SelectList(db.Sucursals, "SucursalID", "Nombre", empleado.Sucursal_SucursalID);
            ViewBag.Tipo_Identificacion_Tipo_IdentificacionID = new SelectList(db.Tipo_Identificacions, "TipoID", "Descripcion", empleado.Tipo_Identificacion_Tipo_IdentificacionID);
            return View(empleado);
        }

        // GET: Empleadoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            Empleado empleado = db.Empleados.Find(id);

            var user = await UserManager.FindByEmailAsync(empleado.UserNameID);
            var roles = await UserManager.GetRolesAsync(user.Id);
            foreach (var role in roles.ToList())
            {
                var r = await UserManager.RemoveFromRoleAsync(user.Id, role);
            }
            var rc = await UserManager.DeleteAsync(user);

            db.Empleados.Remove(empleado);
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
