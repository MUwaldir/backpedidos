using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPedidos.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public decimal precio { get; set; }

         // Propiedad de navegación para los detalles de la pedido
        public ICollection<DetallePedido>? DetallePedido { get; set; }
    }
}