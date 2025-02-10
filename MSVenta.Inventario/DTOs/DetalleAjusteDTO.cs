namespace MSVenta.Inventario.DTOs
{
    public class DetalleAjusteDTO
    {
          public int Id { get; set; }
            public int IdAjusteInventario { get; set; }
            public AjusteInventarioDetalleDTO AjusteInventario { get; set; }
            public int IdProductoAlmacen { get; set; }
            public ProductoAlmacenDetalleDTO ProductoAlmacen { get; set; }
            public int Cantidad { get; set; }
    }

    public class AjusteInventarioDetalleDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }

    public class ProductoAlmacenDetalleDTO
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public ProductoDTO Producto { get; set; }
        public AlmacenDTO Almacen { get; set; }
    }

    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class AlmacenDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
