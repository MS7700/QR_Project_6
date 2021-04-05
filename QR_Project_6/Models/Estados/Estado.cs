using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QR_Project_6.Models
{
    public abstract class Estado
    {
        
        public virtual Nullable<int> EstadoID { get; set; }
        public string Descripcion { get; set; }
    }
}