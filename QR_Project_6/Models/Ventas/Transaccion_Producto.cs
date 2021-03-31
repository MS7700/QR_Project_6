using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QR_Project_6.Models
{
    public partial class Transaccion_Producto
    {
        [Key, Column(Order = 0)]
        public int TransaccionID { get; set; }
        [Key, Column(Order = 1)]
        public int ProductoID { get; set; }
        public Nullable<int> Cantidad_Producto { get; set; }

        public virtual Producto Producto { get; set; }
        public virtual Transaccion Transaccion { get; set; }
    }
}