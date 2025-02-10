using System.Collections.Generic;
using System.Threading.Tasks;
using MSVenta.Inventario.Models;


namespace MSVenta.Inventario.Services
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> GetAllProductosAsync();
        Task<Producto> GetProductoByIdAsync(int id);
        Task<Producto> CreateProductoAsync(Producto producto);
        Task<Producto> UpdateProductoAsync(Producto producto);
        Task DeleteProductoAsync(int id);
        bool Exists(int id);
    }
}
