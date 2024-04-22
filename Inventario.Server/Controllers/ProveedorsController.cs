using Inventarios.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorsController : ControllerBase
    {
        private readonly MiDbContext _context;

        public ProveedorsController(MiDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar(Proveedor proveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Proveedors.AddAsync(proveedor);
            await _context.SaveChangesAsync();

            return Ok("Se guardo exitosamente");
        }

        [HttpGet]
        [Route("Consultar")]
        public async Task<ActionResult<IEnumerable<Proveedor>>> Consultar()
        {
            var proveedors = await _context.Proveedors.ToListAsync();

            if (proveedors == null || proveedors.Count == 0)
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                return Ok(proveedors);
            }
        }

        [HttpGet]
        [Route("Filtrar/Id")]
        public async Task<ActionResult> FiltarPorId(int id)
        {
            Proveedor proveedor = await _context.Proveedors.FindAsync(id);

            if (proveedor == null)
            {
                return NotFound("No existes el dato que buscas");
            }
            else
            {
                return Ok(proveedor);
            }
        }

        [HttpGet]
        [Route("Filtrar/Nombre")]
        public async Task<ActionResult<List<Proveedor>>> FiltarPorNombre(string nombre)
        {
            var proveedor = await _context.Proveedors
                .Where(p => p.Nombre == nombre)
                .ToListAsync();

            if (proveedor == null || proveedor.Count == 0)
            {
                return NotFound("No existen el dato que buscas");
            }
            else
            {
                return Ok(proveedor);
            }
        }

        [HttpGet]
        [Route("Filtrar/Celular")]
        public async Task<ActionResult> FiltarPorCelular(string celular)
        {
            var proveedor = await _context.Proveedors.Where(p => p.Celular == celular).ToListAsync();

            if (proveedor == null)
            {
                return NotFound("No existes el dato que buscas");
            }
            else
            {
                return Ok(proveedor);
            }
        }

        [HttpGet]
        [Route("Filtrar/Correo")]
        public async Task<ActionResult> FiltarPorCorreo(string correo)
        {
            var proveedor = await _context.Proveedors.Where(p => p.Correo == correo).ToListAsync();

            if (proveedor == null)
            {
                return NotFound("No existes el dato que buscas");
            }
            else
            {
                return Ok(proveedor);
            }
        }


        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar(int id, Proveedor proveedor)
        {
            var proveedorExistente = await _context.Proveedors.FindAsync(id);

            if (proveedorExistente == null)
            {
                return NotFound("No existes el dato que buscas");
            }
            else
            {
                proveedorExistente!.Nombre = proveedor.Nombre;
                proveedorExistente!.Celular = proveedor.Celular;
                proveedorExistente!.Correo = proveedor.Correo;

                await _context.SaveChangesAsync();

                return Ok("Se actualizo exitosamente");
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var proveedorEliminar = await _context.Proveedors.FindAsync(id);

            if (proveedorEliminar == null)
            {
                return NotFound("No existes el dato que buscas");
            }
            else
            {
                _context.Proveedors.Remove(proveedorEliminar!);

                await _context.SaveChangesAsync();

                return Ok("Se elimino exitosamente");
            }

        }
    }
}
