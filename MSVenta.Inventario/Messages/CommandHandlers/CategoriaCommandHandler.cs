using System.Threading;
using System.Threading.Tasks;
using Aforo255.Cross.Event.Src.Bus;
using MediatR;
using MSVenta.Inventario.Messages.Commands;
using MSVenta.Inventario.Messages.Events;

namespace MSVenta.Inventario.Messages.CommandHandlers
{
    public class CategoriaCommandHandler :
        IRequestHandler<CategoriaCreateCommand, bool>,
        IRequestHandler<CategoriaUpdatedCommand, bool>,
        IRequestHandler<CategoriaDeletedCommand, bool>
    {
        private readonly IEventBus _bus;

        public CategoriaCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }

        public Task<bool> Handle(CategoriaCreateCommand request, CancellationToken cancellationToken)
        {
            _bus.Publish(new CategoriaCreatedEvent(request.Id, request.Nombre));
            return Task.FromResult(true);
        }

        public Task<bool> Handle(CategoriaUpdatedCommand request, CancellationToken cancellationToken)
        {
            _bus.Publish(new CategoriaUpdatedEvent(request.Id, request.Nombre));
            return Task.FromResult(true);
        }

        public Task<bool> Handle(CategoriaDeletedCommand request, CancellationToken cancellationToken)
        {
            _bus.Publish(new CategoriaDeletedEvent(request.Id));
            return Task.FromResult(true);
        }
    }
}
