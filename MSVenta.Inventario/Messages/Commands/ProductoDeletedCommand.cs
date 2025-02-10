using Aforo255.Cross.Event.Src.Commands;

namespace MSVenta.Inventario.Messages.Commands
{
    public class ProductoDeletedCommand : Command
    {
        public int Id { get; set; }

        public ProductoDeletedCommand(int id)
        {
            Id = id;
        }
    }
}
