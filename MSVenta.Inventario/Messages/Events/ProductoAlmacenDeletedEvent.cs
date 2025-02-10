using Aforo255.Cross.Event.Src.Events;

namespace MSVenta.Inventario.Messages.Events
{
    public class ProductoAlmacenDeletedEvent : Event
    {
        public int Id { get; set; }

        public ProductoAlmacenDeletedEvent(int id)
        {
            Id = id;
        }
    }
}
