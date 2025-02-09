using System.Collections.Generic;
using System.Threading.Tasks;
using MSVenta.Inventario.Models;

namespace MSVenta.Inventario.Sevices
{
    public interface IAlmacenService
    {
        Task<IEnumerable<Almacen>> GetAllAlmacenesAsync();
        Task<Almacen> GetAlmacenByIdAsync(int id);
        Task CreateAlmacenAsync(Almacen almacen);
        Task UpdateAlmacenAsync(Almacen almacen);
        Task DeleteAlmacenAsync(int id);
        bool Exists(int id);
    }
}
