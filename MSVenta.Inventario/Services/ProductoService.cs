﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSVenta.Inventario.Models;
using MSVenta.Inventario.Repositories;

namespace MSVenta.Inventario.Services
{
    public class ProductoService : IProductoService
    {
        private readonly ContextDatabase _contextDatabase;

        public ProductoService(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }

        public async Task<IEnumerable<Producto>> GetAllProductosAsync()
        {
            return await _contextDatabase.Productos.Include(p => p.Categoria).ToListAsync();
        }

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            return await _contextDatabase.Productos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Producto> CreateProductoAsync(Producto producto)
        {
            _contextDatabase.Productos.Add(producto);
            await _contextDatabase.SaveChangesAsync();
            return producto;    
        }

        public async Task<Producto> UpdateProductoAsync(Producto producto)
        {
            _contextDatabase.Productos.Update(producto);
            await _contextDatabase.SaveChangesAsync();
            return producto;
        }

        public async Task DeleteProductoAsync(int id)
        {
            var producto = await _contextDatabase.Productos.FindAsync(id);
            if (producto != null)
            {
                _contextDatabase.Productos.Remove(producto);
                await _contextDatabase.SaveChangesAsync();
            }
        }

        public bool Exists(int id)
        {
            return _contextDatabase.Productos.Any(p => p.Id == id);
        }
    }
}
