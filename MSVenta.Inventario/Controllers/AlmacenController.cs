using Microsoft.AspNetCore.Mvc;
using MSVenta.Inventario.Models;
using MSVenta.Inventario.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlmacenController : Controller
    {
        private readonly IAlmacenService _almacenService;

        public AlmacenController(IAlmacenService almacenService)
        {
            _almacenService = almacenService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Almacen>>> GetAlmacenes()
        {
            var almacenes = await _almacenService.GetAllAlmacenesAsync();
            return Ok(almacenes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Almacen>> GetById(int id)
        {
            var almacen = await _almacenService.GetAlmacenByIdAsync(id);
            if (almacen == null)
                return NotFound();

            return Ok(almacen);
        }

        [HttpPost]
        public async Task<ActionResult<Almacen>> Create([FromBody] Almacen almacen)
        {
            if (almacen == null)
                return BadRequest("Datos inválidos");

            await _almacenService.CreateAlmacenAsync(almacen);
            var nuevoAlmacen = await _almacenService.GetAlmacenByIdAsync(almacen.Id);
            return CreatedAtAction(nameof(GetById), new { id = nuevoAlmacen.Id }, nuevoAlmacen);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Almacen almacen)
        {
            if (id != almacen.Id)
                return BadRequest("Los IDs no coinciden");

            await _almacenService.UpdateAlmacenAsync(almacen);
            if (!_almacenService.Exists(id))
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!_almacenService.Exists(id))
                return NotFound();

            await _almacenService.DeleteAlmacenAsync(id);
            return NoContent();
        }

        public IActionResult Index()
        {
            return View();
        }
    }

}