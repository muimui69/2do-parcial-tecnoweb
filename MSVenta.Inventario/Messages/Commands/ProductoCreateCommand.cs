using Aforo255.Cross.Event.Src.Commands;

namespace MSVenta.Inventario.Messages.Commands
{
    public class ProductoCreateCommand : Command
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int? IdCategoria { get; set; }

        public ProductoCreateCommand(int id, string nombre,string descripcion,decimal precio,int? id_categoria)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            IdCategoria = id_categoria;
        }

    }
}
