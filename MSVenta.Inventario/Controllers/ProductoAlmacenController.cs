using Microsoft.AspNetCore.Mvc;
using MSVenta.Inventario.Models;
using MSVenta.Inventario.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoAlmacenController : Controller
    {
        private readonly IProductoAlmacenService _productoAlmacenService;

        public ProductoAlmacenController(IProductoAlmacenService productoAlmacenService)
        {
            _productoAlmacenService = productoAlmacenService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductoAlmacen>>> GetProductosAlmacenes()
        {
            var productosAlmacenes = await _productoAlmacenService.GetAllAsync();
            return Ok(productosAlmacenes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoAlmacen>> GetById(int id)
        {
            var productoAlmacen = await _productoAlmacenService.GetByIdAsync(id);
            if (productoAlmacen == null)
                return NotFound();

            return Ok(productoAlmacen);
        }

        [HttpPost]
        public async Task<ActionResult<ProductoAlmacen>> Create([FromBody] ProductoAlmacen productoAlmacen)
        {
            if (productoAlmacen == null)
                return BadRequest("Datos inválidos");

            await _productoAlmacenService.AddAsync(productoAlmacen);
            var nuevoProductoAlmacen = await _productoAlmacenService.GetByIdAsync(productoAlmacen.Id);
            return CreatedAtAction(nameof(GetById), new { id = nuevoProductoAlmacen.Id }, nuevoProductoAlmacen);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductoAlmacen productoAlmacen)
        {
            if (id != productoAlmacen.Id)
                return BadRequest("Los IDs no coinciden");

            if (!_productoAlmacenService.Exists(id))
                return NotFound();

            await _productoAlmacenService.UpdateAsync(productoAlmacen);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!_productoAlmacenService.Exists(id))
                return NotFound();

            await _productoAlmacenService.DeleteAsync(id);
            return NoContent();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
