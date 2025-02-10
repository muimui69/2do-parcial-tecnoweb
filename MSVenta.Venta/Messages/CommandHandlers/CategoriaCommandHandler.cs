using System.Threading;
using System.Threading.Tasks;
using Aforo255.Cross.Event.Src.Bus;
using MediatR;
using MSVenta.Venta.Messages.Commands;
using MSVenta.Venta.Messages.Events;

namespace MSVenta.Venta.Messages.CommandHandlers
{
    public class CategoriaCommandHandler : IRequestHandler<CategoriaCreateCommand, bool>
    {
        private readonly IEventBus _bus;

        public CategoriaCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }

        public Task<bool> Handle(CategoriaCreateCommand request, CancellationToken cancellationToken)
        {
            _bus.Publish(new CategoriaCreatedEvent(
                request.Id,
                request.Nombre
            ));

            return Task.FromResult(true);
        }
    }
}
