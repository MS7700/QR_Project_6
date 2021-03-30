using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QR_Project_6.Models
{
    public abstract class Tipo
    {
        public virtual int TipoID { get; set; }
        public string Descripcion { get; set; }
    }
}