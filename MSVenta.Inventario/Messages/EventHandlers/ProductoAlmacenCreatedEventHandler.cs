using Aforo255.Cross.Event.Src.Bus;
using MSVenta.Inventario.Messages.Events;
using MSVenta.Inventario.Models;
using MSVenta.Inventario.Services;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Messages.EventHandlers
{
    public class ProductoAlmacenCreatedEventHandler : IEventHandler<ProductoAlmacenCreatedEvent>
    {
        private readonly IProductoAlmacenService _productoAlmacenService;

        public ProductoAlmacenCreatedEventHandler(IProductoAlmacenService productoAlmacenService)
        {
            _productoAlmacenService = productoAlmacenService;
        }

        public async Task Handle(ProductoAlmacenCreatedEvent @event)
        {
            // Lógica para manejar el evento ProductoAlmacenCreatedEvent
            await _productoAlmacenService.AddAsync(new ProductoAlmacen
            {
                Id = @event.Id,
                ProductoId = @event.ProductoId,
                AlmacenId = @event.AlmacenId,
                Stock = @event.Stock
            });
        }
    }
}
