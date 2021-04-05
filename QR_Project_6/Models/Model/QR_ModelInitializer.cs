using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            context.Tipo_Productos.AddRange(tipo_Productos());
            context.Tipo_Quejas.AddRange(tipo_Quejas());
            context.Tipo_Reclamacions.AddRange(tipo_Reclamacions());
            context.Estado_QRs.AddRange(estado_QRs());
            context.Paises.AddRange(paises());
            context.Sucursals.AddRange(sucursals());
            context.Departamentos.AddRange(departamentos());
            context.Estado_Clientes.AddRange(estado_Clientes());
            context.Estado_Transaccions.AddRange(estado_Transaccions());
            //context.Direccions.AddRange(direccions());
            //context.Empleados.AddRange(empleados());

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

        private List<Estado_Cliente> estado_Clientes()
        {
            return new List<Estado_Cliente>()
            {
                new Estado_Cliente(){ Descripcion = "Activo"},
                new Estado_Cliente(){ Descripcion = "Inactivo"}
            };
        }

        private List<Estado_Transaccion> estado_Transaccions()
        {
            return new List<Estado_Transaccion>()
            {
                new Estado_Transaccion(){ Descripcion = "Pagada"},
                new Estado_Transaccion(){ Descripcion = "Pendiente"}
            };
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

        private List<Direccion> direccions()
        {
            return new List<Direccion>()
            {
                new Direccion()
                {
                    Provincia = "Distrito Nacional",
                    Sector = "Arroyo Hondo",
                    Municipio = "Distrito Nacional",
                    Barrio = "Arroyo Hondo",
                    Direccion_1 = "Calle F",
                    Direccion_2 = "45",
                    Pais_PaisID = 0
                }
            };
        }

        private List<Empleado> empleados()
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var admin = UserManager.FindByName("admin@gmail.com");
            return new List<Empleado>()
            {
                new Empleado()
                {
                    Identificacion = "40200390074",
                    Nombre = "Manuel",
                    Apellido = "López",
                    Fecha_Ingreso = new DateTime(2020,8,15),
                    Telefono = "8297441345",
                    UserNameID = admin.Id,
                    Direccion_DireccionID = 0,
                    Estado_Cliente_Estado_ClienteID = 0,
                    Tipo_Identificacion_Tipo_IdentificacionID = 0,
                    Departamento_DepartamentoID = 6,
                    Sucursal_SucursalID = 2

                }
            };
        }

    }
    
    
}