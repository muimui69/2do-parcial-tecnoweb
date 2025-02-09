﻿using MSVenta.Inventario.Repositories;
using MSVenta.Inventario.Models;
using MSVenta.Inventario.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MSVenta.Inventario.Sevices
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ContextDatabase _contextDatabase;

        public CategoriaService(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }

        public async Task<IEnumerable<Categoria>> GetAllCategoriasAsync()
        {
            return await _contextDatabase.Categorias.ToListAsync();
        }

        public async Task<Categoria> GetCategoriaByIdAsync(int id)
        {
            return await _contextDatabase.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateCategoriaAsync(Categoria categoria)
        {
            _contextDatabase.Categorias.Add(categoria);
            await _contextDatabase.SaveChangesAsync();
        }

        public async Task UpdateCategoriaAsync(Categoria categoria)
        {
            _contextDatabase.Categorias.Update(categoria);
            await _contextDatabase.SaveChangesAsync();
        }

        public async Task DeleteCategoriaAsync(int id)
        {
            var categoria = await _contextDatabase.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _contextDatabase.Categorias.Remove(categoria);
                await _contextDatabase.SaveChangesAsync();
            }
        }

        public bool Exists(int id)
        {
            return _contextDatabase.Categorias.Any(c => c.Id == id);
        }
    }
}
