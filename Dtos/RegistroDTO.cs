using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPedidos.Dtos
{
    public class RegistroDTO
    {
    public string UsuarioNombre { get; set; }
    public string CorreoElectronico { get; set; }
    public int DNI { get; set; }
    public string Rol { get; set; }
    public string Password { get; set; }
    }
}