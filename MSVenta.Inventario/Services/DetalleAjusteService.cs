using Microsoft.EntityFrameworkCore;
using MSVenta.Inventario.DTOs;
using MSVenta.Inventario.Models;
using MSVenta.Inventario.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Services
{
    public class DetalleAjusteService : IDetalleAjusteService
    {
        private readonly ContextDatabase _contextDatabase;

        public DetalleAjusteService(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }

        public async Task<IEnumerable<DetalleAjusteDTO>> GetAllAsync()
        {
            return await _contextDatabase.DetallesAjuste
                .Include(da => da.AjusteInventario)
                .Include(da => da.ProductoAlmacen)
                .Select(da => new DetalleAjusteDTO
                {
                    Id = da.Id,
                    IdAjusteInventario = da.IdAjusteInventario,
                    AjusteInventario = new AjusteInventarioDetalleDTO
                    {
                        Id = da.AjusteInventario.Id,
                        Descripcion = da.AjusteInventario.Descripcion
                    },
                    IdProductoAlmacen = da.IdProductoAlmacen,
                    ProductoAlmacen = new ProductoAlmacenDetalleDTO
                    {
                        Id = da.ProductoAlmacen.Id,
                        Stock = da.ProductoAlmacen.Stock
                    },
                    Cantidad = da.Cantidad
                })
                .ToListAsync();
        }



        public async Task<DetalleAjusteDTO> GetByIdAsync(int id)
        {
            var detalleAjuste = await _contextDatabase.DetallesAjuste
                .Include(da => da.AjusteInventario)
                .Include(da => da.ProductoAlmacen)
                    .ThenInclude(pa => pa.Producto)
                .Include(da => da.ProductoAlmacen.Almacen)
                .FirstOrDefaultAsync(da => da.Id == id);

            if (detalleAjuste == null)
                return null;

            return new DetalleAjusteDTO
            {
                Id = detalleAjuste.Id,
                IdAjusteInventario = detalleAjuste.IdAjusteInventario,
                AjusteInventario = new AjusteInventarioDetalleDTO
                {
                    Id = detalleAjuste.AjusteInventario.Id,
                    Descripcion = detalleAjuste.AjusteInventario.Descripcion
                },
                IdProductoAlmacen = detalleAjuste.IdProductoAlmacen,
                ProductoAlmacen = new ProductoAlmacenDetalleDTO
                {
                    Id = detalleAjuste.ProductoAlmacen.Id,
                    Stock = detalleAjuste.ProductoAlmacen.Stock,
                    Producto = new ProductoDTO
                    {
                        Id = detalleAjuste.ProductoAlmacen.Producto.Id,
                        Nombre = detalleAjuste.ProductoAlmacen.Producto.Nombre
                    },
                    Almacen = new AlmacenDTO
                    {
                        Id = detalleAjuste.ProductoAlmacen.Almacen.Id,
                        Nombre = detalleAjuste.ProductoAlmacen.Almacen.Nombre
                    }
                },
                Cantidad = detalleAjuste.Cantidad
            };
        }


        public async Task<DetalleAjuste> AddAsync(DetalleAjuste detalleAjuste)
        {
            _contextDatabase.DetallesAjuste.Add(detalleAjuste);
            await _contextDatabase.SaveChangesAsync();
            return detalleAjuste;
        }

        public async Task<bool> UpdateAsync(DetalleAjuste detalleAjuste)
        {
            var existingDetalleAjuste = await _contextDatabase.DetallesAjuste.FindAsync(detalleAjuste.Id);
            if (existingDetalleAjuste == null) return false;

            existingDetalleAjuste.IdAjusteInventario = detalleAjuste.IdAjusteInventario;
            existingDetalleAjuste.IdProductoAlmacen = detalleAjuste.IdProductoAlmacen;
            existingDetalleAjuste.Cantidad = detalleAjuste.Cantidad;

            await _contextDatabase.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var detalleAjuste = await _contextDatabase.DetallesAjuste.FindAsync(id);
            if (detalleAjuste == null) return false;

            _contextDatabase.DetallesAjuste.Remove(detalleAjuste);
            await _contextDatabase.SaveChangesAsync();
            return true;
        }

        public bool Exists(int id)
        {
            return _contextDatabase.DetallesAjuste.Any(da => da.Id == id);
        }
    }
}
