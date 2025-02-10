using Aforo255.Cross.Event.Src.Commands;

namespace MSVenta.Venta.Messages.Commands
{
    public class CategoriaCreateCommand : Command
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public CategoriaCreateCommand(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
