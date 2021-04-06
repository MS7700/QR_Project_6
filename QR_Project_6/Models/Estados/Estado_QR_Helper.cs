using QR_Project_6.Models.Model;
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

        private readonly List<Estado_QR> estado_QRs = new List<Estado_QR>()
        {
            new Estado_QR(){ EstadoID = 1, Descripcion= ABIERTO },
            new Estado_QR(){ EstadoID = 2, Descripcion= REABIERTO_DISCONFORMIDAD },
            new Estado_QR(){ EstadoID = 3, Descripcion= REDIRIGIDO_DEPARTAMENTO },
            new Estado_QR(){ EstadoID = 4, Descripcion= REDIRIGIDO_SUCURSAL },
            new Estado_QR(){ EstadoID = 5, Descripcion= REDIRIGIDO_EMPLEADO },
            new Estado_QR(){ EstadoID = 6, Descripcion= PENDIENTE_VALORACION },
            new Estado_QR(){ EstadoID = 7, Descripcion= CERRADO }
        };

        public List<Estado_QR> GetEstado_QRs()
        {
            return estado_QRs;
        }
        public Estado_QR GetEstadoByDescripcion(string descripcion)
        {
            return estado_QRs.Find(e => e.Descripcion == descripcion);
        }
        //public  Estado_QR GetEstadoByDescripcion(string descripcion)
        //{
            
        //    return;
        //}
        //public int? GetIdByDescripcion(string descripcion)
        //{

        //    //return db.Estado_QR.Where(e => e.Descripcion == descripcion).FirstOrDefault<Estado_QR>().ID_Estado_QR;
        //}
    }
}