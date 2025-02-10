using Aforo255.Cross.Event.Src.Bus;
using MSVenta.Inventario.Messages.Events;
using MSVenta.Inventario.Models;
using MSVenta.Inventario.Services;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Messages.EventHandlers
{
    public class ProductoUpdatedEventHandler : IEventHandler<ProductoUpdatedEvent>
    {
        private readonly IProductoService _productoService;

        public ProductoUpdatedEventHandler(IProductoService productoService)
        {
            _productoService = productoService;
        }

        public async Task Handle(ProductoUpdatedEvent @event)
        {
            // Lógica para manejar el evento ProductoUpdatedEvent
            await _productoService.UpdateProductoAsync(new Producto
            {
                Id = @event.Id,
                Nombre = @event.Nombre,
                Precio = @event.Precio
            });
        }
    }
}
