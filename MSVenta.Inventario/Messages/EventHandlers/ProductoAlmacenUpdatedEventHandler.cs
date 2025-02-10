using Aforo255.Cross.Event.Src.Bus;
using MSVenta.Inventario.Messages.Events;
using MSVenta.Inventario.Models;
using MSVenta.Inventario.Services;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Messages.EventHandlers
{
    public class ProductoAlmacenUpdatedEventHandler : IEventHandler<ProductoAlmacenUpdatedEvent>
    {
        private readonly IProductoAlmacenService _productoAlmacenService;

        public ProductoAlmacenUpdatedEventHandler(IProductoAlmacenService productoAlmacenService)
        {
            _productoAlmacenService = productoAlmacenService;
        }

        public async Task Handle(ProductoAlmacenUpdatedEvent @event)
        {
            // Lógica para manejar el evento ProductoAlmacenUpdatedEvent
            await _productoAlmacenService.UpdateAsync(new ProductoAlmacen
            {
                Id = @event.Id,
                ProductoId = @event.ProductoId,
                AlmacenId = @event.AlmacenId,
                Stock = @event.Stock
            });
        }
    }
}
