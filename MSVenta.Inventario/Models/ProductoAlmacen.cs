using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSVenta.Inventario.Models
{
    [Table("producto_almacen")]
    public class ProductoAlmacen
    {
        [Key]
        [Column("id_producto_almacen")]
        public int Id { get; set; }

        [Column("id_producto")]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; } = null!;

        [Column("id_almacen")]
        public int AlmacenId { get; set; }

        [ForeignKey("AlmacenId")]
        public Almacen Almacen { get; set; } = null!;

        [Column("stock")]
        public int Stock { get; set; }

        public ICollection<DetalleAjuste> DetallesAjuste { get; set; } = new List<DetalleAjuste>();

    }
}