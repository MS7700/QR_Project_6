using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QR_Project_6.Models
{
    public abstract class Respuesta
    {
        
        public virtual Nullable<int> RespuestaID { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Detalle { get; set; }

        public virtual Estado_QR Estado_Origen { get; set; }
        public virtual Estado_QR Estado_Destino { get; set; }
        public virtual Queja Queja { get; set; }
        public virtual Reclamacion Reclamacion { get; set; }
    }
}