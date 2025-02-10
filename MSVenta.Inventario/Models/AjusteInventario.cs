using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSVenta.Inventario.Models
{
    [Table("ajuste_inventario")]
    public class AjusteInventario
    {
        [Key]
        [Column("id_ajuste_inventario")]
        public int Id { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Column("tipo")]
        public string Tipo { get; set; } = null!;

        [Column("descripcion")]
        public string? Descripcion { get; set; }

        // Relación uno a muchos con DetalleAjuste
        public ICollection<DetalleAjuste> DetallesAjuste { get; set; } = new List<DetalleAjuste>();
    }
}
