using Aforo255.Cross.Event.Src.Events;

namespace MSVenta.Inventario.Messages.Events
{
    public class AlmacenDeletedEvent : Event
    {
        public int Id { get; set; }

        public AlmacenDeletedEvent(int id)
        {
            Id = id;
        }
    }
}
