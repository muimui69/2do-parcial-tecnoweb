using MSVenta.Inventario.Models;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Services
{
    public interface IVentaService
    {
        // Categoria
        Task<bool> CreateCategoria(Categoria request);
        Task<bool> UpdateCategoria(Categoria request);
        Task<bool> DeleteCategoria(int id);

        // Producto
        Task<bool> CreateProducto(Producto request);
        Task<bool> UpdateProducto(Producto request);
        Task<bool> DeleteProducto(int id);

        // Almacen
        Task<bool> CreateAlmacen(Almacen request);
        Task<bool> UpdateAlmacen(Almacen request);
        Task<bool> DeleteAlmacen(int id);

        // Producto_Almacen
        Task<bool> CreateProductoAlmacen(ProductoAlmacen request);
        Task<bool> UpdateProductoAlmacen(ProductoAlmacen request);
        Task<bool> DeleteProductoAlmacen(int id);

        bool Execute<T>(T request, string action) where T : class;
    }
}
