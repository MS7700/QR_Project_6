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
using QR_Project_6.Models.Estados;

namespace QR_Project_6.Controllers
{
    [Authorize]
    public class Respuesta_EmpleadoController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Respuesta_Empleado
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var respuesta_Empleados = db.Respuesta_Empleados.Include(r => r.Departamento_Destino).Include(r => r.Departamento_Origen).Include(r => r.Empleado_Destino).Include(r => r.Empleado_Origen).Include(r => r.Estado_Destino).Include(r => r.Estado_Origen).Include(r => r.Queja).Include(r => r.Reclamacion).Include(r => r.Sucursal_Destino).Include(r => r.Sucursal_Origen);
            return View(respuesta_Empleados.ToList());
        }

        // GET: Respuesta_Empleado/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        // GET: Respuesta_Empleado/Create
        public ActionResult CreateFromQueja(int? id)
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
            RespuestaEmpleadoQuejaViewModel viewmodel = InitializeREQViewModel(queja);

            int? id_queja = queja.QRID;

            AddListRespuestasQueja(viewmodel, id_queja);

            Respuesta_Empleado respuesta_Empleado = viewmodel.Respuesta_Empleado;

            AddViewBagCreate(respuesta_Empleado);

            return View(viewmodel);
        }

        private void AddViewBagCreate(Respuesta_Empleado respuesta_Empleado)
        {
            
            
            ViewBag.ID_Departamento_Destino = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", respuesta_Empleado.Departamento_Departamento_OrigenID);
            ViewBag.ID_Empleado_Destino = new SelectList(db.Empleados, "PersonaID", "Identificacion", respuesta_Empleado.Empleado_Empleado_OrigenID);
            ViewBag.ID_Estado_Destino = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", respuesta_Empleado.Estado_QR_Estado_OrigenID);
            ViewBag.ID_Sucursal_Destino = new SelectList(db.Sucursals, "SucursalID", "Nombre", respuesta_Empleado.Sucursal_Sucursal_OrigenID);
        }

        private void AddListRespuestasQueja(RespuestaEmpleadoQuejaViewModel viewmodel, int? id_queja)
        {
            List<Respuesta_Empleado> respuesta_Empleados = db.Respuesta_Empleados.Where(e => e.Queja_QuejaID == id_queja).ToList();
            List<Respuesta_Cliente> respuesta_Clientes = db.Respuesta_Clientes.Where(e => e.Queja_QuejaID == id_queja).ToList();
            viewmodel.QuejaViewModel.Respuestas.AddRange(respuesta_Clientes);
            viewmodel.QuejaViewModel.Respuestas.AddRange(respuesta_Empleados);
            viewmodel.QuejaViewModel.Respuestas.Sort(ModelHelpers.CompareRespuestas);
        }

        private static RespuestaEmpleadoQuejaViewModel InitializeREQViewModel(Queja queja)
        {
            return new RespuestaEmpleadoQuejaViewModel()
            {
                QuejaViewModel = new QuejaViewModel
                {
                    Queja = queja,
                    Respuestas = new List<Respuesta>()
                },
                Respuesta_Empleado = new Respuesta_Empleado
                {
                    Queja_QuejaID = queja.QRID,
                    Departamento_Departamento_OrigenID = queja.Departamento_DepartamentoID,
                    Empleado_Empleado_OrigenID = queja.Empleado_EmpleadoID,
                    Sucursal_Sucursal_OrigenID = queja.Sucursal_SucursalID,
                    Estado_QR_Estado_OrigenID = queja.Estado_QR_EstadoID
                }
            };
        }

        private int? CheckNull(int? destino, int? origen)
        {
            return destino == null ? origen : destino;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromQueja(RespuestaEmpleadoQuejaViewModel viewModel)
        {
            string id = User.Identity.GetUserId();
            Empleado empleado = db.Empleados.Where(e => e.UserNameID == id).FirstOrDefault<Empleado>();
            Respuesta_Empleado respuesta_Empleado = viewModel.Respuesta_Empleado;
            Queja queja = db.Quejas.Find(viewModel.QuejaViewModel.Queja.QRID);

            if (ModelState.IsValid)
            {
                respuesta_Empleado.Queja_QuejaID = queja.QRID;
                respuesta_Empleado.Fecha = DateTime.Now;
                AddParametrosDestinoPorEstado(empleado, respuesta_Empleado);


                respuesta_Empleado.Empleado_Empleado_OrigenID = empleado.PersonaID;
                queja.Estado_QR_EstadoID = respuesta_Empleado.Estado_QR_Estado_DestinoID;
                queja.Sucursal_SucursalID = respuesta_Empleado.Sucursal_Sucursal_DestinoID;
                queja.Departamento_DepartamentoID = respuesta_Empleado.Departamento_Departamento_DestinoID;
                queja.Empleado_EmpleadoID = respuesta_Empleado.Empleado_Empleado_DestinoID;
                db.Respuesta_Empleados.Add(respuesta_Empleado);
                db.Entry(queja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Quejas");
            }

            AddViewBagPostCreate(respuesta_Empleado);
            return View(viewModel);
        }

        private void AddViewBagPostCreate(Respuesta_Empleado respuesta_Empleado)
        {
            ViewBag.ID_Departamento_Destino = new SelectList(db.Departamentos, "DepartamentoID", "Nombre", respuesta_Empleado.Departamento_Departamento_DestinoID);
            ViewBag.ID_Empleado_Destino = new SelectList(db.Empleados, "PersonaID", "Identificacion", respuesta_Empleado.Empleado_Empleado_DestinoID);
            ViewBag.ID_Estado_Destino = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", respuesta_Empleado.Estado_QR_Estado_DestinoID);
            ViewBag.ID_Sucursal_Destino = new SelectList(db.Sucursals, "SucursalID", "Nombre", respuesta_Empleado.Sucursal_Sucursal_DestinoID);
        }

        private void AddParametrosDestinoPorEstado(Empleado empleado, Respuesta_Empleado respuesta_Empleado)
        {
            Estado_QR_Helper estado_QR_Helper = new Estado_QR_Helper();
            if (respuesta_Empleado.Estado_QR_Estado_DestinoID == estado_QR_Helper.GetEstadoByDescripcion(Estado_QR_Helper.PENDIENTE_VALORACION).EstadoID)
            {
                respuesta_Empleado.Empleado_Empleado_DestinoID = empleado.PersonaID;
                respuesta_Empleado.Departamento_Departamento_DestinoID = empleado.Departamento_DepartamentoID;
                respuesta_Empleado.Sucursal_Sucursal_DestinoID = empleado.Sucursal_SucursalID;
            }
            if (respuesta_Empleado.Estado_QR_Estado_DestinoID == estado_QR_Helper.GetEstadoByDescripcion(Estado_QR_Helper.REDIRIGIDO_DEPARTAMENTO).EstadoID)
            {
                respuesta_Empleado.Sucursal_Sucursal_DestinoID = empleado.Sucursal_SucursalID;
                respuesta_Empleado.Empleado_Empleado_DestinoID = null;
            }
            if (respuesta_Empleado.Estado_QR_Estado_DestinoID == estado_QR_Helper.GetEstadoByDescripcion(Estado_QR_Helper.REDIRIGIDO_SUCURSAL).EstadoID)
            {
                respuesta_Empleado.Departamento_Departamento_DestinoID = CheckNull(respuesta_Empleado.Departamento_Departamento_DestinoID, respuesta_Empleado.Departamento_Departamento_OrigenID);
                respuesta_Empleado.Empleado_Empleado_DestinoID = null;
            }
            if (respuesta_Empleado.Estado_QR_Estado_DestinoID == estado_QR_Helper.GetEstadoByDescripcion(Estado_QR_Helper.REDIRIGIDO_EMPLEADO).EstadoID)
            {
                Empleado empleado_Destino = db.Empleados.Find(respuesta_Empleado.Empleado_Empleado_DestinoID);
                respuesta_Empleado.Departamento_Departamento_DestinoID = empleado_Destino.Departamento_DepartamentoID;
                respuesta_Empleado.Sucursal_Sucursal_DestinoID = empleado_Destino.Sucursal_SucursalID;
            }
            if (respuesta_Empleado.Estado_QR_Estado_DestinoID == estado_QR_Helper.GetEstadoByDescripcion(Estado_QR_Helper.CERRADO).EstadoID)
            {
                respuesta_Empleado.Empleado_Empleado_DestinoID = empleado.PersonaID;
                respuesta_Empleado.Departamento_Departamento_DestinoID = empleado.Departamento_DepartamentoID;
                respuesta_Empleado.Sucursal_Sucursal_DestinoID = empleado.Sucursal_SucursalID;
            }
        }


        public ActionResult CreateFromReclamacion(int? id)
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
            RespuestaEmpleadoReclamacionViewModel viewmodel = InitializeRERViewModel(reclamacion);

            int? id_reclamacion = reclamacion.QRID;
            AddListRespuestasReclamacion(viewmodel, id_reclamacion);
            Respuesta_Empleado respuesta_Empleado = viewmodel.Respuesta_Empleado;

            AddViewBagCreate(respuesta_Empleado);

            
            return View(viewmodel);
        }

        private static RespuestaEmpleadoReclamacionViewModel InitializeRERViewModel(Reclamacion reclamacion)
        {
            return new RespuestaEmpleadoReclamacionViewModel()
            {
                ReclamacionViewModel = new ReclamacionViewModel
                {
                    Reclamacion = reclamacion,
                    Respuestas = new List<Respuesta>()
                },
                Respuesta_Empleado = new Respuesta_Empleado
                {
                    Reclamacion_ReclamacionID = reclamacion.QRID,
                    Departamento_Departamento_OrigenID = reclamacion.Departamento_DepartamentoID,
                    Empleado_Empleado_OrigenID = reclamacion.Empleado_EmpleadoID,
                    Sucursal_Sucursal_OrigenID = reclamacion.Sucursal_SucursalID,
                    Estado_QR_Estado_OrigenID = reclamacion.Estado_QR_EstadoID
                }
            };
        }

        private void AddListRespuestasReclamacion(RespuestaEmpleadoReclamacionViewModel viewmodel, int? id_reclamacion)
        {
            
            List<Respuesta_Empleado> respuesta_Empleados = db.Respuesta_Empleados.Where(e => e.Reclamacion_ReclamacionID == id_reclamacion).ToList();
            List<Respuesta_Cliente> respuesta_Clientes = db.Respuesta_Clientes.Where(e => e.Reclamacion_ReclamacionID == id_reclamacion).ToList();
            viewmodel.ReclamacionViewModel.Respuestas.AddRange(respuesta_Clientes);
            viewmodel.ReclamacionViewModel.Respuestas.AddRange(respuesta_Empleados);
            viewmodel.ReclamacionViewModel.Respuestas.Sort(ModelHelpers.CompareRespuestas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromReclamacion(RespuestaEmpleadoReclamacionViewModel viewModel)
        {
            string id = User.Identity.GetUserId();
            Empleado empleado = db.Empleados.Where(e => e.UserNameID == id).FirstOrDefault<Empleado>();
            Respuesta_Empleado respuesta_Empleado = viewModel.Respuesta_Empleado;
            Reclamacion reclamacion = db.Reclamacions.Find(viewModel.ReclamacionViewModel.Reclamacion.QRID);

            if (ModelState.IsValid)
            {
                respuesta_Empleado.Reclamacion_ReclamacionID = reclamacion.QRID;
                respuesta_Empleado.Fecha = DateTime.Now;

                AddParametrosDestinoPorEstado(empleado, respuesta_Empleado);


                respuesta_Empleado.Empleado_Empleado_OrigenID = empleado.PersonaID;
                reclamacion.Estado_QR_EstadoID = respuesta_Empleado.Estado_QR_Estado_DestinoID;
                reclamacion.Sucursal_SucursalID = respuesta_Empleado.Sucursal_Sucursal_DestinoID;
                reclamacion.Departamento_DepartamentoID = respuesta_Empleado.Departamento_Departamento_DestinoID;
                reclamacion.Empleado_EmpleadoID = respuesta_Empleado.Empleado_Empleado_DestinoID;
                db.Respuesta_Empleados.Add(respuesta_Empleado);
                db.Entry(reclamacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Reclamacions");
            }

            AddViewBagPostCreate(respuesta_Empleado);

            return View(viewModel);
        }


        // GET: Respuesta_Empleado/Edit/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
