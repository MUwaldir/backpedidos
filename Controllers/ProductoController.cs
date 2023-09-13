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
    public class ProductoController : ControllerBase
    {
        private readonly ILogger<ProductoController> _logger;
        private readonly DataContext _context;

        public ProductoController(ILogger<ProductoController> logger, DataContext context)
        {
            _logger=logger;
            _context=context;
        }
        
        [HttpGet(Name ="GetProductos")]

        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        
        [HttpGet("{id}",Name ="GetProducto")]

        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if(producto == null)
            {
                return NotFound("Producto no encontrado");
            }
            return producto;
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> Post(Producto producto)
        {
            _context.Add(producto);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetProducto", new {id=producto.Id}, producto);
        }

        [HttpPut("{id}")]
            
        public async Task<ActionResult> Put(int id, Producto producto)
        {
            if(id  != producto.Id)
            {
                return BadRequest("El Id en la URL no coincide con el Id del producto.");
            }
            try
    {
           
                // Obtén el producto existente del contexto
                var productoExistente = await _context.Productos.FindAsync(id);

                if (productoExistente == null)
                {
                    return NotFound("Producto no encontrado.");
                }

                // Modifica las propiedades del producto existente
                productoExistente.NombreProducto = producto.NombreProducto;
                productoExistente.precio = producto.precio;

                // Actualiza el contexto
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Manejar excepción de concurrencia si es necesario
                return NotFound(); // Otra respuesta apropiada según tus requisitos
            }
                    
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Producto>> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if(producto == null)
            {
                return NotFound();
            }
            _context.Remove(producto);
            await _context.SaveChangesAsync();

            return producto;
        }
    }
}