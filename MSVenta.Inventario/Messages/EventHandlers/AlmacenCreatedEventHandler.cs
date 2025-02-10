using Aforo255.Cross.Event.Src.Bus;
using MSVenta.Inventario.Messages.Events;
using MSVenta.Inventario.Models;
using MSVenta.Inventario.Services;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Messages.EventHandlers
{
    public class AlmacenCreatedEventHandler : IEventHandler<AlmacenCreatedEvent>
    {
        private readonly IAlmacenService _almacenService;

        public AlmacenCreatedEventHandler(IAlmacenService almacenService)
        {
            _almacenService = almacenService;
        }

        public async Task Handle(AlmacenCreatedEvent @event)
        {
            await _almacenService.CreateAlmacenAsync(new Almacen
            {
                Id = @event.Id,
                Nombre = @event.Nombre,
                Ubicacion = @event.Ubicacion
            });
        }
    }
}
