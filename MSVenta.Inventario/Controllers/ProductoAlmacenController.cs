using Microsoft.AspNetCore.Mvc;
using MSVenta.Inventario.Models;
using MSVenta.Inventario.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aforo255.Cross.Event.Src.Bus;
using MSVenta.Inventario.Messages.Commands;

namespace MSVenta.Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoAlmacenController : Controller
    {
        private readonly IProductoAlmacenService _productoAlmacenService;
        private readonly IEventBus _bus;
        private readonly IVentaService _ventaService;

        public ProductoAlmacenController(IProductoAlmacenService productoAlmacenService, IEventBus bus, IVentaService ventaService)
        {
            _productoAlmacenService = productoAlmacenService;
            _bus = bus;
            _ventaService = ventaService;
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

            // Crear el objeto ProductoAlmacen
            ProductoAlmacen productoAlmacenC = new ProductoAlmacen()
            {
                ProductoId = productoAlmacen.ProductoId,
                AlmacenId = productoAlmacen.AlmacenId,
                Stock = productoAlmacen.Stock
            };

            productoAlmacenC = await _productoAlmacenService.AddAsync(productoAlmacenC);

            // Ejecutar proceso en el servicio de ventas
            bool isProccess = _ventaService.Execute(productoAlmacenC, "create");
            if (isProccess)
            {
                var productoAlmacenCreateCommand = new ProductoAlmacenCreateCommand(
                    id: productoAlmacenC.Id,
                    productoId: productoAlmacenC.ProductoId,
                    almacenId: productoAlmacenC.AlmacenId,
                    stock: productoAlmacenC.Stock
                );
                _bus.SendCommand(productoAlmacenCreateCommand);
            }

            return Ok(productoAlmacenC);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductoAlmacen productoAlmacen)
        {
            if (id != productoAlmacen.Id)
                return BadRequest("Los IDs no coinciden");

            if (!_productoAlmacenService.Exists(id))
                return NotFound();

            // Crear el objeto actualizado
            ProductoAlmacen productoAlmacenP = new ProductoAlmacen()
            {
                Id = productoAlmacen.Id,
                ProductoId = productoAlmacen.ProductoId,
                AlmacenId = productoAlmacen.AlmacenId,
                Stock = productoAlmacen.Stock
            };

            await _productoAlmacenService.UpdateAsync(productoAlmacenP);

            bool isProccess = _ventaService.Execute(productoAlmacenP, "update");
            if (isProccess)
            {
                var productoAlmacenUpdatedCommand = new ProductoAlmacenUpdatedCommand(
                    id: productoAlmacenP.Id,
                    productoId: productoAlmacenP.ProductoId,
                    almacenId: productoAlmacenP.AlmacenId,
                    stock: productoAlmacenP.Stock
                );
                _bus.SendCommand(productoAlmacenUpdatedCommand);
            }

            return Ok(productoAlmacenP);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!_productoAlmacenService.Exists(id))
                return NotFound();

            await _productoAlmacenService.DeleteAsync(id);

            bool isProccess = _ventaService.Execute(new ProductoAlmacen { Id = id }, "delete");
            if (isProccess)
            {
                var productoAlmacenDeletedCommand = new ProductoAlmacenDeletedCommand(id);
                _bus.SendCommand(productoAlmacenDeletedCommand);
            }

            return NoContent();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
