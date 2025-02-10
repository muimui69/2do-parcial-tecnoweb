using MSVenta.Inventario.Services;
using MSVenta.Inventario.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MSVenta.Inventario.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class AlmacenService : IAlmacenService
{
    private readonly ContextDatabase _contextDatabase;

    public AlmacenService(ContextDatabase contextDatabase)
    {
        _contextDatabase = contextDatabase;
    }

    public async Task<IEnumerable<Almacen>> GetAllAlmacenesAsync()
    {
        return await _contextDatabase.Almacenes.ToListAsync();
    }

    public async Task<Almacen> GetAlmacenByIdAsync(int id)
    {
        return await _contextDatabase.Almacenes.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Almacen> CreateAlmacenAsync(Almacen almacen)
    {
        _contextDatabase.Almacenes.Add(almacen);
        await _contextDatabase.SaveChangesAsync();
        return almacen;
    }

    public async Task<Almacen> UpdateAlmacenAsync(Almacen almacen)
    {
        _contextDatabase.Almacenes.Update(almacen);
        await _contextDatabase.SaveChangesAsync();
        return almacen;
    }

    public async Task DeleteAlmacenAsync(int id)
    {
        var almacen = await _contextDatabase.Almacenes.FindAsync(id);
        if (almacen != null)
        {
            _contextDatabase.Almacenes.Remove(almacen);
            await _contextDatabase.SaveChangesAsync();
        }
    }

    public bool Exists(int id)
    {
        return _contextDatabase.Almacenes.Any(a => a.Id == id);
    }
}