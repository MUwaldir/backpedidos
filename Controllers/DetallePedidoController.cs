using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPedidos.Data;
using ApiPedidos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPedidos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetallePedidoController : ControllerBase
    {
        
        private readonly ILogger<DetallePedidoController> _logger;
        private readonly DataContext _context;

        public DetallePedidoController(ILogger<DetallePedidoController> logger, DataContext context)
        {
            _logger=logger;
            _context=context;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<DetallePedido>> ObtenerDetallePedido(int id)
    {
    var detallePedido = await _context.DetallePedido
        .Include(dp => dp.Pedido) // Incluir la relación con Pedido
        .Include(dp => dp.Producto) // Incluir la relación con Producto
        .FirstOrDefaultAsync(dp => dp.Id == id);

    if (detallePedido == null)
    {
        return NotFound();
    }

    return Ok(detallePedido);
}


       
    }
}