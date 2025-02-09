namespace MSVenta.Inventario.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public double Precio { get; set; }
        public int Id_Categoria { get; set; }
        public Categoria Categoria { get; set; }
    }
}