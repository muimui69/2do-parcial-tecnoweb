using Aforo255.Cross.Event.Src.Bus;
using MSVenta.Inventario.Messages.Events;
using MSVenta.Inventario.Repositories;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Messages.EventHandlers
{
    public class CategoriaUpdatedEventHandler : IEventHandler<CategoriaUpdatedEvent>
    {
        private readonly ContextDatabase _contextDatabase;

        public CategoriaUpdatedEventHandler(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }

        public Task Handle(CategoriaUpdatedEvent @event)
        {
            var categoria = _contextDatabase.Categorias.Find(@event.Id);
            if (categoria != null)
            {
                categoria.Nombre = @event.Nombre;
                _contextDatabase.SaveChanges();
            }
            return Task.CompletedTask;
        }
    }
}
