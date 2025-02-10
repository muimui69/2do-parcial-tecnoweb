using Aforo255.Cross.Event.Src.Commands;

namespace MSVenta.Inventario.Messages.Commands
{
    public class AlmacenCreateCommand : Command
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }

        public AlmacenCreateCommand(int id, string nombre, string ubicacion)
        {
            Id = id;
            Nombre = nombre;
            Ubicacion = ubicacion;
        }
    }
}
