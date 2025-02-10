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
    public class ProductoController : Controller
    {
        private readonly IProductoService _productoService;
        private readonly IEventBus _bus;
        private readonly IVentaService _ventaService;

        public ProductoController(IProductoService productoService, IEventBus bus, IVentaService ventaService)
        {
            _productoService = productoService;
            _bus = bus;
            _ventaService = ventaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetProductos()
        {
            var productos = await _productoService.GetAllProductosAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetById(int id)
        {
            var producto = await _productoService.GetProductoByIdAsync(id);
            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> Create([FromBody] Producto producto)
        {
            if (producto == null)
                return BadRequest("Datos inválidos");

            Producto productoC = new Producto()
            {
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                IdCategoria = producto.IdCategoria
            };

            productoC = await _productoService.CreateProductoAsync(producto);
            bool isProccess = _ventaService.Execute(productoC, "create");
            if (isProccess)
            {
                var productoCreateCommand = new ProductoCreateCommand(
                    id: productoC.Id,
                    nombre: productoC.Nombre,
                    descripcion: productoC.Descripcion,
                    precio: productoC.Precio,
                    id_categoria: productoC.IdCategoria
                );
                _bus.SendCommand(productoCreateCommand);
            }
            return Ok(productoC);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Producto producto)
        {
            if (id != producto.Id)
                return BadRequest("Los IDs no coinciden");

            if (!_productoService.Exists(id))
                return NotFound();

            Producto productoP = new Producto()
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                IdCategoria = producto.IdCategoria
            };

            productoP = await _productoService.UpdateProductoAsync(producto);

            bool isProccess = _ventaService.Execute(productoP, "update");
            if (isProccess)
            {
                var productoUpdatedCommand = new ProductoUpdatedCommand(
                    id: productoP.Id,
                    nombre: productoP.Nombre,
                    descripcion: productoP.Descripcion,
                    precio: productoP.Precio,
                    id_categoria: productoP.IdCategoria
                );
                _bus.SendCommand(productoUpdatedCommand);
            };

            return Ok(productoP);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!_productoService.Exists(id))
                return NotFound();

            await _productoService.DeleteProductoAsync(id);

            bool isProccess = _ventaService.Execute(new Producto { Id = id }, "delete");
            if (isProccess)
            {
                var productoDeletedCommand = new ProductoDeletedCommand(id);
                _bus.SendCommand(productoDeletedCommand);
            };
            return NoContent();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
