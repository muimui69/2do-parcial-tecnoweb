using Microsoft.EntityFrameworkCore;
using MSVenta.Inventario.DTOs;

//using MSVenta.Inventario.DTOs;
using MSVenta.Inventario.Models;
using MSVenta.Inventario.Repositories;
using MSVenta.Inventario.Services;
using Polly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Services
{
    public class ProductoAlmacenService :IProductoAlmacenService
    {
        private readonly ContextDatabase _contextDatabase;

        public ProductoAlmacenService(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }

        //public async Task<IEnumerable<ProductoAlmacenDTO>> GetAllAsync()
        public async Task<IEnumerable<ProductoAlmacen>> GetAllAsync()
        {
            //return await _contextDatabase.ProductosAlmacenes
            //    .Include(pa => pa.Producto)
            //    .Include(pa => pa.Almacen)
            //    .Include(pa => pa.DetallesAjuste)
            //    .Select(pa => new ProductoAlmacenDTO
            //    {
            //        Id = pa.Id,
            //        ProductoNombre = pa.Producto.Nombre,
            //        AlmacenNombre = pa.Almacen.Nombre,
            //        Stock = pa.Stock,
            //        DetallesAjuste = pa.DetallesAjuste.Select(da => new DetalleAjusteProductoAlmacenDTO
            //        {
            //            IdDetalleAjuste = da.Id,
            //            Cantidad = da.Cantidad
            //        }).ToList()
            //    })
            //    .ToListAsync();
            return await _contextDatabase.ProductosAlmacenes
              .Include(pa => pa.Producto)
                  .ThenInclude(c => c.Categoria)
              .Include(pa => pa.Almacen)
              .ToListAsync();
        }


          public async Task<ProductoAlmacenDTO> GetByIdAsync(int id)
          {
                return await _contextDatabase.ProductosAlmacenes
                    .Include(pa => pa.Producto)
                    .Include(pa => pa.Almacen)
                    .Include(pa => pa.DetallesAjuste)
                    .Where(pa => pa.Id == id)
                    .Select(pa => new ProductoAlmacenDTO
                    {
                        Id = pa.Id,
                        ProductoNombre = pa.Producto.Nombre,
                        AlmacenNombre = pa.Almacen.Nombre,
                        Stock = pa.Stock,
                        DetallesAjuste = pa.DetallesAjuste.Select(da => new DetalleAjusteProductoAlmacenDTO
                        {
                             IdDetalleAjuste = da.Id,
                            Cantidad = da.Cantidad
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();
          }


        public async Task<ProductoAlmacen> AddAsync(ProductoAlmacen productoAlmacen)
        {
            _contextDatabase.ProductosAlmacenes.Add(productoAlmacen);
            await _contextDatabase.SaveChangesAsync();
            return productoAlmacen;
        }

        public async Task<bool> UpdateAsync(ProductoAlmacen productoAlmacen)
        {
            var existingProductoAlmacen = await _contextDatabase.ProductosAlmacenes.FindAsync(productoAlmacen.Id);
            if (existingProductoAlmacen == null) return false;

            existingProductoAlmacen.ProductoId = productoAlmacen.ProductoId;
            existingProductoAlmacen.AlmacenId = productoAlmacen.AlmacenId;
            existingProductoAlmacen.Stock = productoAlmacen.Stock;

            await _contextDatabase.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var productoAlmacen = await _contextDatabase.ProductosAlmacenes.FindAsync(id);
            if (productoAlmacen == null) return false;

            _contextDatabase.ProductosAlmacenes.Remove(productoAlmacen);
            await _contextDatabase.SaveChangesAsync();
            return true;
        }

        public bool Exists(int id)
        {
            return _contextDatabase.ProductosAlmacenes.Any(a => a.Id == id);
        }

        public async Task<AlmacenConProductosDto> GetAlmacenConProductosAsync(int almacenId)
        {
            var productosAlmacen = await _contextDatabase.ProductosAlmacenes
                .Where(pa => pa.AlmacenId == almacenId)
                .Include(pa => pa.Producto)
                    .ThenInclude(p => p.Categoria)
                .Include(pa => pa.Almacen)
                .ToListAsync();

            if (productosAlmacen == null || !productosAlmacen.Any())
            {
                return null;
            }

            var almacen = productosAlmacen.FirstOrDefault()?.Almacen;

            if (almacen == null)
            {
                return null;
            }

            var almacenConProductosDto = new AlmacenConProductosDto
            {
                AlmacenId = almacen.Id,
                AlmacenNombre = almacen.Nombre,
                Productos = productosAlmacen.Select(pa => new ProductoAllDto
                {
                    ProductoId = pa.ProductoId,
                    Nombre = pa.Producto.Nombre,
                    Precio = (decimal)pa.Producto.Precio,
                    Categoria = pa.Producto.Categoria.Nombre,
                    Stock = pa.Stock
                }).ToList()
            };

            return almacenConProductosDto;
        }
    }
}
