using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QR_Project_6.Models
{
    public class EmpleadoViewModel
    {
        public Empleado Empleado { get; set; }
        public Direccion Direccion { get; set; }
    }

    public class ClienteViewModel
    {
        public Cliente Cliente { get; set; }
        public Direccion Direccion { get; set; }
    }

    public class QuejaViewModel
    {
        public Queja Queja { get; set; }
        public List<Respuesta> Respuestas { get; set; }
        //TODO: Lista de tipo respuesta interfaz con las respuestas de empleados y clientes
    }
    
    public class RespuestaEmpleadoQuejaViewModel
    {
        public QuejaViewModel QuejaViewModel { get; set; }
        public Respuesta_Empleado Respuesta_Empleado { get; set; }
    }
    public class RespuestaClienteQuejaViewModel
    {
        public QuejaViewModel QuejaViewModel { get; set; }
        public Respuesta_Cliente Respuesta_Cliente { get; set; }
    }
    public class ReclamacionViewModel
    {
        public Reclamacion Reclamacion { get; set; }
        public List<Respuesta> Respuestas { get; set; }
        //TODO: Lista de tipo respuesta interfaz con las respuestas de empleados y clientes
    }

    public class RespuestaEmpleadoReclamacionViewModel
    {
        public ReclamacionViewModel ReclamacionViewModel { get; set; }
        public Respuesta_Empleado Respuesta_Empleado { get; set; }
    }
    public class RespuestaClienteReclamacionViewModel
    {
        public ReclamacionViewModel ReclamacionViewModel { get; set; }
        public Respuesta_Cliente Respuesta_Cliente { get; set; }
    }
    public class SatisfaccionViewModel
    {
        public List<ValoracionViewModel> Valoracions { get; set; }
        [Display(Name = "Grado de satisfacción")]
        public decimal GradoSatisfaccion { get; set; }
    }
    public class ValoracionViewModel
    {
        public string Valoracion { get; set; }
        public int Cantidad { get; set; }
        public decimal Porcentaje { get; set; }
    }

    public class QuejaReportViewModel
    {
        [Display(Name = "Número de queja")]
        public int Numero { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Departamento { get; set; }
        public string Sucursal { get; set; }
        public string Empleado { get; set; }
        public string Estado { get; set; }
        [Display(Name = "Tipo de queja")]
        public string Tipo { get; set; }
        public string Comentario { get; set; }
        
    }
    public class ReclamacionReportViewModel
    {
        [Display(Name = "Número de queja")]
        public int Numero { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Departamento { get; set; }
        public string Sucursal { get; set; }
        public string Empleado { get; set; }
        public string Estado { get; set; }
        [Display(Name = "Tipo de queja")]
        public string Tipo { get; set; }
        public string Comentario { get; set; }

    }
    public class EstadoViewModel
    {
        public string Estado { get; set; }
        public int Cantidad { get; set; }
        public decimal Porcentaje { get; set; }
    }
    public class QuejasReportViewModel
    {
        public List<QuejaReportViewModel> Quejas { get; set; }
        public List<EstadoViewModel> Estados { get; set; }
    }
    public class ReclamacionesReportViewModel
    {
        public List<ReclamacionReportViewModel> Reclamaciones { get; set; }
        public List<EstadoViewModel> Estados { get; set; }
    }
}