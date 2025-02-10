using Aforo255.Cross.Event.Src.Bus;
using MSVenta.Inventario.Messages.Events;
using MSVenta.Inventario.Models;
using MSVenta.Inventario.Services;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Messages.EventHandlers
{
    public class AlmacenUpdatedEventHandler : IEventHandler<AlmacenUpdatedEvent>
    {
        private readonly IAlmacenService _almacenService;

        public AlmacenUpdatedEventHandler(IAlmacenService almacenService)
        {
            _almacenService = almacenService;
        }

        public async Task Handle(AlmacenUpdatedEvent @event)
        {
            // Lógica para manejar el evento AlmacenUpdatedEvent
            await _almacenService.UpdateAlmacenAsync(new Almacen
            {
                Id = @event.Id,
                Nombre = @event.Nombre,
                Ubicacion = @event.Ubicacion
            });
        }
    }
}
