using Aforo255.Cross.Event.Src.Commands;

namespace MSVenta.Inventario.Messages.Commands
{
    public class ProductoAlmacenDeletedCommand : Command
    {
        public int Id { get; set; }

        public ProductoAlmacenDeletedCommand(int id)
        {
            Id = id;
        }
    }
}
