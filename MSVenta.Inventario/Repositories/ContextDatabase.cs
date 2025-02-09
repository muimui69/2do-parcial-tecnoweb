using Microsoft.EntityFrameworkCore;
using MSVenta.Inventario.Models;

namespace MSVenta.Inventario.Repositories
{
    public class ContextDatabase : DbContext
    {
        public ContextDatabase(DbContextOptions<ContextDatabase> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Almacen> Almacenes { get; set; }
        public DbSet<ProductoAlmacen> ProductosAlmacenes { get; set; }
        public DbSet<AjusteInventario> AjustesInventario { get; set; }
        public DbSet<DetalleAjuste> DetallesAjuste { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().ToTable("categoria");
            modelBuilder.Entity<Producto>().ToTable("producto");
            modelBuilder.Entity<Almacen>().ToTable("almacen");
            modelBuilder.Entity<ProductoAlmacen>().ToTable("producto_almacen");
            modelBuilder.Entity<AjusteInventario>().ToTable("ajuste_inventario");
            modelBuilder.Entity<DetalleAjuste>().ToTable("detalle_ajuste");

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(p => p.Id_Categoria).HasColumnName("id_categoria");
                entity.HasOne(p => p.Categoria)
                      .WithMany()
                      .HasForeignKey(p => p.Id_Categoria)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<ProductoAlmacen>(entity =>
            {
                entity.Property(pa => pa.ProductoId).HasColumnName("id_producto");
                entity.Property(pa => pa.AlmacenId).HasColumnName("id_almacen");
                entity.Property(pa => pa.Stock).HasColumnName("stock");

                entity.HasOne(pa => pa.Producto)
                      .WithMany()
                      .HasForeignKey(pa => pa.ProductoId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pa => pa.Almacen)
                      .WithMany()
                      .HasForeignKey(pa => pa.AlmacenId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DetalleAjuste>(entity =>
            {
                entity.Property(da => da.AjusteInventarioId).HasColumnName("id_ajuste");
                entity.Property(da => da.ProductoId).HasColumnName("id_producto");
                entity.Property(da => da.Cantidad).HasColumnName("cantidad");

                entity.HasOne(da => da.AjusteInventario)
                      .WithMany()
                      .HasForeignKey(da => da.AjusteInventarioId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(da => da.Producto)
                      .WithMany()
                      .HasForeignKey(da => da.ProductoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
