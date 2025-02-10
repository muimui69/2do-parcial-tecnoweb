using Aforo255.Cross.Event.Src.Commands;

namespace MSVenta.Inventario.Messages.Commands
{
    public class AlmacenDeletedCommand : Command
    {
        public int Id { get; set; }

        public AlmacenDeletedCommand(int id)
        {
            Id = id;
        }
    }
}
