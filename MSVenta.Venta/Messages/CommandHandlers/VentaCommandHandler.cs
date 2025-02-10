////using System.Threading;
////using System.Threading.Tasks;
////using Aforo255.Cross.Event.Src.Bus;
////using MediatR;
////using MSVenta.Venta.Messages.Commands;
////using MSVenta.Venta.Messages.Events;

////namespace MSVenta.Venta.Messages.CommandHandlers
////{
////    public class VentaCommandHandler : IRequestHandler<VentaCreateCommand, bool>
////    {
////        private readonly IEventBus _bus;

//        public VentaCommandHandler(IEventBus bus)
//        {
//            _bus = bus;
//        }

//        public Task<bool> Handle(VentaCreateCommand request, CancellationToken cancellationToken)
//        {
//            _bus.Publish(new VentaCreatedEvent(
//                request.VentaId,
//                request.ProductoId,
//                request.Cantidad
//            ));
//            return Task.FromResult(true);
//        }
//    }
//}
