using MSVenta.Inventario.DTOs;
using MSVenta.Inventario.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Services
{
    public interface IDetalleAjusteService
    {
        Task<IEnumerable<DetalleAjusteDTO>> GetAllAsync();
        Task<DetalleAjusteDTO> GetByIdAsync(int id);
        Task<DetalleAjuste> AddAsync(DetalleAjuste detalleAjuste);
        Task<bool> UpdateAsync(DetalleAjuste detalleAjuste);
        Task<bool> DeleteAsync(int id);

        bool Exists(int id);
    }
}