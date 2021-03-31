namespace QR_Project_6.Models
{
    using QR_Project_6.Models.Model;
    using System;
    using System.Data.Entity;
    using System.Linq;

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