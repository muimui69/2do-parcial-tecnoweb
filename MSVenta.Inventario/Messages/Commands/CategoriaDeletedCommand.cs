using Aforo255.Cross.Event.Src.Commands;

namespace MSVenta.Inventario.Messages.Commands
{
    public class CategoriaDeletedCommand : Command
    {
        public int Id { get; set; }

        public CategoriaDeletedCommand(int id)
        {
            Id = id;
        }
    }
}
