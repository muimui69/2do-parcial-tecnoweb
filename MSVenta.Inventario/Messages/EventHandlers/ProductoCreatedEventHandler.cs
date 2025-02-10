using Aforo255.Cross.Event.Src.Bus;
using MSVenta.Inventario.Messages.Events;
using MSVenta.Inventario.Models;
using MSVenta.Inventario.Services;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Messages.EventHandlers
{
    public class ProductoCreatedEventHandler : IEventHandler<ProductoCreatedEvent>
    {
        private readonly IProductoService _productoService;

        public ProductoCreatedEventHandler(IProductoService productoService)
        {
            _productoService = productoService;
        }

        public async Task Handle(ProductoCreatedEvent @event)
        {
            // Lógica para manejar el evento ProductoCreatedEvent
            await _productoService.CreateProductoAsync(new Producto
            {
                Id = @event.Id,
                Nombre = @event.Nombre,
                Precio = @event.Precio,
                Descripcion = @event.Descripcion,
                IdCategoria = @event.IdCategoria
            });
        }
    }
}
