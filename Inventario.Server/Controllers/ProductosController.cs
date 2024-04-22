using Inventarios.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly MiDbContext _context;

        public ProductosController(MiDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();

            return Ok("Se guardo exitosamente");
        }

        [HttpGet]
        [Route("Consultar")]
        public async Task<ActionResult<IEnumerable<Producto>>> Consultar()
        {
            var productos = await _context.Productos
                        .Include(p => p.Categoria)
                        .Select(p => new {
                            p.Id,
                            p.Nombre,
                            p.Descripcion,
                            p.IdCategoria,
                            NombreCategoria = p.Categoria.Nombre
                        })
                        .ToListAsync();

            if (productos == null || productos.Count == 0)
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                return Ok(productos);
            }
        }

        [HttpGet]
        [Route("Filtrar/Id")]
        public async Task<ActionResult> FiltarPorId(int id)
        {
            Producto producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound("No existes el dato que buscas");
            }
            else
            {
                return Ok(producto);
            }
        }

        [HttpGet]
        [Route("Filtrar/Nombre")]
        public async Task<ActionResult<List<Producto>>> FiltarPorNombre(string nombre)
        {
            var productos = await _context.Productos
                .Where(p => p.Nombre == nombre)
                .ToListAsync();

            if (productos == null || productos.Count == 0)
            {
                return NotFound("No existen el dato que buscas");
            }
            else
            {
                return Ok(productos);
            }
        }

        [HttpGet]
        [Route("Filtrar/IdCategoria")]
        public async Task<ActionResult<List<Producto>>> FiltarPorIdCategoria(int idCategoria)
        {
            var productos = await _context.Productos
                .Where(p => p.IdCategoria == idCategoria)
                .ToListAsync();

            if (productos == null || productos.Count == 0)
            {
                return NotFound("No se encontraron notas para la categoría especificada.");
            }
            else
            {

                return Ok(productos);
            }
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar(int id, Producto producto)
        {
            var productoExistente = await _context.Productos.FindAsync(id);

            if (productoExistente == null)
            {
                return NotFound("No existes el dato que buscas");
            }
            else
            {
                productoExistente!.Nombre = producto.Nombre;
                productoExistente!.Descripcion = producto.Descripcion;
                productoExistente!.IdCategoria = producto.IdCategoria;

                await _context.SaveChangesAsync();

                return Ok("Se actualizo exitosamente");
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var productoEliminar = await _context.Productos.FindAsync(id);

            if (productoEliminar == null)
            {
                return NotFound("No existes el dato que buscas");
            }
            else
            {
                _context.Productos.Remove(productoEliminar!);

                await _context.SaveChangesAsync();

                return Ok("Se elimino exitosamente");
            }
        }
    }
}
