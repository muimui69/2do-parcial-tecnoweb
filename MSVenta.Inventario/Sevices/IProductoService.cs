﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MSVenta.Inventario.Models;


namespace MSVenta.Inventario.Sevices
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> GetAllProductosAsync();
        Task<Producto> GetProductoByIdAsync(int id);
        Task CreateProductoAsync(Producto producto);
        Task UpdateProductoAsync(Producto producto);
        Task DeleteProductoAsync(int id);
        bool Exists(int id);
    }
}
