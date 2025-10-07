using Cp2_CsharpModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Cp2_CsharpData
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
    }
}