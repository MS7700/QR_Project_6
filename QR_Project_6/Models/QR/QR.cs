using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QR_Project_6.Models
{
    public abstract class QR
    {
        public virtual int QRID { get; set; }
        public string UserNameID { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Comentario { get; set; }
        public int Cliente_ClienteID { get; set; }
        public virtual Cliente Cliente { get; set; }
        public int Departamento_DepartamentoID { get; set; }
        public virtual Departamento Departamento { get; set; }
        public int Empleado_EmpleadoID { get; set; }
        public virtual Empleado Empleado { get; set; }
        public int Estado_QR_EstadoID { get; set; }
        public virtual Estado_QR Estado_QR { get; set; }
        public int Sucursal_SucursalID { get; set; }
        public virtual Sucursal Sucursal { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Respuesta_Empleado> Respuesta_Empleado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Respuesta_Cliente> Respuesta_Cliente { get; set; }
        
    }
}