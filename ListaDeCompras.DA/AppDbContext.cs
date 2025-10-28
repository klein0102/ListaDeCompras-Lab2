using ListaDeCompras.BC.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ListaDeCompras.DA
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<ListaCompra> ListasCompra { get; set; }
        public DbSet<ItemLista> ItemLista { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListaCompra>().ToTable("ListaCompra");
            modelBuilder.Entity<ItemLista>().ToTable("ItemLista");

            base.OnModelCreating(modelBuilder);
        }

    }
}
