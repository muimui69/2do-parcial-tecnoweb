using Aforo255.Cross.Event.Src.Bus;
using Microsoft.AspNetCore.Mvc;
using MSVenta.Inventario.Messages.Commands;
using MSVenta.Inventario.Models;
using MSVenta.Inventario.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSVenta.Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : Controller
    {
        private readonly IEventBus _bus;
        private readonly ICategoriaService _categoriaService;
        private readonly IVentaService _ventaService;

        public CategoriaController(ICategoriaService categoriaService, IEventBus bus,IVentaService ventaService)
        {
            _categoriaService = categoriaService;
            _bus = bus;
            _ventaService = ventaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> GetCategorias()
        {
            var categorias = await _categoriaService.GetAllCategoriasAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetById(int id)
        {
            var categoria = await _categoriaService.GetCategoriaByIdAsync(id);
            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> Create([FromBody] Categoria categoria)
        {
            if (categoria == null)
                return BadRequest("Datos inválidos");

            await _categoriaService.CreateCategoriaAsync(categoria);
            var nuevaCategoria = await _categoriaService.GetCategoriaByIdAsync(categoria.Id);
            return CreatedAtAction(nameof(GetById), new { id = nuevaCategoria.Id }, nuevaCategoria);
        }

        [HttpPost("CrearCategoria")]
        public async Task<ActionResult<Categoria>> CrearCategoria([FromBody] Categoria request)
        {
            Categoria categoria = new Categoria()
            {
                Nombre = request.Nombre
            };

            categoria = await _categoriaService.CreateCategoriaAsync(categoria);

            bool isProccess = _ventaService.Execute(categoria);
            if (isProccess) 
            {
                var categoriaCreateCommand = new CategoriaCreateCommand(
                id: categoria.Id,
                nombre: categoria.Nombre
                );
                _bus.SendCommand(categoriaCreateCommand);
            };
            return Ok(categoria);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.Id)
                return BadRequest("Los IDs no coinciden");

            if (!_categoriaService.Exists(id))
                return NotFound();

            await _categoriaService.UpdateCategoriaAsync(categoria);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!_categoriaService.Exists(id))
                return NotFound();

            await _categoriaService.DeleteCategoriaAsync(id);
            return NoContent();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
