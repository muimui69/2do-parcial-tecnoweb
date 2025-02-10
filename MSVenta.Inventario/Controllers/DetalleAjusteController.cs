using Microsoft.AspNetCore.Mvc;
using MSVenta.Inventario.Models;
using MSVenta.Inventario.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleAjusteController : Controller
    {
        private readonly IDetalleAjusteService _detalleAjusteService;

        public DetalleAjusteController(IDetalleAjusteService detalleAjusteService)
        {
            _detalleAjusteService = detalleAjusteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DetalleAjuste>>> GetDetallesAjuste()
        {
            var detallesAjuste = await _detalleAjusteService.GetAllAsync();
            return Ok(detallesAjuste);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleAjuste>> GetById(int id)
        {
            var detalleAjuste = await _detalleAjusteService.GetByIdAsync(id);
            if (detalleAjuste == null)
                return NotFound();

            return Ok(detalleAjuste);
        }

        [HttpPost]
        public async Task<ActionResult<DetalleAjuste>> Create([FromBody] DetalleAjuste detalleAjuste)
        {
            if (detalleAjuste == null)
                return BadRequest("Datos inválidos");

            await _detalleAjusteService.AddAsync(detalleAjuste);
            var nuevoDetalleAjuste = await _detalleAjusteService.GetByIdAsync(detalleAjuste.Id);
            return CreatedAtAction(nameof(GetById), new { id = nuevoDetalleAjuste.Id }, nuevoDetalleAjuste);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DetalleAjuste detalleAjuste)
        {
            if (id != detalleAjuste.Id)
                return BadRequest("Los IDs no coinciden");

            if (!_detalleAjusteService.Exists(id))
                return NotFound();

            await _detalleAjusteService.UpdateAsync(detalleAjuste);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!_detalleAjusteService.Exists(id))
                return NotFound();

            await _detalleAjusteService.DeleteAsync(id);
            return NoContent();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
