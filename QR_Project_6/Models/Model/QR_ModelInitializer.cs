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
            Estados.Estado_QR_Helper helper = new Estados.Estado_QR_Helper();
            return helper.GetEstado_QRs();
        }

        private List<Pais> paises()
        {
            return new List<Pais>()
            {
                new Pais(){ Nombre_Pais = "República Dominicana"},
                new Pais(){ Nombre_Pais = "México"},
                new Pais(){ Nombre_Pais = "Puerto Rico"},
                new Pais(){ Nombre_Pais = "Estados Unidos"}
            };
        }
        private List<Sucursal> sucursals()
        {
            return new List<Sucursal>()
            {
                new Sucursal(){ Nombre = "Villa Consuelo"},
                new Sucursal(){ Nombre = "Arroyo Hondo"},
                new Sucursal(){ Nombre = "Villa Mella"}
            };
        }
        private List<Departamento> departamentos()
        {
            return new List<Departamento>()
            {
                new Departamento(){ Nombre = "Ventas"},
                new Departamento(){ Nombre = "Recursos Humanos"},
                new Departamento(){ Nombre = "Servicio al cliente"},
                new Departamento(){ Nombre = "Transporte"},
                new Departamento(){ Nombre = "Gerencia"},
                new Departamento(){ Nombre = "Soporte técnico"}
            };
        }

    }
    
    
}