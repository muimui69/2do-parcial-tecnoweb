using Aforo255.Cross.Event.Src.Bus;
using MSVenta.Inventario.Messages.Events;
using MSVenta.Inventario.Repositories;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Messages.EventHandlers
{
    public class CategoriaDeletedEventHandler : IEventHandler<CategoriaDeletedEvent>
    {
        private readonly ContextDatabase _contextDatabase;

        public CategoriaDeletedEventHandler(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }

        public Task Handle(CategoriaDeletedEvent @event)
        {
            var categoria = _contextDatabase.Categorias.Find(@event.Id);
            if (categoria != null)
            {
                _contextDatabase.Categorias.Remove(categoria);
                _contextDatabase.SaveChanges();
            }
            return Task.CompletedTask;
        }
    }
}
