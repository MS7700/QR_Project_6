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

    public partial class Queja : QR
    {
        [Key]
        public override int QRID { get; set; }
        public int Tipo_Queja_TipoID { get; set; }
        public virtual Tipo_Queja Tipo_Queja { get; set; }
    }
}
