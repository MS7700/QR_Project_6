//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QR_Project_6.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Direccion
    {
        
        public int DireccionID { get; set; }
        public string Provincia { get; set; }
        public string Sector { get; set; }
        public string Municipio { get; set; }
        public string Barrio { get; set; }
        public string Direccion_1 { get; set; }
        public string Direccion_2 { get; set; }
        public Nullable<int> PaisID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cliente> Cliente { get; set; }
        public virtual Pais Pais { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Empleado> Empleado { get; set; }
    }
}