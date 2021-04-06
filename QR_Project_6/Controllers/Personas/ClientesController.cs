using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using QR_Project_6.Models;

namespace QR_Project_6.Controllers
{
    public class ClientesController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Clientes
        public ActionResult Index()
        {
            var clientes = db.Clientes.Include(c => c.Direccion).Include(c => c.Estado_Cliente).Include(c => c.Tipo_Identificacion);
            return View(clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            ViewBag.Direccion_DireccionID = new SelectList(db.Direccions, "DireccionID", "DireccionID");
            ViewBag.Estado_Cliente_Estado_ClienteID = new SelectList(db.Estado_Clientes, "EstadoID", "Descripcion");
            ViewBag.Tipo_Identificacion_Tipo_IdentificacionID = new SelectList(db.Tipo_Identificacions, "TipoID", "Descripcion");
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonaID,Identificacion,Nombre,Apellido,Fecha_Ingreso,Telefono,UserNameID,Email,Direccion_DireccionID,Estado_Cliente_Estado_ClienteID,Tipo_Identificacion_Tipo_IdentificacionID")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Direccion_DireccionID = new SelectList(db.Direccions, "DireccionID", "DireccionID", cliente.Direccion_DireccionID);
            ViewBag.Estado_Cliente_Estado_ClienteID = new SelectList(db.Estado_Clientes, "EstadoID", "Descripcion", cliente.Estado_Cliente_Estado_ClienteID);
            ViewBag.Tipo_Identificacion_Tipo_IdentificacionID = new SelectList(db.Tipo_Identificacions, "TipoID", "Descripcion", cliente.Tipo_Identificacion_Tipo_IdentificacionID);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.Direccion_DireccionID = new SelectList(db.Direccions, "DireccionID", "DireccionID", cliente.Direccion_DireccionID);
            ViewBag.Estado_Cliente_Estado_ClienteID = new SelectList(db.Estado_Clientes, "EstadoID", "Descripcion", cliente.Estado_Cliente_Estado_ClienteID);
            ViewBag.Tipo_Identificacion_Tipo_IdentificacionID = new SelectList(db.Tipo_Identificacions, "TipoID", "Descripcion", cliente.Tipo_Identificacion_Tipo_IdentificacionID);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonaID,Identificacion,Nombre,Apellido,Fecha_Ingreso,Telefono,UserNameID,Email,Direccion_DireccionID,Estado_Cliente_Estado_ClienteID,Tipo_Identificacion_Tipo_IdentificacionID")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Direccion_DireccionID = new SelectList(db.Direccions, "DireccionID", "DireccionID", cliente.Direccion_DireccionID);
            ViewBag.Estado_Cliente_Estado_ClienteID = new SelectList(db.Estado_Clientes, "EstadoID", "Descripcion", cliente.Estado_Cliente_Estado_ClienteID);
            ViewBag.Tipo_Identificacion_Tipo_IdentificacionID = new SelectList(db.Tipo_Identificacions, "TipoID", "Descripcion", cliente.Tipo_Identificacion_Tipo_IdentificacionID);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> DeleteConfirmedAsync(int id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            Cliente cliente = db.Clientes.Find(id);

            var user = await UserManager.FindByIdAsync(cliente.UserNameID);
            var roles = await UserManager.GetRolesAsync(user.Id);
            foreach (var role in roles.ToList())
            {
                var r = await UserManager.RemoveFromRoleAsync(user.Id, role);
            }
            var rc = await UserManager.DeleteAsync(user);

            
            db.Clientes.Remove(cliente);
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
