using Aforo255.Cross.Event.Src.Events;

namespace MSVenta.Inventario.Messages.Events
{
    public class CategoriaDeletedEvent : Event
    {
        public int Id { get; set; }

        public CategoriaDeletedEvent(int id)
        {
            Id = id;
        }
    }
}
