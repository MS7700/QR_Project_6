using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QR_Project_6.Models
{
    public class QR_ModelInitializer : DropCreateDatabaseIfModelChanges<QR_Model>
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
            context.SaveChanges();
            //context.Direccions.AddRange(direccions());
            direccions(context);
            context.SaveChanges();
            //context.Empleados.AddRange(empleados());
            empleados(context);
            clientes(context);
            productos(context);
            base.Seed(context);
        }


        private void direccions(QR_Model context)
        {
            Pais pais = context.Paises.Where(p => p.Nombre_Pais == "República Dominicana").FirstOrDefault();
            context.Direccions.AddRange(
                new List<Direccion>()
                {
                    new Direccion()
                    {
                        Provincia = "Distrito Nacional",
                        Sector = "Arroyo Hondo",
                        Municipio = "Distrito Nacional",
                        Barrio = "Arroyo Hondo",
                        Direccion_1 = "Calle F",
                        Direccion_2 = "45",
                        Pais = pais
                    },
                    new Direccion()
                    {
                        Provincia = "Distrito Nacional",
                        Sector = "Villa Consuelo",
                        Municipio = "Distrito Nacional",
                        Barrio = "Villa Consuelo",
                        Direccion_1 = "Calle La Flor",
                        Direccion_2 = "24",
                        Pais = pais
                    },
                    new Direccion()
                    {
                        Provincia = "Santiago de Los Caballeros",
                        Sector = "Las Palmas",
                        Municipio = "Santiago",
                        Barrio = "Los Topos",
                        Direccion_1 = "Calle J",
                        Direccion_2 = "53",
                        Pais = pais
                    },
                }
            );
            
        }

        private void empleados(QR_Model context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var admin = UserManager.FindByName("admin@gmail.com");
            var empleado = UserManager.FindByName("empleado@gmail.com");
            Direccion direccion1 = context.Direccions.Find(1);
            Direccion direccion2 = context.Direccions.Find(2);
            Estado_Cliente estado = context.Estado_Clientes.Where(e => e.Descripcion == "Activo").FirstOrDefault();
            Tipo_Identificacion tipo = context.Tipo_Identificacions.Where(e => e.Descripcion == "Cédula").FirstOrDefault();
            Sucursal sucursal1 = context.Sucursals.Where(e => e.Nombre == "Arroyo Hondo").FirstOrDefault();
            Sucursal sucursal2 = context.Sucursals.Where(e => e.Nombre == "Villa Consuelo").FirstOrDefault();
            Departamento departamento1 = context.Departamentos.Where(e => e.Nombre == "Soporte técnico").FirstOrDefault();
            Departamento departamento2 = context.Departamentos.Where(e => e.Nombre == "Ventas").FirstOrDefault();

            context.Empleados.AddRange(
                new List<Empleado>()
                {
                    new Empleado()
                    {
                        Identificacion = "40200390074",
                        Nombre = "Manuel",
                        Apellido = "López",
                        Fecha_Ingreso = new DateTime(2020,8,15),
                        Telefono = "8297441345",
                        UserNameID = admin.Id,
                        Email = admin.Email,
                        Direccion = direccion1,
                        Estado_Cliente = estado,
                        Tipo_Identificacion = tipo,
                        Departamento = departamento1,
                        Sucursal = sucursal1

                    },
                    new Empleado()
                    {
                        Identificacion = "55544433322",
                        Nombre = "Benjamín",
                        Apellido = "Ortiz",
                        Fecha_Ingreso = new DateTime(2021,3,5),
                        Telefono = "8091112233",
                        UserNameID = empleado.Id,
                        Email = empleado.Email,
                        Direccion = direccion2,
                        Estado_Cliente = estado,
                        Tipo_Identificacion = tipo,
                        Departamento = departamento2,
                        Sucursal = sucursal2

                    }
                }
                );
            
        }

        private void clientes(QR_Model context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var cliente = UserManager.FindByName("cliente@gmail.com");
            Direccion direccion1 = context.Direccions.Find(3);
            Estado_Cliente estado = context.Estado_Clientes.Where(e => e.Descripcion == "Activo").FirstOrDefault();
            Tipo_Identificacion tipo = context.Tipo_Identificacions.Where(e => e.Descripcion == "Cédula").FirstOrDefault();

            context.Clientes.AddRange(
                new List<Cliente>()
                {
                    new Cliente()
                    {
                        Identificacion = "00122233312",
                        Nombre = "Pedro",
                        Apellido = "Almánzar",
                        Fecha_Ingreso = new DateTime(2021,2,22),
                        Telefono = "8091112233",
                        UserNameID = cliente.Id,
                        Email = cliente.Email,
                        Direccion = direccion1,
                        Estado_Cliente = estado,
                        Tipo_Identificacion = tipo,
                 
                    }
                }
                );

        }


        private void productos(QR_Model context)
        {
            Tipo_Producto tipoSuculenta = context.Tipo_Productos.Where(p => p.Descripcion == "Suculenta").FirstOrDefault();
            Tipo_Producto tipoMaceta = context.Tipo_Productos.Where(p => p.Descripcion == "Maceta").FirstOrDefault();
            Tipo_Producto tipoOrquidea = context.Tipo_Productos.Where(p => p.Descripcion == "Orquídea").FirstOrDefault();
            context.Productos.AddRange(
                new List<Producto>()
                {
                    new Producto()
                    {
                        Nombre = "Aloe Vera",
                        Monto = 300,
                        Tipo_Producto = tipoSuculenta
                    },
                    new Producto()
                    {
                        Nombre = "Maceta pequeña",
                        Monto = 200,
                        Tipo_Producto = tipoMaceta
                    },
                    new Producto()
                    {
                        Nombre = "Orquídea azul",
                        Monto = 1200,
                        Tipo_Producto = tipoOrquidea
                    },
                }
            );

        }


        //private List<Direccion> direccions()
        //{
        //    return new List<Direccion>()
        //    {
        //        new Direccion()
        //        {
        //            Provincia = "Distrito Nacional",
        //            Sector = "Arroyo Hondo",
        //            Municipio = "Distrito Nacional",
        //            Barrio = "Arroyo Hondo",
        //            Direccion_1 = "Calle F",
        //            Direccion_2 = "45",
        //            Pais_PaisID = 0
        //        }
        //    };
        //}

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
                new Tipo_Producto(){ Descripcion = "Maceta"},
                new Tipo_Producto(){ Descripcion = "Orquídea"}
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

        

        //private List<Empleado> empleados()
        //{
        //    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        //    var admin = UserManager.FindByName("admin@gmail.com");
        //    return new List<Empleado>()
        //    {
        //        new Empleado()
        //        {
        //            Identificacion = "40200390074",
        //            Nombre = "Manuel",
        //            Apellido = "López",
        //            Fecha_Ingreso = new DateTime(2020,8,15),
        //            Telefono = "8297441345",
        //            UserNameID = admin.Id,
        //            Direccion_DireccionID = 0,
        //            Estado_Cliente_Estado_ClienteID = 0,
        //            Tipo_Identificacion_Tipo_IdentificacionID = 0,
        //            Departamento_DepartamentoID = 6,
        //            Sucursal_SucursalID = 2

        //        }
        //    };
        //}

    }
    
    
}