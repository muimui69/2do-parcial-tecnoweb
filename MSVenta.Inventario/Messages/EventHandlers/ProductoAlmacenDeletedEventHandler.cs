using Aforo255.Cross.Event.Src.Bus;
using MSVenta.Inventario.Messages.Events;
using MSVenta.Inventario.Services;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Messages.EventHandlers
{
    public class ProductoAlmacenDeletedEventHandler : IEventHandler<ProductoAlmacenDeletedEvent>
    {
        private readonly IProductoAlmacenService _productoAlmacenService;

        public ProductoAlmacenDeletedEventHandler(IProductoAlmacenService productoAlmacenService)
        {
            _productoAlmacenService = productoAlmacenService;
        }

        public async Task Handle(ProductoAlmacenDeletedEvent @event)
        {
            // Lógica para manejar el evento ProductoAlmacenDeletedEvent
            await _productoAlmacenService.DeleteAsync(@event.Id);
        }
    }
}
