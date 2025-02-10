using Microsoft.AspNetCore.Mvc;
using MSVenta.Inventario.Models;
using MSVenta.Inventario.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AjusteInventarioController : Controller
    {
        private readonly IAjusteInventarioService _ajusteInventarioService;

        public AjusteInventarioController(IAjusteInventarioService ajusteInventarioService)
        {
            _ajusteInventarioService = ajusteInventarioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AjusteInventario>>> GetAjustes()
        {
            var ajustes = await _ajusteInventarioService.GetAllAsync();
            return Ok(ajustes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AjusteInventario>> GetById(int id)
        {
            var ajusteDTO = (await _ajusteInventarioService.GetAllAsync())
                   .FirstOrDefault(a => a.Id == id);

            if (ajusteDTO == null)
                return NotFound();

            return Ok(ajusteDTO);
        }

        [HttpPost]
        public async Task<ActionResult<AjusteInventarioDTO>> Create([FromBody] AjusteInventario ajusteInventario)
        {
            if (ajusteInventario == null)
                return BadRequest("Datos inválidos");

            await _ajusteInventarioService.AddAsync(ajusteInventario);
            var nuevoAjuste = await _ajusteInventarioService.GetByIdAsync(ajusteInventario.Id);

            var ajusteDTO = new AjusteInventarioDTO
            {
                Id = nuevoAjuste.Id,
                Fecha = nuevoAjuste.Fecha,
                Tipo = nuevoAjuste.Tipo,
                Descripcion = nuevoAjuste.Descripcion,
                IdUsuario = nuevoAjuste.IdUsuario,
                DetallesAjuste = nuevoAjuste.DetallesAjuste.Select(d => new DetalleAjusteInventarioDTO
                {
                    IdProductoAlmacen = d.IdProductoAlmacen,
                    Cantidad = d.Cantidad
                }).ToList()
            };

            return CreatedAtAction(nameof(GetById), new { id = ajusteDTO.Id }, ajusteDTO);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AjusteInventario ajusteInventario)
        {
            if (id != ajusteInventario.Id)
                return BadRequest("Los IDs no coinciden");

            await _ajusteInventarioService.UpdateAsync(ajusteInventario);
            if (!_ajusteInventarioService.Exists(id))
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _ajusteInventarioService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
