using Aforo255.Cross.Event.Src.Events;

namespace MSVenta.Inventario.Messages.Events
{
    public class AlmacenUpdatedEvent : Event
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }

        public AlmacenUpdatedEvent(int id, string nombre, string ubicacion)
        {
            Id = id;
            Nombre = nombre;
            Ubicacion = ubicacion;
        }
    }
}
