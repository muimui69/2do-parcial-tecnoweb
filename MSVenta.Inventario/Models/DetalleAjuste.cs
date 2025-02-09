namespace MSVenta.Inventario.Models
{
    public class DetalleAjuste
    {
        public int Id { get; set; }
        public int AjusteInventarioId { get; set; }
        public AjusteInventario AjusteInventario { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
    }
}