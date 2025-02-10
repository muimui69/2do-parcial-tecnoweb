using Aforo255.Cross.Event.Src.Bus;
using MSVenta.Venta.Models;
using MSVenta.Venta.Repositories;
using MSVenta.Venta.Messages.Events;
using System.Threading.Tasks;

namespace MSVenta.Venta.Messages.EventHandlers
{
    public class CategoriaCreatedEventHandler : IEventHandler<CategoriaCreatedEvent>
    {
        private readonly ContextDatabase _contextDatabase;

        public CategoriaCreatedEventHandler(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }

        public Task Handle(CategoriaCreatedEvent @event)
        {
            var categoria = new Categoria()
            {
                Id = @event.Id,
                Nombre = @event.Nombre
            };
            _contextDatabase.Categorias.Add(categoria);
            _contextDatabase.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
