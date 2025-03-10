﻿using MSVenta.Inventario.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Services
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> GetAllCategoriasAsync();
        Task<Categoria> GetCategoriaByIdAsync(int id);
        Task <Categoria> CreateCategoriaAsync(Categoria categoria);
        Task<Categoria>UpdateCategoriaAsync(Categoria categoria);
        Task DeleteCategoriaAsync(int id);
        bool Exists(int id);
    }
}