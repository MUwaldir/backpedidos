using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPedidos.Data;
using ApiPedidos.Dtos;
using ApiPedidos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;

namespace ApiPedidos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly DataContext _context;

        public PedidoController(ILogger<PedidoController> logger, DataContext context)
        {
            _logger=logger;
            _context=context;
        }

        [HttpPost]
     
    public async Task<ActionResult<Pedido>> PostPedido(PedidoDTO pedidoDTO)
    {
    // Validar el modelo antes de continuar
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }
    // Verificar si el Id del usuario existe en la tabla Usuarios
        var usuario = await _context.Usuarios.FindAsync(pedidoDTO.UsuarioId); // Cambia UsuarioId por Id
        if (usuario == null)
        {
            // El Id del usuario no existe, devolver un BadRequest u otro código de estado apropiado
            return BadRequest("El Id del usuario no es válido.");
        }

    // Crear una instancia de Pedido y asignar los datos del DTO
    var nuevoPedido = new Pedido
    {
        UsuarioId = pedidoDTO.UsuarioId,
        FechaPedido = DateTime.Now.ToUniversalTime(), // Puedes ajustar la fecha según tus necesidades
        Estado = "Pendiente" // Otra opción es establecer un estado predeterminado
    };

    // Agregar el pedido a la base de datos
    _context.Pedidos.Add(nuevoPedido);
    await _context.SaveChangesAsync();

    // Agregar detalles del pedido a la tabla DetallePedido
    foreach (var detalleDTO in pedidoDTO.DetallesPedido)
    {
        var producto = await _context.Productos.FindAsync(detalleDTO.ProductoId);
        if (producto != null)
        {
            var nuevoDetalle = new DetallePedido
            {
                PedidoId = nuevoPedido.Id,
                ProductoId = detalleDTO.ProductoId,
                PrecioUnitario = producto.precio
            };

            // Agregar el detalle a la base de datos
            _context.DetallePedido.Add(nuevoDetalle);
        }
    }

    await _context.SaveChangesAsync();

    return CreatedAtAction("GetPedido", new { id = nuevoPedido.Id }, nuevoPedido);
}

        [HttpGet]
       
        public async Task<ActionResult<IEnumerable<Pedido>>> ObtenerPedidos()
        {
            
            var pedidos = await _context.Pedidos
                .Include(p => p.DetallePedido) // Incluir los detalles del pedido
                .ThenInclude(dp => dp.Producto)
                .ToListAsync();

            return Ok(pedidos);
        }

        [HttpGet("{id}")]
       
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.DetallePedido)
                .ThenInclude(dp => dp.Producto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound(); // Devolver 404 si el pedido no se encuentra
            }

            return Ok(pedido);
        }

        [HttpGet("por-usuario/{usuarioId}")]
       
        public async Task<ActionResult<IEnumerable<Pedido>>> ObtenerPedidosPorUsuario(string usuarioId)
        {
            // Filtrar pedidos por UsuarioId
            var pedidosPorUsuario = await _context.Pedidos
                .Where(p => p.UsuarioId == usuarioId)
                .Include(p => p.DetallePedido)
                .ThenInclude(dp => dp.Producto)
                .ToListAsync();

            // Verificar si se encontraron pedidos
            if (pedidosPorUsuario.Count == 0)
            {
                return NotFound("No se encontraron pedidos para el usuario.");
            }

            return Ok(pedidosPorUsuario);
        }





    }
}