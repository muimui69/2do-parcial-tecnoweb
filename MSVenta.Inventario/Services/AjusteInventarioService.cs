using Microsoft.EntityFrameworkCore;
using MSVenta.Inventario.Models;
using MSVenta.Inventario.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Services
{
    public class AjusteInventarioService : IAjusteInventarioService
    {
        private readonly ContextDatabase _context;

        public AjusteInventarioService(ContextDatabase context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AjusteInventarioDTO>> GetAllAsync()
        {
            var ajustes = await _context.AjustesInventario
                .Include(a => a.DetallesAjuste)
                    .ThenInclude(d => d.ProductoAlmacen)
                        .ThenInclude(pa => pa.Producto)
                .Include(a => a.DetallesAjuste)
                    .ThenInclude(d => d.ProductoAlmacen)
                        .ThenInclude(pa => pa.Almacen)
                .ToListAsync();

            // Convertir los ajustes a DTOs
            var ajusteDTOs = ajustes.Select(a => new AjusteInventarioDTO
            {
                Id = a.Id,
                Fecha = a.Fecha,
                Tipo = a.Tipo,
                Descripcion = a.Descripcion,
                DetallesAjuste = a.DetallesAjuste.Select(d => new DetalleAjusteInventarioDTO
                {
                    IdProductoAlmacen = d.IdProductoAlmacen,
                    Cantidad = d.Cantidad,
                    NombreProducto = d.ProductoAlmacen?.Producto?.Nombre,
                    NombreAlmacen = d.ProductoAlmacen?.Almacen?.Nombre
                }).ToList()
            });

            return ajusteDTOs;
        }


        public async Task<AjusteInventario> GetByIdAsync(int id)
        {
            return await _context.AjustesInventario.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<AjusteInventario> AddAsync(AjusteInventario ajusteInventario)
        {
            // Guardar el ajuste principal primero
            _context.AjustesInventario.Add(ajusteInventario);
            await _context.SaveChangesAsync();

            foreach (var detalle in ajusteInventario.DetallesAjuste)
            {
                detalle.IdAjusteInventario = ajusteInventario.Id;  // Relacionar el detalle con el ajuste creado

                // Verificar que el producto_almacen existe
                var productoAlmacen = await _context.ProductosAlmacenes
                    .FirstOrDefaultAsync(pa => pa.Id == detalle.IdProductoAlmacen);

                if (productoAlmacen == null)
                {
                    throw new Exception($"El registro producto_almacen con ID {detalle.IdProductoAlmacen} no existe.");
                }

                // Ajustar el stock dependiendo del tipo de ajuste (Ingreso o Egreso)
                if (ajusteInventario.Tipo.ToLower() == "ingreso")
                {
                    productoAlmacen.Stock += detalle.Cantidad;
                }
                else if (ajusteInventario.Tipo.ToLower() == "egreso")
                {
                    productoAlmacen.Stock -= detalle.Cantidad;

                    // Evitar que el stock sea negativo
                    if (productoAlmacen.Stock < 0)
                    {
                        productoAlmacen.Stock = 0;
                    }
                }
                else
                {
                    throw new Exception($"Tipo de ajuste no válido: {ajusteInventario.Tipo}. Debe ser 'ingreso' o 'egreso'.");
                }

                // Solo actualiza el stock en la tabla producto_almacen
                _context.ProductosAlmacenes.Update(productoAlmacen);

                // Aquí guardamos el detalle del ajuste sin conflicto de clave primaria
                detalle.Id = 0; // Asegúrate de que EF Core genere un nuevo ID autoincremental
                _context.DetallesAjuste.Add(detalle);
            }

            // Guardar todos los cambios en la base de datos
            await _context.SaveChangesAsync();
            return ajusteInventario;
        }



        public async Task<bool> UpdateAsync(AjusteInventario ajusteInventario)
        {
            var existingAjuste = await _context.AjustesInventario.FindAsync(ajusteInventario.Id);
            if (existingAjuste == null) return false;

            existingAjuste.Fecha = ajusteInventario.Fecha;
            existingAjuste.Tipo = ajusteInventario.Tipo;
            existingAjuste.Descripcion = ajusteInventario.Descripcion;
            existingAjuste.IdUsuario = ajusteInventario.IdUsuario; 

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ajusteInventario = await _context.AjustesInventario.FindAsync(id);
            if (ajusteInventario == null) return false;

            _context.AjustesInventario.Remove(ajusteInventario);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool Exists(int id)
        {
            return _context.AjustesInventario.Any(a => a.Id == id);
        }

       
    }
}
