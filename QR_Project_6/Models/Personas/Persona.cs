using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QR_Project_6.Models
{
    public abstract partial class Persona
    {

        //public virtual int PersonaID { get; set; }
        //public string Identificacion { get; set; }
        //public string Nombre { get; set; }
        //public string Apellido { get; set; }
        //public Nullable<System.DateTime> Fecha_Ingreso { get; set; }
        //public string Telefono { get; set; }
        //public string UserNameID { get; set; }
        //public int Direccion_DireccionID { get; set; }
        //public virtual Direccion Direccion { get; set; }
        //public int Estado_Cliente_Estado_ClienteID { get; set; }
        //public virtual Estado_Cliente Estado_Cliente { get; set; }
        //public int Tipo_Identificacion_Tipo_IdentificacionID { get; set; }
        //public virtual Tipo_Identificacion Tipo_Identificacion { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public abstract ICollection<Queja> Queja { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public abstract ICollection<Reclamacion> Reclamacion { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public abstract ICollection<Transaccion> Transaccion { get; set; }


        public virtual Nullable<int> PersonaID { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Nullable<System.DateTime> Fecha_Ingreso { get; set; }
        public string Telefono { get; set; }
        public string UserNameID { get; set; }
        public string Email { get; set; }
        public Nullable<int> Direccion_DireccionID { get; set; }
        public virtual Direccion Direccion { get; set; }
        public Nullable<int> Estado_Cliente_Estado_ClienteID { get; set; }
        public virtual Estado_Cliente Estado_Cliente { get; set; }
        public Nullable<int> Tipo_Identificacion_Tipo_IdentificacionID { get; set; }
        public virtual Tipo_Identificacion Tipo_Identificacion { get; set; }
    }
}