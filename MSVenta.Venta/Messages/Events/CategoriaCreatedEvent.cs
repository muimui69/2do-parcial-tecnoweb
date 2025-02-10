using Aforo255.Cross.Event.Src.Events;

namespace MSVenta.Venta.Messages.Events
{
    public class CategoriaCreatedEvent : Event
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public CategoriaCreatedEvent(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
