using Inventario.Server.Models;
using Inventarios.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly MiDbContext _context;

        public CategoriasController(MiDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            return Ok("Se guardo exitosamente");
        }

        [HttpGet]
        [Route("Consultar")]
        public async Task<ActionResult<IEnumerable<Categoria>>> Consultar()
        {
            var categorias = await _context.Categorias.ToListAsync();

            if (categorias == null || categorias.Count == 0)
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                return Ok(categorias);
            }
        }

        [HttpGet]
        [Route("Filtrar/Id")]
        public async Task<ActionResult> FiltarPorId(int id)
        {
            Categoria categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound("No existes el dato que buscas");
            }

            return Ok(categoria);
        }

        [HttpGet]
        [Route("Filtrar/Nombre")]
        public async Task<ActionResult<List<Categoria>>> FiltarPorNombre(string nombre)
        {
            var categorias = await _context.Categorias
                .Where(c => c.Nombre == nombre)
                .ToListAsync();

            if (categorias == null || categorias.Count == 0)
            {
                return NotFound("No existen el dato que buscas");
            }

            return Ok(categorias);
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar(int id, Categoria categoria)
        {
            var categoriaExistente = await _context.Categorias.FindAsync(id);

            if (categoriaExistente == null)
            {
                return NotFound("No existes el dato que buscas");
            }
            else
            {
                categoriaExistente!.Nombre = categoria.Nombre;

                await _context.SaveChangesAsync();

                return Ok("Se actualizo exitosamente");
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var categoriaEliminar = await _context.Categorias.FindAsync(id);

            if (categoriaEliminar == null || categoriaEliminar.Id == 1)
            {
                return NotFound("No existe la categoria que buscas o la categoria que desea eliminar es la predeterminada");
            }

            var productosEnCategoria = await _context.Productos
                                                .Where(p => p.IdCategoria == id)
                                                .ToListAsync();

            if (productosEnCategoria.Any())
            {
                var categoriaNueva = await _context.Categorias.FindAsync(1);

                if (categoriaNueva != null)
                {
                    foreach (var producto in productosEnCategoria)
                    {
                        producto.IdCategoria = categoriaNueva.Id;
                    }
                }
            }

            _context.Categorias.Remove(categoriaEliminar);

            await _context.SaveChangesAsync();

            return Ok("Se elimino exitosamente la categoria");
        }
    }
}
