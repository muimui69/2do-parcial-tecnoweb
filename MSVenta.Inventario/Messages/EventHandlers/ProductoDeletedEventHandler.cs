using Aforo255.Cross.Event.Src.Bus;
using MSVenta.Inventario.Messages.Events;
using MSVenta.Inventario.Services;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Messages.EventHandlers
{
    public class ProductoDeletedEventHandler : IEventHandler<ProductoDeletedEvent>
    {
        private readonly IProductoService _productoService;

        public ProductoDeletedEventHandler(IProductoService productoService)
        {
            _productoService = productoService;
        }

        public async Task Handle(ProductoDeletedEvent @event)
        {
            // Lógica para manejar el evento ProductoDeletedEvent
            await _productoService.DeleteProductoAsync(@event.Id);
        }
    }
}
