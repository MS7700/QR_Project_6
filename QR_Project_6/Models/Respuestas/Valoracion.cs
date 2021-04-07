using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QR_Project_6.Models
{
    public partial class Valoracion
    {
        public Nullable<int> ValoracionID { get; set; }
        public string Descripcion { get; set; }
        public int Valor { get; set; }
    }
}