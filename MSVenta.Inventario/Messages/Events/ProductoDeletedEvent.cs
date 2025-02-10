using Aforo255.Cross.Event.Src.Events;

namespace MSVenta.Inventario.Messages.Events
{
    public class ProductoDeletedEvent : Event
    {
        public int Id { get; set; }

        public ProductoDeletedEvent(int id)
        {
            Id = id;
        }
    }
}
