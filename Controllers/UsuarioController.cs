using System;
using System.Collections.Generic;
using System.Text; 
using System.Linq;
using System.Threading.Tasks;
using ApiPedidos.Data;
using ApiPedidos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ApiPedidos.Dtos;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;





namespace ApiPedidos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IConfiguration _configuration;
        private readonly string secretkey;

        public object JsonConvert { get; private set; }

        public UsuarioController(UserManager<Usuario> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            secretkey = configuration.GetSection("Jwt").GetSection("Key").ToString();
             }

    

        [HttpPost("registro")]
        
        public async Task<ActionResult> RegistrarUsuario(RegistroDTO registroDTO)
        {
            
            try
            {
                // Validar si el usuario ya existe en la base de datos por su nombre de usuario o correo electrónico
                var usuarioExistente = await _userManager.FindByNameAsync(registroDTO.UsuarioNombre);
                if (usuarioExistente != null)
                {
                    return BadRequest("El usuario ya existe en la base de datos.");
                }
                Console.WriteLine("SSSs");
                var nuevoUsuario = new Usuario
                {
                    UserName= registroDTO.UsuarioNombre,
                    UsuarioNombre= registroDTO.UsuarioNombre,
                    Email = registroDTO.CorreoElectronico,
                    DNI = registroDTO.DNI,
                    Rol = registroDTO.Rol,
                    // Otras propiedades personalizadas del usuario
                };
                Console.WriteLine(nuevoUsuario.DNI);
              

                // Crear el usuario en ASP.NET Identity
                var resultadoCreacionUsuario = await _userManager.CreateAsync(nuevoUsuario, registroDTO.Password);
                

                if (resultadoCreacionUsuario.Succeeded)
                {
                    return Ok("Usuario registrado correctamente.");
                }
                else
                {
                    return BadRequest("No se pudo crear el usuario.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

       

        [HttpPost("Login")]
       
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            Console.WriteLine(loginDTO);
            try
            {
                var usuario = await _userManager.FindByNameAsync(loginDTO.UsuarioNombre);
               
                
                if (usuario != null && await _userManager.CheckPasswordAsync(usuario, loginDTO.Password))
                {
                    // Generar el token JWT
                    
                   Console.WriteLine("gigi 111");
                    var token = GenerarTokenJWT(usuario.ToString());
                    Console.WriteLine("gigi");
                    Console.WriteLine(token);
                    return Ok(new { Token = token, Message = "Inicio de sesión exitoso." ,usuarioId =usuario.Id });
                }
                else
                {
                    return BadRequest("Credenciales inválidas.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // private object GenerarTokenJWT(Usuario usuario)
        // {
        //     throw new NotImplementedException();
        // }

        private string GenerarTokenJWT(string usuario)
        {
                 var keyBytes = Encoding.ASCII.GetBytes(secretkey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario));
                

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokencreado = tokenHandler.WriteToken(tokenConfig);

                return tokencreado;
        }
        
    }


}
