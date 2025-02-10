using Aforo255.Cross.Event.Src.Bus;
using MSVenta.Inventario.Messages.Events;
using MSVenta.Inventario.Services;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Messages.EventHandlers
{
    public class AlmacenDeletedEventHandler : IEventHandler<AlmacenDeletedEvent>
    {
        private readonly IAlmacenService _almacenService;

        public AlmacenDeletedEventHandler(IAlmacenService almacenService)
        {
            _almacenService = almacenService;
        }

        public async Task Handle(AlmacenDeletedEvent @event)
        {
            await _almacenService.DeleteAlmacenAsync(@event.Id);
        }
    }
}
