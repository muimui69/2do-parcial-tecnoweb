using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSVenta.Inventario.Models
{
    [Table("almacen")]
    public class Almacen
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; } = null!;

        [Column("ubicacion")]
        public string Ubicacion { get; set; } = null!;
    }
}