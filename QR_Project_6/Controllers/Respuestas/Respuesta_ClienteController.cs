using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QR_Project_6.Extensions;
using QR_Project_6.Models;
using QR_Project_6.Models.Estados;

namespace QR_Project_6.Controllers
{
    [Authorize]
    public class Respuesta_ClienteController : Controller
    {
        private QR_Model db = new QR_Model();

        // GET: Respuesta_Cliente
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var respuesta_Clientes = db.Respuesta_Clientes.Include(r => r.Estado_Destino).Include(r => r.Estado_Origen).Include(r => r.Queja).Include(r => r.Reclamacion).Include(r=>r.Valoracion);
            return View(respuesta_Clientes.ToList());
        }

        // GET: Respuesta_Cliente/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta_Cliente respuesta_Cliente = db.Respuesta_Clientes.Find(id);
            if (respuesta_Cliente == null)
            {
                return HttpNotFound();
            }
            return View(respuesta_Cliente);
        }

        // GET: Respuesta_Cliente/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.Estado_QR_Estado_DestinoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion");
            ViewBag.Estado_QR_Estado_OrigenID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion");
            ViewBag.Queja_QuejaID = new SelectList(db.Quejas, "QRID", "QRID");
            ViewBag.Reclamacion_ReclamacionID = new SelectList(db.Reclamacions, "QRID", "QRID");
            ViewBag.ValoracionID = new SelectList(db.Valoracions, "ValoracionID", "Descripcion");
            return View();
        }

        // POST: Respuesta_Cliente/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "RespuestaID,ValoracionID,Fecha,Detalle,Estado_QR_Estado_OrigenID,Estado_QR_Estado_DestinoID,Queja_QuejaID,Reclamacion_ReclamacionID")] Respuesta_Cliente respuesta_Cliente)
        {
            if (ModelState.IsValid)
            {
                db.Respuesta_Clientes.Add(respuesta_Cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Estado_QR_Estado_DestinoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", respuesta_Cliente.Estado_QR_Estado_DestinoID);
            ViewBag.Estado_QR_Estado_OrigenID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", respuesta_Cliente.Estado_QR_Estado_OrigenID);
            ViewBag.Queja_QuejaID = new SelectList(db.Quejas, "QRID", "QRID", respuesta_Cliente.Queja_QuejaID);
            ViewBag.Reclamacion_ReclamacionID = new SelectList(db.Reclamacions, "QRID", "QRID", respuesta_Cliente.Reclamacion_ReclamacionID);
            ViewBag.ValoracionID = new SelectList(db.Valoracions, "ValoracionID", "Descripcion",respuesta_Cliente.ValoracionID);
            return View(respuesta_Cliente);
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
            RespuestaClienteQuejaViewModel viewmodel = new RespuestaClienteQuejaViewModel()
            {
                QuejaViewModel = new QuejaViewModel { Queja = queja, Respuestas = new List<Respuesta>() },
                Respuesta_Cliente = new Respuesta_Cliente
                {
                    Queja_QuejaID = queja.QRID,
                    Estado_QR_Estado_OrigenID = queja.Estado_QR_EstadoID
                }
            };
            AddListRespuestasQuejas(queja, viewmodel);
            Respuesta_Cliente respuesta_Cliente = viewmodel.Respuesta_Cliente;

            AddViewBagCreate(respuesta_Cliente);

            return View(viewmodel);
        }

        private void AddListRespuestasQuejas(Queja queja, RespuestaClienteQuejaViewModel viewmodel)
        {
            int? id_queja = queja.QRID;
            List<Respuesta_Empleado> respuesta_Empleados = db.Respuesta_Empleados.Where(e => e.Queja_QuejaID == id_queja).ToList();
            List<Respuesta_Cliente> respuesta_Clientes = db.Respuesta_Clientes.Where(e => e.Queja_QuejaID == id_queja).ToList();
            if(viewmodel.QuejaViewModel.Respuestas == null)
            {
                viewmodel.QuejaViewModel.Respuestas = new List<Respuesta>();
            }
            viewmodel.QuejaViewModel.Respuestas.AddRange(respuesta_Clientes);
            viewmodel.QuejaViewModel.Respuestas.AddRange(respuesta_Empleados);
            viewmodel.QuejaViewModel.Respuestas.Sort(ModelHelpers.CompareRespuestas);
        }

        private void AddViewBagCreate(Respuesta_Cliente respuesta_Cliente)
        {

            ViewBag.Valoraciones = db.Valoracions.ToList();
            ViewBag.ValoracionesArray = db.Valoracions.ToArray();

            ViewBag.ID_Estado_Destino = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", respuesta_Cliente.Estado_QR_Estado_DestinoID);
        }

        private int? CheckNull(int? destino, int? origen)
        {
            return destino == null ? origen : destino;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromQueja(RespuestaClienteQuejaViewModel viewModel)
        {

            Respuesta_Cliente respuesta_Cliente = viewModel.Respuesta_Cliente;
            Queja queja = db.Quejas.Find(viewModel.QuejaViewModel.Queja.QRID);
            respuesta_Cliente.Queja_QuejaID = queja.QRID;

            if (ValoracionInvalida(respuesta_Cliente))
            {
                RestartRespuestaClienteQuejaViewModel(viewModel, respuesta_Cliente, queja);
                ModelState.AddModelError("", "Valoración inválida");
                return View(viewModel);
            }
            if (EstadoInvalido(respuesta_Cliente))
            {
                RestartRespuestaClienteQuejaViewModel(viewModel, respuesta_Cliente, queja);
                ModelState.AddModelError("", "Estado inválido");
                return View(viewModel);
            }

                if (ModelState.IsValid)
                {

                    respuesta_Cliente.Fecha = DateTime.Now;



                    queja.Estado_QR_EstadoID = respuesta_Cliente.Estado_QR_Estado_DestinoID;

                    db.Respuesta_Clientes.Add(respuesta_Cliente);
                    db.Entry(queja).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Quejas");
                }
            RestartRespuestaClienteQuejaViewModel(viewModel, respuesta_Cliente, queja);
            return View(viewModel);
        }

        private static bool EstadoInvalido(Respuesta_Cliente respuesta_Cliente)
        {
            Estado_QR_Helper helper = new Estado_QR_Helper();
            bool EstadoReabierto = respuesta_Cliente.Estado_QR_Estado_DestinoID == helper.GetEstadoByDescripcion(Estado_QR_Helper.REABIERTO_DISCONFORMIDAD).EstadoID;
            bool EstadoCerrado = respuesta_Cliente.Estado_QR_Estado_DestinoID == helper.GetEstadoByDescripcion(Estado_QR_Helper.CERRADO).EstadoID;
            //Debe ser falso si es uno de los estados válidos
            return !(EstadoReabierto || EstadoCerrado);
        }

        private bool ValoracionInvalida(Respuesta_Cliente respuesta_Cliente)
        {
            return !db.Valoracions.Any(v => v.ValoracionID == respuesta_Cliente.ValoracionID);
        }

        private void RestartRespuestaClienteQuejaViewModel(RespuestaClienteQuejaViewModel viewModel, Respuesta_Cliente respuesta_Cliente, Queja queja)
        {
            viewModel.QuejaViewModel.Queja = queja;
            
            AddListRespuestasQuejas(queja, viewModel);
            AddViewBagCreatePost(respuesta_Cliente);
        }


        private void AddViewBagCreatePost(Respuesta_Cliente respuesta_Cliente)
        {
            ViewBag.Valoraciones = db.Valoracions.ToList();
            ViewBag.ID_Estado_Origen = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", respuesta_Cliente.Estado_QR_Estado_DestinoID);
            ViewBag.ID_Estado_Destino = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", respuesta_Cliente.Estado_QR_Estado_DestinoID);
        }


        // GET: Respuesta_Empleado/Create
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
            RespuestaClienteReclamacionViewModel viewmodel = new RespuestaClienteReclamacionViewModel()
            {
                ReclamacionViewModel = new ReclamacionViewModel { Reclamacion = reclamacion, Respuestas = new List<Respuesta>() },
                Respuesta_Cliente = new Respuesta_Cliente
                {
                    Reclamacion_ReclamacionID = reclamacion.QRID,
                    Estado_QR_Estado_OrigenID = reclamacion.Estado_QR_EstadoID
                }
            };
            AddListRespuestasReclamaciones(reclamacion, viewmodel);
            Respuesta_Cliente respuesta_Cliente = viewmodel.Respuesta_Cliente;


            AddViewBagCreate(respuesta_Cliente);

            return View(viewmodel);
        }

        private void AddListRespuestasReclamaciones(Reclamacion reclamacion, RespuestaClienteReclamacionViewModel viewmodel)
        {
            int? id_reclamacion = reclamacion.QRID;
            List<Respuesta_Empleado> respuesta_Empleados = db.Respuesta_Empleados.Where(e => e.Reclamacion_ReclamacionID == id_reclamacion).ToList();
            List<Respuesta_Cliente> respuesta_Clientes = db.Respuesta_Clientes.Where(e => e.Reclamacion_ReclamacionID == id_reclamacion).ToList();
            viewmodel.ReclamacionViewModel.Respuestas.AddRange(respuesta_Clientes);
            viewmodel.ReclamacionViewModel.Respuestas.AddRange(respuesta_Empleados);
            viewmodel.ReclamacionViewModel.Respuestas.Sort(ModelHelpers.CompareRespuestas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromReclamacion(RespuestaClienteReclamacionViewModel viewModel)
        {

            Respuesta_Cliente respuesta_Cliente = viewModel.Respuesta_Cliente;
            Reclamacion reclamacion = db.Reclamacions.Find(viewModel.ReclamacionViewModel.Reclamacion.QRID);

            if (ModelState.IsValid)
            {
                respuesta_Cliente.Reclamacion_ReclamacionID = reclamacion.QRID;
                respuesta_Cliente.Fecha = DateTime.Now;



                reclamacion.Estado_QR_EstadoID = respuesta_Cliente.Estado_QR_Estado_DestinoID;

                db.Respuesta_Clientes.Add(respuesta_Cliente);
                db.Entry(reclamacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Reclamacions");
            }

            AddViewBagCreatePost(respuesta_Cliente);
            return View(viewModel);
        }



        // GET: Respuesta_Cliente/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta_Cliente respuesta_Cliente = db.Respuesta_Clientes.Find(id);
            if (respuesta_Cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.Estado_QR_Estado_DestinoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", respuesta_Cliente.Estado_QR_Estado_DestinoID);
            ViewBag.Estado_QR_Estado_OrigenID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", respuesta_Cliente.Estado_QR_Estado_OrigenID);
            ViewBag.Queja_QuejaID = new SelectList(db.Quejas, "QRID", "QRID", respuesta_Cliente.Queja_QuejaID);
            ViewBag.Reclamacion_ReclamacionID = new SelectList(db.Reclamacions, "QRID", "QRID", respuesta_Cliente.Reclamacion_ReclamacionID);
            return View(respuesta_Cliente);
        }

        // POST: Respuesta_Cliente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "RespuestaID,ValoracionID,Fecha,Detalle,Estado_QR_Estado_OrigenID,Estado_QR_Estado_DestinoID,Queja_QuejaID,Reclamacion_ReclamacionID")] Respuesta_Cliente respuesta_Cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(respuesta_Cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Estado_QR_Estado_DestinoID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", respuesta_Cliente.Estado_QR_Estado_DestinoID);
            ViewBag.Estado_QR_Estado_OrigenID = new SelectList(db.Estado_QRs, "EstadoID", "Descripcion", respuesta_Cliente.Estado_QR_Estado_OrigenID);
            ViewBag.Queja_QuejaID = new SelectList(db.Quejas, "QRID", "QRID", respuesta_Cliente.Queja_QuejaID);
            ViewBag.Reclamacion_ReclamacionID = new SelectList(db.Reclamacions, "QRID", "QRID", respuesta_Cliente.Reclamacion_ReclamacionID);
            return View(respuesta_Cliente);
        }

        // GET: Respuesta_Cliente/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta_Cliente respuesta_Cliente = db.Respuesta_Clientes.Find(id);
            if (respuesta_Cliente == null)
            {
                return HttpNotFound();
            }
            return View(respuesta_Cliente);
        }

        // POST: Respuesta_Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Respuesta_Cliente respuesta_Cliente = db.Respuesta_Clientes.Find(id);
            db.Respuesta_Clientes.Remove(respuesta_Cliente);
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
