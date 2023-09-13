using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPedidos.Dtos
{
    public class PedidoDTO
    {
        public string UsuarioId { get; set; }
        public List<DetalleCreatePedidoDTO> DetallesPedido { get; set; }
    }
}