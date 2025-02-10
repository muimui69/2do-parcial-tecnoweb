using System.Collections.Generic;
using System.Threading.Tasks;
using MSVenta.Inventario.Models;

namespace MSVenta.Inventario.Services
{
    public interface IAlmacenService
    {
        Task<IEnumerable<Almacen>> GetAllAlmacenesAsync();
        Task<Almacen> GetAlmacenByIdAsync(int id);
        Task<Almacen> CreateAlmacenAsync(Almacen almacen);
        Task<Almacen> UpdateAlmacenAsync(Almacen almacen);
        Task DeleteAlmacenAsync(int id);
        bool Exists(int id);
    }
}
