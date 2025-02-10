using MSVenta.Inventario.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Services
{
    public interface IProductoAlmacenService
    {
        //Task<IEnumerable<ProductoAlmacenDTO>> GetAllAsync();
        Task<IEnumerable<ProductoAlmacen>> GetAllAsync();

        Task<ProductoAlmacenDTO> GetByIdAsync(int id);
        Task<ProductoAlmacen> AddAsync(ProductoAlmacen productoAlmacen);
        Task<bool> UpdateAsync(ProductoAlmacen productoAlmacen);
        Task<bool> DeleteAsync(int id);
        bool Exists(int id);

    }
}
