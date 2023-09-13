using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPedidos.Models
{
    public class DetallePedido
    {
         public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }
        public decimal PrecioUnitario { get; set; }

         // Propiedad de navegación para la Pedido
        public Pedido Pedido { get; set; }
        // Propiedad de navegación para el producto
        public Producto Producto { get; set; }
    }
}