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
    using System.ComponentModel.DataAnnotations;

    public partial class Respuesta_Empleado : Respuesta
    {
        [Key]
        public override Nullable<int> RespuestaID { get; set; }
    
        public virtual Departamento Departamento_Origen { get; set; }
        public virtual Departamento Departamento_Destino { get; set; }
        public virtual Empleado Empleado_Origen { get; set; }
        public virtual Empleado Empleado_Destino { get; set; }
        public virtual Sucursal Sucursal_Origen { get; set; }
        public virtual Sucursal Sucursal_Destino { get; set; }
    }
}
