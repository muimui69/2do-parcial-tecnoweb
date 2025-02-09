using Microsoft.EntityFrameworkCore;
//using MSVenta.Inventario.DTOs;
using MSVenta.Inventario.Models;
using MSVenta.Inventario.Repositories;
using MSVenta.Inventario.Sevices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSVenta.Venta.Services
{
    public class ProductoAlmacenService :IProductoAlmacenService
    {
        private readonly ContextDatabase _context;

        public ProductoAlmacenService(ContextDatabase context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductoAlmacen>> GetAllAsync()
        {
            return await _context.ProductosAlmacenes
                .Include(pa => pa.Producto)
                    .ThenInclude(p => p.Categoria)
                .Include(pa => pa.Almacen)
                .ToListAsync();
        }

        public async Task<ProductoAlmacen> GetByIdAsync(int id)
        {
            return await _context.ProductosAlmacenes
                .Include(pa => pa.Producto)
                    .ThenInclude(p => p.Categoria)
                .Include(pa => pa.Almacen)
                .FirstOrDefaultAsync(pa => pa.Id == id);
        }

        public async Task<ProductoAlmacen> AddAsync(ProductoAlmacen productoAlmacen)
        {
            _context.ProductosAlmacenes.Add(productoAlmacen);
            await _context.SaveChangesAsync();
            return productoAlmacen;
        }

        public async Task<bool> UpdateAsync(ProductoAlmacen productoAlmacen)
        {
            var existingProductoAlmacen = await _context.ProductosAlmacenes.FindAsync(productoAlmacen.Id);
            if (existingProductoAlmacen == null) return false;

            existingProductoAlmacen.ProductoId = productoAlmacen.ProductoId;
            existingProductoAlmacen.AlmacenId = productoAlmacen.AlmacenId;
            existingProductoAlmacen.Stock = productoAlmacen.Stock;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var productoAlmacen = await _context.ProductosAlmacenes.FindAsync(id);
            if (productoAlmacen == null) return false;

            _context.ProductosAlmacenes.Remove(productoAlmacen);
            await _context.SaveChangesAsync();
            return true;
        }

        //public async Task<AlmacenConProductosDto> GetAlmacenConProductosAsync(int almacenId)
        //{
        //    var productosAlmacen = await _context.ProductosAlmacenes
        //        .Where(pa => pa.AlmacenId == almacenId)
        //        .Include(pa => pa.Producto)
        //            .ThenInclude(p => p.Categoria)
        //        .Include(pa => pa.Almacen)
        //        .ToListAsync();

        //    if (productosAlmacen == null || !productosAlmacen.Any())
        //    {
        //        return null;
        //    }

        //    var almacen = productosAlmacen.FirstOrDefault()?.Almacen;

        //    if (almacen == null)
        //    {
        //        return null;
        //    }

        //    var almacenConProductosDto = new AlmacenConProductosDto
        //    {
        //        AlmacenId = almacen.Id,
        //        AlmacenNombre = almacen.Nombre,
        //        Productos = productosAlmacen.Select(pa => new ProductoDto
        //        {
        //            ProductoId = pa.ProductoId,
        //            Nombre = pa.Producto.Nombre,
        //            Precio = (decimal)pa.Producto.Precio,
        //            Categoria = pa.Producto.Categoria.Nombre,
        //            Stock = pa.Stock
        //        }).ToList()
        //    };

        //    return almacenConProductosDto;
        //}
    }
}
