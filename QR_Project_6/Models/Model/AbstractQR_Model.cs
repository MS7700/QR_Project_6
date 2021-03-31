using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QR_Project_6.Models.Model
{
    public abstract class AbstractQR_Model : DbContext
    {
        public AbstractQR_Model(string name)
            : base($"name={name}")
        {
            
        }
        public virtual DbSet<Estado_Cliente> Estado_Clientes { get; set; }
        public virtual DbSet<Estado_QR> Estado_QRs { get; set; }
        public virtual DbSet<Estado_Transaccion> Estado_Transaccions { get; set; }
        public virtual DbSet<Departamento> Departamentos { get; set; }
        public virtual DbSet<Direccion> Direccions { get; set; }
        public virtual DbSet<Pais> Paises { get; set; }
        public virtual DbSet<Sucursal> Sucursals { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<Queja> Quejas { get; set; }
        public virtual DbSet<Reclamacion> Reclamacions { get; set; }
        public virtual DbSet<Respuesta_Cliente> Respuesta_Clientes { get; set; }
        public virtual DbSet<Respuesta_Empleado> Respuesta_Empleados { get; set; }
        public virtual DbSet<Tipo_Identificacion> Tipo_Identificacions { get; set; }
        public virtual DbSet<Tipo_Queja> Tipo_Quejas { get; set; }
        public virtual DbSet<Tipo_Reclamacion> Tipo_Reclamacions { get; set; }
        public virtual DbSet<Tipo_Producto> Tipo_Productos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Transaccion> Transaccions { get; set; }
        public virtual DbSet<Transaccion_Producto> Transaccion_Productos { get; set; }
    }
}