using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPedidos.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime FechaPedido { get; set; }
        public string UsuarioId { get; set; }
        public string Estado { get; set; } = string.Empty;

        // Propiedad de navegación para el usuario
        public Usuario Usuario { get; set; }

        // Propiedad de navegación para los detalles de la pediDetallePedido
        public ICollection<DetallePedido> DetallePedido { get; set; }

    }
}