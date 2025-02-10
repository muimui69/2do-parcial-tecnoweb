using System.Collections.Generic;

namespace MSVenta.Inventario.DTOs
{
    public class AlmacenConProductosDto
    {
        public int AlmacenId { get; set; }
        public string AlmacenNombre { get; set; }
        public List<ProductoAllDto> Productos { get; set; }
    }

    public class ProductoAllDto
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
        public int Stock { get; set; }
    }
}