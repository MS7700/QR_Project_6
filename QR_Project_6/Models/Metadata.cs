using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QR_Project_6.Models
{

    public abstract partial class Persona
    {
        //[NotMapped]
        //public string NombreApellido
        //{
        //    get
        //    {
        //        return Nombre + " " + Apellido;
        //    }
        //}
    }

    [DisplayColumn("Identificacion")]
    [DisplayName("Cliente")]
    public class ClienteMetadata
    {
        [Display(Name = "Cliente")]
        public int PersonaID { get; set; }
        
        [MinLength(11, ErrorMessage = "Identificación inválida")]
        [MaxLength(11, ErrorMessage = "Identificación inválida")]
        [Display(Name = "No. Identificación")]
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de ingreso")]
        public Nullable<System.DateTime> Fecha_Ingreso { get; set; }
        
        [Phone]
        [StringLength(10,ErrorMessage = "Teléfono inválido", MinimumLength = 10)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "ID Usuario")]
        public string UserNameID { get; set; }

        [Display(Name = "Dirección")]
        public virtual Direccion Direccion { get; set; }
        [Display(Name = "Dirección")]
        public Nullable<int> Direccion_DireccionID { get; set; }
        [Display(Name = "Estado del cliente")]
        public virtual Estado_Cliente Estado_Cliente { get; set; }
        [Display(Name = "Estado del cliente")]
        public Nullable<int> Estado_Cliente_Estado_ClienteID { get; set; }
        [Display(Name = "Tipo de identificación")]
        public virtual Tipo_Identificacion Tipo_Identificacion { get; set; }
        [Display(Name = "Tipo de identificación")]
        public Nullable<int> Tipo_Identificacion_Tipo_IdentificacionID { get; set; }
    }

    [DisplayName("Departamento")]
    public class DepartamentoMetadata
    {
        [Display(Name = "Departamento")]
        public int DepartamentoID { get; set; }
        [Display(Name = "Departamento")]
        public string Nombre { get; set; }
        [Display(Name = "Representante")]
        public virtual Empleado EmpleadoRepresentante { get; set; }
        [Display(Name = "Representante")]
        public Nullable<int> Empleado_PersonaID { get; set; }
    }

    [DisplayColumn("DireccionID")]
    [DisplayName("Dirección")]
    public class DireccionMetadata
    {
        [Display(Name = "Dirección")]
        public int DireccionID { get; set; }
        public string Provincia { get; set; }
        public string Sector { get; set; }
        public string Municipio { get; set; }
        public string Barrio { get; set; }
        [Display(Name = "Dirección 1")]
        public string Direccion_1 { get; set; }
        [Display(Name = "Dirección 2")]
        public string Direccion_2 { get; set; }
        [Display(Name = "País")]
        public virtual Pais Pais { get; set; }
        [Display(Name = "País")]
        public Nullable<int> Pais_PaisID { get; set; }
    }

    [DisplayColumn("Identificacion")]
    [DisplayName("Empleado")]
    public class EmpleadoMetadata
    {
        [Display(Name = "Empleado")]
        public int PersonaID { get; set; }
        [Display(Name = "No. Identificación")]
        [MinLength(11, ErrorMessage = "Identificación inválida")]
        [MaxLength(11, ErrorMessage = "Identificación inválida")]
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de ingreso")]
        public Nullable<System.DateTime> Fecha_Ingreso { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [StringLength(10, ErrorMessage = "Teléfono inválido", MinimumLength = 10)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
        [Display(Name = "Email")]
        public string UserNameID { get; set; }
        [Display(Name = "Departamento")]
        public virtual Departamento Departamento { get; set; }
        [Display(Name = "Departamento")]
        public Nullable<int> Departamento_DepartamentoID { get; set; }

        [Display(Name = "Dirección")]
        public virtual Direccion Direccion { get; set; }
        [Display(Name = "Dirección")]
        public Nullable<int> Direccion_DireccionID { get; set; }
        [Display(Name = "Estado del empleado")]
        public virtual Estado_Cliente Estado_Cliente { get; set; }
        [Display(Name = "Estado del empleado")]
        public Nullable<int> Estado_Cliente_Estado_ClienteID { get; set; }
        [Display(Name = "Tipo de identificación")]
        public virtual Tipo_Identificacion Tipo_Identificacion { get; set; }
        [Display(Name = "Tipo de identificación")]
        public Nullable<int> Tipo_Identificacion_Tipo_IdentificacionID { get; set; }
        [Display(Name = "Sucursal")]
        public virtual Sucursal Sucursal { get; set; }
        [Display(Name = "Sucursal")]
        public Nullable<int> Sucursal_SucursalID { get; set; }

    }

    [DisplayName("Estado del cliente")]
    public class Estado_ClienteMetadata
    {
        [Display(Name = "Estado del cliente")]
        public virtual int EstadoID { get; set; }
        [Display(Name = "Estado del cliente")]
        public string Descripcion { get; set; }
    }

    [DisplayName("Estado")]
    public class Estado_QRMetadata
    {
        [Display(Name = "Estado")]
        public virtual int EstadoID { get; set; }
        [Display(Name = "Estado")]
        public string Descripcion { get; set; }
    }

    [DisplayName("Estado de transacción")]
    public class Estado_TransaccionMetadata
    {
        [Display(Name = "Estado de transacción")]
        public virtual int EstadoID { get; set; }
        [Display(Name = "Estado de transacción")]
        public string Descripcion { get; set; }
    }

    [DisplayName("País")]
    public class PaisMetadata
    {
        [Display(Name = "País")]
        public int PaisID { get; set; }
        [Display(Name = "País")]
        public string Nombre_Pais { get; set; }
    }
    [DisplayName("Producto")]
    public class ProductoMetadata
    {
        [Display(Name = "Producto")]
        public int ProductoID { get; set; }
        [Display(Name = "Nombre del producto")]
        public string Nombre { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Precio")]
        public Nullable<decimal> Monto { get; set; }
        [Display(Name = "Tipo de producto")]
        public virtual Tipo_Producto Tipo_Producto { get; set; }
        [Display(Name = "Tipo de producto")]
        public Nullable<int> Tipo_Producto_TipoID { get; set; }
    }

    [DisplayColumn("QRID")]
    [DisplayName("Queja")]
    public class QuejaMetadata
    {
        [Display(Name = "Queja")]
        public int QRID { get; set; }
        [Display(Name = "Redactor de la queja")]
        public string UserNameID { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        public Nullable<System.DateTime> Fecha { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comentario { get; set; }
        [Display(Name = "Sucursal")]
        public virtual Sucursal Sucursal { get; set; }
        [Display(Name = "Sucursal")]
        public Nullable<int> Sucursal_SucursalID { get; set; }
        
        [Display(Name = "Cliente")]
        public virtual Cliente Cliente { get; set; }
        [Display(Name = "Cliente")]
        public Nullable<int> Cliente_ClienteID { get; set; }
        [Display(Name = "Departamento")]
        public virtual Departamento Departamento { get; set; }
        [Display(Name = "Departamento")]
        public Nullable<int> Departamento_DepartamentoID { get; set; }
        [Display(Name = "Empleado")]
        public virtual Empleado Empleado { get; set; }
        [Display(Name = "Empleado")]
        public Nullable<int> Empleado_EmpleadoID { get; set; }
        [Display(Name = "Estado de queja")]
        public virtual Estado_QR Estado_QR { get; set; }
        [Display(Name = "Estado de queja")]
        public Nullable<int> Estado_QR_EstadoID { get; set; }
        [Display(Name = "Tipo de queja")]
        public virtual Tipo_Queja Tipo_Queja { get; set; }
        [Display(Name = "Tipo de queja")]
        public Nullable<int> Tipo_Queja_TipoID { get; set; }
    }

    [DisplayColumn("QRID")]
    [DisplayName("Reclamación")]
    public class ReclamacionMetadata
    {
        [Display(Name = "Reclamación")]
        public int QRID { get; set; }
        [Display(Name = "Redactor de la reclamación")]
        public string UserNameID { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        public Nullable<System.DateTime> Fecha { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string Comentario { get; set; }

        [Display(Name = "Sucursal")]
        public virtual Sucursal Sucursal { get; set; }
        [Display(Name = "Sucursal")]
        public Nullable<int> Sucursal_SucursalID { get; set; }

        [Display(Name = "Cliente")]
        public virtual Cliente Cliente { get; set; }
        [Display(Name = "Cliente")]
        public Nullable<int> Cliente_ClienteID { get; set; }
        [Display(Name = "Departamento")]
        public virtual Departamento Departamento { get; set; }
        [Display(Name = "Departamento")]
        public Nullable<int> Departamento_DepartamentoID { get; set; }
        [Display(Name = "Empleado")]
        public virtual Empleado Empleado { get; set; }
        [Display(Name = "Empleado")]
        public Nullable<int> Empleado_EmpleadoID { get; set; }
        [Display(Name = "Estado de queja")]
        public virtual Estado_QR Estado_QR { get; set; }
        [Display(Name = "Estado de queja")]
        public Nullable<int> Estado_QR_EstadoID { get; set; }

        [Display(Name = "Tipo de reclamación")]
        public virtual Tipo_Reclamacion Tipo_Reclamacion { get; set; }
        [Display(Name = "Tipo de reclamación")]
        public Nullable<int> Tipo_Reclamacion_TipoID { get; set; }
    }

    [DisplayColumn("TransaccionID")]
    [DisplayName("Transacción de productos")]
    public class Transaccion_ProductoMetadata{
        [Display(Name = "Transacción")]
        public int TransaccionID { get; set; }
        [Display(Name = "Producto")]
        public int ProductoID { get; set; }
        [Display(Name = "Cantidad de productos")]
        public Nullable<int> Cantidad_Producto { get; set; }
        [Display(Name = "Producto")]
        public virtual Producto Producto { get; set; }
        [Display(Name = "Transacción")]
        public virtual Transaccion Transaccion { get; set; }
    }

    [DisplayColumn("RespuestaID")]
    [DisplayName("Respuesta del cliente")]
    public class Respuesta_ClienteMetadata
    {
        [Display(Name = "Respuesta del cliente")]
        public int RespuestaID { get; set; }
        [Display(Name = "Valoración")]
        [Range(typeof(int),"1","5",ErrorMessage = "Valor incorrecto")]
        public Nullable<int> Valoracion { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        public Nullable<System.DateTime> Fecha { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentario")]
        public string Detalle { get; set; }

        [Display(Name = "Estado origen")]
        public virtual Estado_QR Estado_Origen { get; set; }
        [Display(Name = "Estado origen")]
        public Nullable<int> Estado_QR_Estado_OrigenID { get; set; }
        [Display(Name = "Estado destino")]
        public virtual Estado_QR Estado_Destino { get; set; }
        [Display(Name = "Estado destino")]
        public Nullable<int> Estado_QR_Estado_DestinoID { get; set; }
        [Display(Name = "Queja")]
        public virtual Queja Queja { get; set; }
        [Display(Name = "Queja")]
        public Nullable<int> Queja_QuejaID { get; set; }
        [Display(Name = "Reclamación")]
        public virtual Reclamacion Reclamacion { get; set; }
        [Display(Name = "Reclamación")]
        public Nullable<int> Reclamacion_ReclamacionID { get; set; }
    }

    [DisplayColumn("RespuestaID")]
    [DisplayName("Respuesta del empleado")]
    public class Respuesta_EmpleadoMetadata
    {
        [Display(Name = "Respuesta del empleado")]
        public int RespuestaID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        public Nullable<System.DateTime> Fecha { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentario")]
        public string Detalle { get; set; }

        [Display(Name = "Departamento origen")]
        public virtual Departamento Departamento_Origen { get; set; }
        [Display(Name = "Departamento origen")]
        public Nullable<int> Departamento_Departamento_OrigenID { get; set; }
        
        [Display(Name = "Departamento destino")]
        public virtual Departamento Departamento_Destino { get; set; }
        [Display(Name = "Departamento destino")]
        public Nullable<int> Departamento_Departamento_DestinoID { get; set; }
        
        [Display(Name = "Empleado origen")]
        public virtual Empleado Empleado_Origen { get; set; }
        [Display(Name = "Empleado origen")]
        public Nullable<int> Empleado_Empleado_OrigenID { get; set; }
        [Display(Name = "Empleado destino")]
        public virtual Empleado Empleado_Destino { get; set; }
        [Display(Name = "Empleado destino")]
        public Nullable<int> Empleado_Empleado_DestinoID { get; set; }
        [Display(Name = "Estado origen")]
        public virtual Estado_QR Estado_Origen { get; set; }
        [Display(Name = "Estado origen")]
        public Nullable<int> Estado_QR_Estado_OrigenID { get; set; }
        [Display(Name = "Estado destino")]
        public virtual Estado_QR Estado_Destino { get; set; }
        [Display(Name = "Estado destino")]
        public Nullable<int> Estado_QR_Estado_DestinoID { get; set; }
        [Display(Name = "Queja")]
        public virtual Queja Queja { get; set; }
        [Display(Name = "Queja")]
        public Nullable<int> Queja_QuejaID { get; set; }
        [Display(Name = "Reclamación")]
        public virtual Reclamacion Reclamacion { get; set; }
        [Display(Name = "Reclamación")]
        public Nullable<int> Reclamacion_ReclamacionID { get; set; }
        [Display(Name = "Sucursal origen")]
        public virtual Sucursal Sucursal_Origen { get; set; }
        [Display(Name = "Sucursal origen")]
        public Nullable<int> Sucursal_Sucursal_OrigenID { get; set; }
        [Display(Name = "Sucursal destino")]
        public virtual Sucursal Sucursal_Destino { get; set; }
        [Display(Name = "Sucursal destino")]
        public Nullable<int> Sucursal_Sucursal_DestinoID { get; set; }
    }

    [DisplayName("Sucursal")]
    public class SucursalMetadata
    {
        [Display(Name = "Sucursal")]
        public int SucursalID { get; set; }
        [Display(Name = "Sucursal")]
        public string Nombre { get; set; }
        [Display(Name = "Representante")]
        public virtual Empleado EmpleadoRepresentante { get; set; }
        [Display(Name = "Representante")]
        public Nullable<int> Empleado_PersonaID { get; set; }
    }

    [DisplayName("Tipo de identificación")]
    public class Tipo_IdentificacionMetadata
    {
        [Display(Name = "Tipo de identificación")]
        public int TipoID { get; set; }
        [Display(Name = "Tipo de identificación")]
        public string Descripcion { get; set; }
    }

    [DisplayName("Tipo de producto")]
    public class Tipo_ProductoMetadata
    {
        [Display(Name = "Tipo de producto")]
        public int TipoID { get; set; }
        [Display(Name = "Tipo de producto")]
        public string Descripcion { get; set; }
    }

    [DisplayName("Tipo de queja")]
    public class Tipo_QuejaMetadata
    {
        [Display(Name = "Tipo de queja")]
        public int TipoID { get; set; }
        [Display(Name = "Tipo de queja")]
        public string Descripcion { get; set; }
    }

    [DisplayName("Tipo de reclamación")]
    public class Tipo_ReclamacionMetadata
    {
        [Display(Name = "Tipo de reclamación")]
        public int TipoID { get; set; }
        [Display(Name = "Tipo de reclamación")]
        public string Descripcion { get; set; }
    }

    [DisplayColumn("TransaccionID")]
    [DisplayName("Transacción")]
    public class TransaccionMetadata
    {
        [Display(Name = "Transacción")]
        public int TransaccionID { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        public Nullable<System.DateTime> Fecha { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Monto")]
        public Nullable<decimal> Monto { get; set; }

        [Display(Name = "Cliente")]
        public virtual Cliente Cliente { get; set; }
        [Display(Name = "Cliente")]
        public Nullable<int> Cliente_PersonaID { get; set; }
        [Display(Name = "Vendedor")]
        public virtual Empleado Empleado { get; set; }
        [Display(Name = "Vendedor")]
        public Nullable<int> Empleado_PersonaID { get; set; }
        [Display(Name = "Estado de transacción")]
        public virtual Estado_Transaccion Estado_Transaccion { get; set; }
        [Display(Name = "Estado de transacción")]
        public Nullable<int> Estado_Transaccion_EstadoID { get; set; }
    }

}