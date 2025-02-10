//using System.Threading.Tasks;
//using Aforo255.Cross.Event.Src.Bus;
//using MSVenta.Inventario.Messages.Events;
//using MSVenta.Inventario.Services;
//using MSVenta.Venta.Messages.Events;

//namespace MSVenta.Inventario.Messages.EventHandlers
//{
//    public class VentaCreatedEventHandler : IEventHandler<VentaCreatedEvent>
//    {
//        private readonly IProductoAlmacenServic _productoAlmacenService;

//        public VentaCreatedEventHandler(IProductoAlmacenService productoAlmacenService)
//        {
//            _productoAlmacenService = productoAlmacenService;
//        }

//        public async Task Handle(VentaCreatedEvent @event)
//        {
//            // Ajustar stock del producto
//            await _productoAlmacenService.DescontarStockAsync(@event.ProductoId, @event.Cantidad);
//        }
//    }
//}
