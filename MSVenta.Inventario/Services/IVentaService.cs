using MSVenta.Inventario.Models;

namespace MSVenta.Inventario.Services
{
    public interface IVentaService
    {
        bool Execute(Categoria request);
    }
}
