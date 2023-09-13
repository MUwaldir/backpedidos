using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPedidos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ApiPedidos.Data
{
    public class DataContext: DbContext
    {
         public DataContext(DbContextOptions<DataContext> options) :  base(options)
        {
            
        }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallePedido { get; set; }
    }
}