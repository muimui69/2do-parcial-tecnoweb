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
    public class AlmacenController : Controller
    {
        private readonly IEventBus _bus;
        private readonly IAlmacenService _almacenService;
        private readonly IVentaService _ventaService;

        public AlmacenController(IAlmacenService almacenService, IEventBus bus, IVentaService ventaService)
        {
            _almacenService = almacenService;
            _bus = bus;
            _ventaService = ventaService;
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

            Almacen almacenC = new Almacen()
            {
                Nombre = almacen.Nombre,
                Ubicacion = almacen.Ubicacion
            };

            almacenC = await _almacenService.CreateAlmacenAsync(almacen);
            bool isProccess = _ventaService.Execute(almacenC, "create");
            if (isProccess)
            {
                var almacenCreateCommand = new AlmacenCreateCommand(
                    id: almacenC.Id,
                    nombre: almacenC.Nombre,
                    ubicacion: almacenC.Ubicacion
                );
                _bus.SendCommand(almacenCreateCommand);
            };
            return Ok(almacenC);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Almacen almacen)
        {
            if (id != almacen.Id)
                return BadRequest("Los IDs no coinciden");

            if (!_almacenService.Exists(id))
                return NotFound();

            Almacen almacenP = new Almacen()
            {
                Id = almacen.Id,
                Nombre = almacen.Nombre,
                Ubicacion = almacen.Ubicacion
            };

            almacenP = await _almacenService.UpdateAlmacenAsync(almacen);

            bool isProccess = _ventaService.Execute(almacenP, "update");
            if (isProccess)
            {
                var almacenUpdatedCommand = new AlmacenUpdatedCommand(
                    id: almacenP.Id,
                    nombre: almacenP.Nombre,
                    ubicacion: almacenP.Ubicacion
                );
                _bus.SendCommand(almacenUpdatedCommand);
            };

            return Ok(almacenP);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!_almacenService.Exists(id))
                return NotFound();

            await _almacenService.DeleteAlmacenAsync(id);

            bool isProccess = _ventaService.Execute(new Almacen { Id = id }, "delete");
            if (isProccess)
            {
                var almacenDeletedCommand = new AlmacenDeletedCommand(id);
                _bus.SendCommand(almacenDeletedCommand);
            };
            return NoContent();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
