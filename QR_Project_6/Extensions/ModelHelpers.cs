using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using QR_Project_6.Models;
using QR_Project_6.Models.Model;

namespace QR_Project_6.Extensions
{
    public static class ModelHelpers
    {
        public static int CompareRespuestas(Respuesta x, Respuesta y)
        {
            if (x.Fecha == null && y.Fecha == null)
            {
                return 0;
            }
            if (x.Fecha == null && y.Fecha != null)
            {
                return -1;
            }
            if (x.Fecha != null && y.Fecha == null)
            {
                return 1;
            }
            if (x.Fecha > y.Fecha)
            {
                return 1;
            }
            if (x.Fecha < y.Fecha)
            {
                return -1;
            }

            return 0;
        }

        public static string getNombreCompletoUsuario(IIdentity user)
        {
            string nombre = "";
            AbstractQR_Model db = new QR_Model();
            string userName = user.GetUserId();
            List<Empleado> empleados = db.Empleados.Where(e => e.UserNameID == userName).ToList<Empleado>();
            if(empleados.Count() > 0)
            {
                Empleado empleado = empleados.First<Empleado>();
                nombre += empleado.Nombre + " " + empleado.Apellido;
                return nombre;
            }
            else
            {
                List<Cliente> clientes = db.Clientes.Where(e => e.UserNameID == userName).ToList<Cliente>();
                if(clientes.Count() > 0)
                {
                    Cliente cliente = clientes.First<Cliente>();
                    nombre += cliente.Nombre + " " + cliente.Apellido;
                    return nombre;
                }

                
            }
            nombre = user.GetUserName();

            return nombre;
        }
    }
}