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

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.Property(c => c.Id).HasColumnName("id");
                entity.Property(c => c.Nombre).HasColumnName("nombre");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(p => p.Id).HasColumnName("id_producto");
                entity.Property(p => p.Nombre).HasColumnName("nombre");
                entity.Property(p => p.Descripcion).HasColumnName("descripcion");
                entity.Property(p => p.Precio).HasColumnName("precio");
                entity.Property(p => p.IdCategoria).HasColumnName("id_categoria");

                entity.HasOne(p => p.Categoria)
                      .WithMany()
                      .HasForeignKey(p => p.IdCategoria)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Almacen>(entity =>
            {
                entity.Property(a => a.Id).HasColumnName("id");
                entity.Property(a => a.Nombre).HasColumnName("nombre");
                entity.Property(a => a.Ubicacion).HasColumnName("ubicacion");
            });

            modelBuilder.Entity<ProductoAlmacen>(entity =>
            {
                entity.Property(pa => pa.Id).HasColumnName("id_producto_almacen");
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

            modelBuilder.Entity<AjusteInventario>(entity =>
            {
                entity.Property(ai => ai.Id).HasColumnName("id_ajuste_inventario");
                entity.Property(ai => ai.IdUsuario).HasColumnName("id_usuario");
                entity.Property(ai => ai.Fecha).HasColumnName("fecha");
                entity.Property(ai => ai.Tipo).HasColumnName("tipo");
                entity.Property(ai => ai.Descripcion).HasColumnName("descripcion");
            });

            modelBuilder.Entity<DetalleAjuste>(entity =>
            {
                entity.Property(da => da.Id).HasColumnName("id_detalle_ajuste");
                entity.Property(da => da.IdAjusteInventario).HasColumnName("id_ajuste_inventario");
                entity.Property(da => da.IdProductoAlmacen).HasColumnName("id_producto_almacen");
                entity.Property(da => da.Cantidad).HasColumnName("cantidad");

                // Relaciones
                entity.HasOne(da => da.AjusteInventario)
                      .WithMany(ai => ai.DetallesAjuste)
                      .HasForeignKey(da => da.IdAjusteInventario)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(da => da.ProductoAlmacen)
                      .WithMany(da =>da.DetallesAjuste)
                      .HasForeignKey(da => da.IdProductoAlmacen)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
