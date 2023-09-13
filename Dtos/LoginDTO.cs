using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPedidos.Dtos
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El campo UsuarioNombre es requerido.")]
        public string UsuarioNombre { get; set; }

        [Required(ErrorMessage = "El campo Password es requerido.")]
        public string Password { get; set; }
    }
}