using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QR_Project_6.Models.Estados
{
    public class Estado_QR_Helper
    {
        public const string ABIERTO = "Abierto";
        public const string REABIERTO_DISCONFORMIDAD = "Reabierto por inconformidad";
        public const string REDIRIGIDO_DEPARTAMENTO = "Redirigido a departamento";
        public const string REDIRIGIDO_SUCURSAL = "Redirigido a sucursal";
        public const string REDIRIGIDO_EMPLEADO = "Redirigido a empleado";
        public const string PENDIENTE_VALORACION = "Pendiente valoración";
        public const string CERRADO = "Cerrado";

        private List<Estado_QR> estado_QRs = new List<Estado_QR>()
        {
            new Estado_QR(){ EstadoID = 0, Descripcion= ABIERTO },
            new Estado_QR(){ EstadoID = 1, Descripcion= REABIERTO_DISCONFORMIDAD },
            new Estado_QR(){ EstadoID = 2, Descripcion= REDIRIGIDO_DEPARTAMENTO },
            new Estado_QR(){ EstadoID = 3, Descripcion= REDIRIGIDO_SUCURSAL },
            new Estado_QR(){ EstadoID = 4, Descripcion= REDIRIGIDO_EMPLEADO },
            new Estado_QR(){ EstadoID = 5, Descripcion= PENDIENTE_VALORACION },
            new Estado_QR(){ EstadoID = 6, Descripcion= CERRADO }
        }
        public static Estado_QR GetEstadoByDescripcion(string descripcion)
        {

            return;
        }
        public static int? GetIdByDescripcion(DbContext db, string descripcion)
        {

            return db.Estado_QR.Where(e => e.Descripcion == descripcion).FirstOrDefault<Estado_QR>().ID_Estado_QR;
        }
    }
}