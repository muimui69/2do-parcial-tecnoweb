using MSVenta.Inventario.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Sevices
{
    public interface IProductoAlmacenService
    {
        Task<IEnumerable<ProductoAlmacen>> GetAllAsync();
        Task<ProductoAlmacen> GetByIdAsync(int id);
        Task<ProductoAlmacen> AddAsync(ProductoAlmacen productoAlmacen);
        Task<bool> UpdateAsync(ProductoAlmacen productoAlmacen);
        Task<bool> DeleteAsync(int id);
        //Task<AlmacenConProductosDto> GetAlmacenConProductosAsync(int almacenId);
    }
}
