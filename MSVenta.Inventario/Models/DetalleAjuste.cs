using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSVenta.Inventario.Models
{
    [Table("detalle_ajuste")]
    public class DetalleAjuste
    {
        [Key]
        [Column("id_detalle_ajuste")]
        public int Id { get; set; }

        [Column("id_ajuste_inventario")]
        public int IdAjusteInventario { get; set; }
        public AjusteInventario AjusteInventario { get; set; } = null!;

        [Column("id_producto_almacen")]
        public int IdProductoAlmacen { get; set; }
        public ProductoAlmacen ProductoAlmacen { get; set; } = null!;

        [Column("cantidad")]
        public int Cantidad { get; set; }
    }
}
