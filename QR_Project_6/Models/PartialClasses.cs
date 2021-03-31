using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QR_Project_6.Models
{
    [MetadataType(typeof(ClienteMetadata))]
    public partial class Cliente
    {

    }

    [MetadataType(typeof(DepartamentoMetadata))]
    public partial class Departamento
    {

    }
    [MetadataType(typeof(DireccionMetadata))]
    public partial class Direccion
    {

    }

    [MetadataType(typeof(EmpleadoMetadata))]
    public partial class Empleado
    {
        
    }

    

    [MetadataType(typeof(Estado_ClienteMetadata))]
    public partial class Estado_Cliente
    {

    }

    [MetadataType(typeof(Estado_QRMetadata))]
    public partial class Estado_QR
    {
        
    }

    [MetadataType(typeof(Estado_TransaccionMetadata))]
    public partial class Estado_Transaccion
    {

    }

    [MetadataType(typeof(PaisMetadata))]
    public partial class Pais
    {

    }

    [MetadataType(typeof(ProductoMetadata))]
    public partial class Producto
    {

    }

    [MetadataType(typeof(QuejaMetadata))]
    public partial class Queja
    {

    }

    [MetadataType(typeof(ReclamacionMetadata))]
    public partial class Reclamacion
    {

    }

    [MetadataType(typeof(Transaccion_ProductoMetadata))]
    public partial class Transaccion_Producto
    {

    }

    [MetadataType(typeof(Respuesta_ClienteMetadata))]
    public partial class Respuesta_Cliente
    {

    }

    [MetadataType(typeof(Respuesta_EmpleadoMetadata))]
    public partial class Respuesta_Empleado
    {

    }

    [MetadataType(typeof(SucursalMetadata))]
    public partial class Sucursal
    {

    }

    [MetadataType(typeof(Tipo_IdentificacionMetadata))]
    public partial class Tipo_Identificacion
    {

    }

    [MetadataType(typeof(Tipo_ProductoMetadata))]
    public partial class Tipo_Producto
    {

    }

    [MetadataType(typeof(Tipo_QuejaMetadata))]
    public partial class Tipo_Queja
    {

    }

    [MetadataType(typeof(Tipo_ReclamacionMetadata))]
    public partial class Tipo_Reclamacion
    {

    }

    [MetadataType(typeof(TransaccionMetadata))]
    public partial class Transaccion
    {

    }

}