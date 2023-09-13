using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ApiPedidos.Models
{
    public class Usuario:IdentityUser
    {
    
        public string UsuarioNombre { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string CorreoElectronico  { get; set; } = string.Empty;

        public int DNI { get; set; }
        public string Rol { get; set; } = string.Empty;
         // Propiedad de navegación para las órdenes del usuario
        
        public ICollection<Pedido> Pedidos { get; set; }
     
        
    }
}