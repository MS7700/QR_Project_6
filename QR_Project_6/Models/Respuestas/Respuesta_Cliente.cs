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

    public partial class Respuesta_Cliente : Respuesta
    {
        [Key]
        public override Nullable<int> RespuestaID { get; set; }
        //public Nullable<int> Valoracion { get; set; }
        public Nullable<int> ValoracionID { get; set; }
        public virtual Valoracion Valoracion { get; set; }

    }
}
