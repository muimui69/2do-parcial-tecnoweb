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

            Categoria categoriaC = new Categoria()
            {
                Nombre = categoria.Nombre
            };

            categoriaC = await _categoriaService.CreateCategoriaAsync(categoria);
            bool isProccess = _ventaService.Execute(categoriaC, "create");
            if (isProccess)
            {
                var categoriaCreateCommand = new CategoriaCreateCommand(
                id: categoriaC.Id,
                nombre: categoriaC.Nombre
                );
                _bus.SendCommand(categoriaCreateCommand);
            };
            return Ok(categoriaC);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.Id)
                return BadRequest("Los IDs no coinciden");

            if (!_categoriaService.Exists(id))
                return NotFound();
            
            Categoria categoriaP = new Categoria()
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre
            };

            categoriaP = await _categoriaService.UpdateCategoriaAsync(categoria);


            bool isProccess = _ventaService.Execute(categoriaP, "update");
            if (isProccess)
            {
                var categoriaUpdatedCommand = new CategoriaUpdatedCommand(
                    id: categoriaP.Id,
                    nombre: categoriaP.Nombre
                );
                _bus.SendCommand(categoriaUpdatedCommand);
            };

            return Ok(categoriaP);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!_categoriaService.Exists(id))
                return NotFound();

            await _categoriaService.DeleteCategoriaAsync(id);

            bool isProccess = _ventaService.Execute(new Categoria { Id = id}, "delete");
            if (isProccess)
            {
                var categoriaDeletedCommand = new CategoriaDeletedCommand(id);
                _bus.SendCommand(categoriaDeletedCommand);
            };
            return NoContent();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
