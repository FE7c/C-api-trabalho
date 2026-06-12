using LojaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Vendedor> Vendedores { get; set; }
    public DbSet<Cliente> Clientes { get; set; } // ← adicionado
}