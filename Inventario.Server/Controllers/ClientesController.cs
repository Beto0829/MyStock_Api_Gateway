using Inventarios.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly MiDbContext _context;

        public ClientesController(MiDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            return Ok("Se guardo exitosamente");
        }

        [HttpGet]
        [Route("Consultar")]
        public async Task<ActionResult<IEnumerable<Cliente>>> Consultar()
        {
            var clientes = await _context.Clientes.ToListAsync();

            if (clientes == null || clientes.Count == 0)
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                return Ok(clientes);
            }
        }

        [HttpGet]
        [Route("Filtrar/Id")]
        public async Task<ActionResult> FiltarPorId(int id)
        {
            Cliente cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound("No existes el dato que buscas");
            }
            else
            {
                return Ok(cliente);
            }
        }

        [HttpGet]
        [Route("Filtrar/Nombre")]
        public async Task<ActionResult<List<Cliente>>> FiltarPorNombre(string nombre)
        {
            var cliente = await _context.Clientes
                .Where(c => c.Nombre == nombre)
                .ToListAsync();

            if (cliente == null || cliente.Count == 0)
            {
                return NotFound("No existen el dato que buscas");
            }
            else
            {
                return Ok(cliente);
            }
        }

        [HttpGet]
        [Route("Filtrar/Celular")]
        public async Task<ActionResult> FiltarPorCelular(string celular)
        {
            var cliente = await _context.Clientes.Where(c => c.Celular == celular).ToListAsync();

            if (cliente == null)
            {
                return NotFound("No existes el dato que buscas");
            }
            else
            {
                return Ok(cliente);
            }
        }

        [HttpGet]
        [Route("Filtrar/Correo")]
        public async Task<ActionResult> FiltarPorCorreo(string correo)
        {
            var cliente = await _context.Clientes.Where(c => c.Correo == correo).ToListAsync(); ;

            if (cliente == null)
            {
                return NotFound("No existes el dato que buscas");
            }
            else
            {
                return Ok(cliente);
            }
        }


        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar(int id, Cliente cliente)
        {
            var clienteExistente = await _context.Clientes.FindAsync(id);

            if (clienteExistente == null)
            {
                return NotFound("No existes el dato que buscas");
            }
            else
            {
                clienteExistente!.Nombre = cliente.Nombre;
                clienteExistente!.Celular = cliente.Celular;
                clienteExistente!.Correo = cliente.Correo;

                await _context.SaveChangesAsync();

                return Ok("Se actualizo exitosamente");
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var clienteEliminar = await _context.Clientes.FindAsync(id);

            if (clienteEliminar == null)
            {
                return NotFound("No existes el dato que buscas");
            }
            else
            {
                _context.Clientes.Remove(clienteEliminar!);

                await _context.SaveChangesAsync();

                return Ok("Se elimino exitosamente");
            }
        }
    }
}
