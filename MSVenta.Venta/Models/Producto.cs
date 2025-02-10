using System.ComponentModel.DataAnnotations.Schema;

namespace MSVenta.Venta.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Descripcion { get; set; }

        //[Column("id_categoria")]
        public int IdCategoria { get; set; }  // Relación con la categoría
        public Categoria Categoria { get; set; }  // Relación con el objeto Categoria
    }
}
