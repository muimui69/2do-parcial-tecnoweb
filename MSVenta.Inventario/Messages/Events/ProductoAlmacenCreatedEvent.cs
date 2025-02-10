using Aforo255.Cross.Event.Src.Events;

namespace MSVenta.Inventario.Messages.Events
{
    public class ProductoAlmacenCreatedEvent : Event
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public int AlmacenId { get; set; }
        public int Stock { get; set; }

        public ProductoAlmacenCreatedEvent(int id, int productoId, int almacenId,int stock)
        {
            Id = id;
            ProductoId = productoId;
            AlmacenId = almacenId;
            Stock = stock;
        }
    }
}
