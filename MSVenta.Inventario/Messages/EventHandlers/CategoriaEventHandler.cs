using Aforo255.Cross.Event.Src.Bus;
using MSVenta.Inventario.Models;
using MSVenta.Inventario.Messages.Events;
using MSVenta.Inventario.Repositories;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Messages.EventHandlers
{
    public class CategoriaEventHandler : IEventHandler<CategoriaCreatedEvent>
    {
        private readonly ContextDatabase _contextDatabase;

        public CategoriaEventHandler(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }

        public Task Handle(CategoriaCreatedEvent @event)
        {
            Categoria categoria = new Categoria()
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
