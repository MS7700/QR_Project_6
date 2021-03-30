using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QR_Project_6.Models
{
    public class QR_ModelInitializer : DropCreateDatabaseAlways<QR_Model>
    {
        protected override void Seed(QR_Model context)
        {
            context.Tipo_Identificacions.AddRange(tipo_Identificacions());
            base.Seed(context);
        }
        private List<Tipo_Identificacion> tipo_Identificacions()
        {
            return new List<Tipo_Identificacion>()
            {
                new Tipo_Identificacion(){ Descripcion = "Cédula"},
                new Tipo_Identificacion(){ Descripcion = "Pasaporte"}
            };
        }
        private List<Tipo_Producto> tipo_Productos()
        {
            return new List<Tipo_Producto>()
            {
                new Tipo_Producto(){ Descripcion = "Suculenta"},
                new Tipo_Producto(){ Descripcion = "Cáctus"},
                new Tipo_Producto(){ Descripcion = "Maceta"}
            };
        }

        private List<Tipo_Queja> tipo_Quejas()
        {
            return new List<Tipo_Queja>()
            {
                new Tipo_Queja(){ Descripcion = "Mal servicio"},
                new Tipo_Queja(){ Descripcion = "Cobros compulsivos"},
                new Tipo_Queja(){ Descripcion = "Tardanza en la entrega"}
            };
        }
        private List<Tipo_Reclamacion> tipo_Reclamacions()
        {
            return new List<Tipo_Reclamacion>()
            {
                new Tipo_Reclamacion(){ Descripcion = "Mal servicio"},
                new Tipo_Reclamacion(){ Descripcion = "Cobros compulsivos"},
                new Tipo_Reclamacion(){ Descripcion = "Tardanza en la entrega"},
                new Tipo_Reclamacion(){ Descripcion = "Producto en mal estado"}
            };
        }
        private List<Estado_QR> estado_QRs()
        {
            return new List<Estado_QR>()
            {
                new Estado_QR(){ Descripcion = "Mal servicio"},
                new Estado_QR(){ Descripcion = "Cobros compulsivos"},
                new Estado_QR(){ Descripcion = "Tardanza en la entrega"},
                new Estado_QR(){ Descripcion = "Producto en mal estado"}
            };
        }

    }
    
    
}