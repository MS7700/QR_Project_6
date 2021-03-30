namespace QR_Project_6.Models
{
    
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class QR_Model : DbContext
    {
        // El contexto se ha configurado para usar una cadena de conexión 'QR_Model' del archivo 
        // de configuración de la aplicación (App.config o Web.config). De forma predeterminada, 
        // esta cadena de conexión tiene como destino la base de datos 'QR_Project_6.Models.Model.QR_Model' de la instancia LocalDb. 
        // 
        // Si desea tener como destino una base de datos y/o un proveedor de base de datos diferente, 
        // modifique la cadena de conexión 'QR_Model'  en el archivo de configuración de la aplicación.
        public QR_Model()
            : base("name=QR_Model")
        {
            Database.SetInitializer(new QR_ModelInitializer());
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

        // Agregue un DbSet para cada tipo de entidad que desee incluir en el modelo. Para obtener más información 
        // sobre cómo configurar y usar un modelo Code First, vea http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}