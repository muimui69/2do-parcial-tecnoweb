using Aforo255.Cross.Event.Src.Commands;

namespace MSVenta.Inventario.Messages.Commands
{
    public class ProductoCreateCommand : Command
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Id_Categoria { get; set; }

        public ProductoCreateCommand(int id, string nombre,string descripcion,decimal precio,int idCategoria)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Id_Categoria = Id_Categoria;
        }

    }
}
