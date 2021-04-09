namespace QR_Project_6.Models
{
    using QR_Project_6.Models.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class QR_Model : AbstractQR_Model
    {
        // El contexto se ha configurado para usar una cadena de conexión 'QR_Model' del archivo 
        // de configuración de la aplicación (App.config o Web.config). De forma predeterminada, 
        // esta cadena de conexión tiene como destino la base de datos 'QR_Project_6.Models.Model.QR_Model' de la instancia LocalDb. 
        // 
        // Si desea tener como destino una base de datos y/o un proveedor de base de datos diferente, 
        // modifique la cadena de conexión 'QR_Model'  en el archivo de configuración de la aplicación.
        public QR_Model()
            : base("QR_Model")
        {
            Database.SetInitializer(new QR_ModelInitializer());
        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            setForeignKeyLugares(modelBuilder);
            setForeignKeyClientes(modelBuilder);
            setForeignKeyEmpleados(modelBuilder);
            setForeigKeyQuejas(modelBuilder);
            setForeignKeyReclamacion(modelBuilder);
            setForeignKeyRespuesta_Cliente(modelBuilder);
            setForeignKeyRespuesta_Empleado(modelBuilder);
            setForeignKeyProducto(modelBuilder);
            setForeignKeyTransaccion(modelBuilder);
            setForeignKeyTrans_Prod(modelBuilder);


            base.OnModelCreating(modelBuilder);

            
        }

        private static void setForeignKeyTrans_Prod(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaccion_Producto>().HasRequired(p => p.Transaccion)
                                                              .WithMany(b => b.Transaccion_Productos).HasForeignKey(b => b.TransaccionID);
            modelBuilder.Entity<Transaccion_Producto>().HasRequired(p => p.Producto)
                                                  .WithMany(b => b.Transaccion_Productos).HasForeignKey(b => b.ProductoID);
        }

        private static void setForeignKeyTransaccion(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaccion>().HasOptional(p => p.Cliente)
                                                  .WithMany(b => b.Transaccion).HasForeignKey(b => b.Cliente_PersonaID);

            modelBuilder.Entity<Transaccion>().HasOptional(p => p.Empleado)
                                                    .WithMany(b => b.Transaccion).HasForeignKey(b => b.Empleado_PersonaID);
            modelBuilder.Entity<Transaccion>().HasOptional(p => p.Estado_Transaccion)
                                           .WithMany(b => b.Transaccion).HasForeignKey(b => b.Estado_Transaccion_EstadoID);
        }

        private static void setForeignKeyProducto(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().HasOptional(p => p.Tipo_Producto)
                                          .WithMany(b => b.Producto).HasForeignKey(b => b.Tipo_Producto_TipoID);
        }

        private static void setForeignKeyRespuesta_Empleado(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Estado_Origen)
            //                                                                .WithMany(b => b.Respuesta_Empleado).HasForeignKey(b => b.Estado_QR_Estado_OrigenID);
            //modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Estado_Destino)
            //                                        .WithMany(b => b.Respuesta_Empleado1).HasForeignKey(b => b.Estado_QR_Estado_DestinoID);

            //modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Departamento_Origen)
            //                                                    .WithMany(b => b.Respuesta_Empleado).HasForeignKey(b => b.Departamento_Departamento_OrigenID);
            //modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Departamento_Destino)
            //                                        .WithMany(b => b.Respuesta_Empleado1).HasForeignKey(b => b.Departamento_Departamento_DestinoID);

            //modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Empleado_Origen)
            //                                                    .WithMany(b => b.Respuesta_Empleado).HasForeignKey(b => b.Empleado_Empleado_OrigenID);
            //modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Empleado_Destino)
            //                                        .WithMany(b => b.Respuesta_Empleado1).HasForeignKey(b => b.Empleado_Empleado_DestinoID);

            //modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Sucursal_Origen)
            //                                                    .WithMany(b => b.Respuesta_Empleado).HasForeignKey(b => b.Sucursal_Sucursal_OrigenID);
            //modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Sucursal_Destino)
            //                                        .WithMany(b => b.Respuesta_Empleado1).HasForeignKey(b => b.Sucursal_Sucursal_DestinoID);

            //modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Queja)
            //                                        .WithMany(b => b.Respuesta_Empleado).HasForeignKey(b => b.Queja_QuejaID);
            //modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Reclamacion)
            //                                        .WithMany(b => b.Respuesta_Empleado).HasForeignKey(b => b.Reclamacion_ReclamacionID);


            modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Estado_Origen)
                                                                            .WithMany(b => b.Respuesta_Empleado).HasForeignKey(b => b.Estado_QR_Estado_OrigenID);
            modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Estado_Destino)
                                                    .WithMany(b => b.Respuesta_Empleado1).HasForeignKey(b => b.Estado_QR_Estado_DestinoID);

            modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Departamento_Origen)
                                                                .WithMany(b => b.Respuesta_Empleado).HasForeignKey(b => b.Departamento_Departamento_OrigenID);
            modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Departamento_Destino)
                                                    .WithMany(b => b.Respuesta_Empleado1).HasForeignKey(b => b.Departamento_Departamento_DestinoID);

            modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Empleado_Origen)
                                                                .WithMany(b => b.Respuesta_Empleado).HasForeignKey(b => b.Empleado_Empleado_OrigenID);
            modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Empleado_Destino)
                                                    .WithMany(b => b.Respuesta_Empleado1).HasForeignKey(b => b.Empleado_Empleado_DestinoID);

            modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Sucursal_Origen)
                                                                .WithMany(b => b.Respuesta_Empleado).HasForeignKey(b => b.Sucursal_Sucursal_OrigenID);
            modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Sucursal_Destino)
                                                    .WithMany(b => b.Respuesta_Empleado1).HasForeignKey(b => b.Sucursal_Sucursal_DestinoID);

            modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Queja)
                                                    .WithMany(b => b.Respuesta_Empleado).HasForeignKey(b => b.Queja_QuejaID).WillCascadeOnDelete(true);
            modelBuilder.Entity<Respuesta_Empleado>().HasOptional(p => p.Reclamacion)
                                                    .WithMany(b => b.Respuesta_Empleado).HasForeignKey(b => b.Reclamacion_ReclamacionID).WillCascadeOnDelete(true);
        }

        private static void setForeignKeyRespuesta_Cliente(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Respuesta_Cliente>().HasOptional(p => p.Estado_Origen)
                                                                .WithMany(b => b.Respuesta_Cliente).HasForeignKey(b => b.Estado_QR_Estado_OrigenID);
            modelBuilder.Entity<Respuesta_Cliente>().HasOptional(p => p.Estado_Destino)
                                                    .WithMany(b => b.Respuesta_Cliente1).HasForeignKey(b => b.Estado_QR_Estado_DestinoID);
            modelBuilder.Entity<Respuesta_Cliente>().HasOptional(p => p.Queja)
                                                    .WithMany(b => b.Respuesta_Cliente).HasForeignKey(b => b.Queja_QuejaID).WillCascadeOnDelete(true);
            modelBuilder.Entity<Respuesta_Cliente>().HasOptional(p => p.Reclamacion)
                                                    .WithMany(b => b.Respuesta_Cliente).HasForeignKey(b => b.Reclamacion_ReclamacionID).WillCascadeOnDelete(true);
        }

        private static void setForeignKeyReclamacion(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reclamacion>().HasOptional(p => p.Cliente)
                                                    .WithMany(b => b.Reclamacion).HasForeignKey(b => b.Cliente_ClienteID);
            modelBuilder.Entity<Reclamacion>().HasOptional(p => p.Departamento)
                                                    .WithMany(b => b.Reclamacion).HasForeignKey(b => b.Departamento_DepartamentoID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Reclamacion>().HasOptional(p => p.Empleado)
                                                    .WithMany(b => b.Reclamacion).HasForeignKey(b => b.Empleado_EmpleadoID);
            modelBuilder.Entity<Reclamacion>().HasOptional(p => p.Estado_QR)
                                                    .WithMany(b => b.Reclamacion).HasForeignKey(b => b.Estado_QR_EstadoID);
            modelBuilder.Entity<Reclamacion>().HasOptional(p => p.Sucursal)
                                                    .WithMany(b => b.Reclamacion).HasForeignKey(b => b.Sucursal_SucursalID);
            modelBuilder.Entity<Reclamacion>().HasOptional(p => p.Tipo_Reclamacion)
                                                    .WithMany(b => b.Reclamacion).HasForeignKey(b => b.Tipo_Reclamacion_TipoID);
        }

        private static void setForeigKeyQuejas(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Queja>().HasOptional(p => p.Cliente)
                                                                .WithMany(b => b.Queja).HasForeignKey(b => b.Cliente_ClienteID);
            modelBuilder.Entity<Queja>().HasOptional(p => p.Departamento)
                                                    .WithMany(b => b.Queja).HasForeignKey(b => b.Departamento_DepartamentoID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Queja>().HasOptional(p => p.Empleado)
                                                    .WithMany(b => b.Queja).HasForeignKey(b => b.Empleado_EmpleadoID);
            modelBuilder.Entity<Queja>().HasOptional(p => p.Estado_QR)
                                                    .WithMany(b => b.Queja).HasForeignKey(b => b.Estado_QR_EstadoID);
            modelBuilder.Entity<Queja>().HasOptional(p => p.Sucursal)
                                                    .WithMany(b => b.Queja).HasForeignKey(b => b.Sucursal_SucursalID);
            modelBuilder.Entity<Queja>().HasOptional(p => p.Tipo_Queja)
                                                    .WithMany(b => b.Queja).HasForeignKey(b => b.Tipo_Queja_TipoID);
        }

        private static void setForeignKeyEmpleados(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>().HasOptional(p => p.Direccion)
                                                    .WithMany(b => b.Empleado).HasForeignKey(b => b.Direccion_DireccionID);
            modelBuilder.Entity<Empleado>().HasOptional(p => p.Estado_Cliente)
                            .WithMany(b => b.Empleado).HasForeignKey(b => b.Estado_Cliente_Estado_ClienteID);
            modelBuilder.Entity<Empleado>().HasOptional(p => p.Tipo_Identificacion)
                            .WithMany(b => b.Empleado).HasForeignKey(b => b.Tipo_Identificacion_Tipo_IdentificacionID);
            modelBuilder.Entity<Empleado>().HasOptional(p => p.Direccion)
                            .WithMany(b => b.Empleado).HasForeignKey(b => b.Direccion_DireccionID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Empleado>().HasOptional(p => p.Departamento)
                            .WithMany(b => b.Empleado1).HasForeignKey(b => b.Departamento_DepartamentoID);
            modelBuilder.Entity<Empleado>().HasOptional(p => p.Sucursal)
                            .WithMany(b => b.Empleado1).HasForeignKey(b => b.Sucursal_SucursalID);
        }

        private static void setForeignKeyClientes(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasOptional(p => p.Direccion)
                                        .WithMany(b => b.Cliente).HasForeignKey(b => b.Direccion_DireccionID);
            modelBuilder.Entity<Cliente>().HasOptional(p => p.Estado_Cliente)
                            .WithMany(b => b.Cliente).HasForeignKey(b => b.Estado_Cliente_Estado_ClienteID);
            modelBuilder.Entity<Cliente>().HasOptional(p => p.Tipo_Identificacion)
                            .WithMany(b => b.Cliente).HasForeignKey(b => b.Tipo_Identificacion_Tipo_IdentificacionID);
            modelBuilder.Entity<Cliente>().HasOptional(p => p.Direccion)
                            .WithMany(b => b.Cliente).HasForeignKey(b => b.Direccion_DireccionID);
        }

        private void setForeignKeyLugares(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sucursal>().HasOptional(p => p.EmpleadoRepresentante)
                            .WithMany(b => b.Sucursal1).HasForeignKey(b => b.Empleado_PersonaID);
            modelBuilder.Entity<Departamento>().HasOptional(p => p.EmpleadoRepresentante)
                .WithMany(b => b.Departamento1).HasForeignKey(b => b.Empleado_PersonaID);
            modelBuilder.Entity<Direccion>().HasOptional(p => p.Pais)
                .WithMany(b => b.Direccion).HasForeignKey(b => b.Pais_PaisID);
            
        }


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