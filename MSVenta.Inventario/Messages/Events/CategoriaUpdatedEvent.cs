using Aforo255.Cross.Event.Src.Events;

namespace MSVenta.Inventario.Messages.Events
{
    public class CategoriaUpdatedEvent : Event
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public CategoriaUpdatedEvent(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
