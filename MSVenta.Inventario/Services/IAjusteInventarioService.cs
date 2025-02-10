using MSVenta.Inventario.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Services
{
    public interface IAjusteInventarioService
    {
        Task<IEnumerable<AjusteInventarioDTO>> GetAllAsync();
        Task<AjusteInventario> GetByIdAsync(int id);
        Task<AjusteInventario> AddAsync(AjusteInventario ajusteInventario);
        Task<bool> UpdateAsync(AjusteInventario ajusteInventario);
        Task<bool> DeleteAsync(int id);
        bool Exists(int id);
    }
}
