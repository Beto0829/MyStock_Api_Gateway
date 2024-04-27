using Inventarios.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntradasController : ControllerBase
    {
        private readonly MiDbContext _context;

        public EntradasController(MiDbContext context)
        {
            _context = context;
        }

        //[HttpPost]
        //[Route("Agregarviejo")]
        //public async Task<IActionResult> Agregar(Entrada entrada)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    entrada.ExistenciaActual = entrada.ExistenciaInicial;
        //    entrada.FechaEntrada = DateTime.Now;

        //    await _context.Entradas.AddAsync(entrada);
        //    await _context.SaveChangesAsync();

        //    return Ok("Se guardó exitosamente");
        //}

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar(EntradaDTO entradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entrada = new Entrada
            {
                IdCategoria = entradaDTO.IdCategoria,
                IdProducto = entradaDTO.IdProducto,
                IdProveedor = entradaDTO.IdProveedor,
                PrecioCompra = entradaDTO.PrecioCompra,
                PrecioVenta = entradaDTO.PrecioVenta,
                ExistenciaInicial = entradaDTO.ExistenciaInicial,
                Nota = entradaDTO.Nota,
                ExistenciaActual = entradaDTO.ExistenciaInicial,
                FechaEntrada = DateTime.Now
            };

            await _context.Entradas.AddAsync(entrada);
            await _context.SaveChangesAsync();

            return Ok("Se guardó exitosamente");
        }


        [HttpGet]
        [Route("Consultar")]
        public async Task<ActionResult<IEnumerable<Entrada>>> Consultar()
        {
            var entradas = await _context.Entradas
                        .Include(c => c.Categoria)
                        .Include(c => c.Producto)
                        .Include(c => c.proveedor)
                        .Select(p => new {
                            p.Id,
                            p.IdCategoria,
                            NombreCategoria = p.Categoria.Nombre,
                            p.IdProducto,
                            NombreProducto = p.Producto.Nombre,
                            p.IdProveedor,
                            NombreProveedor = p.proveedor.Nombre,
                            p.ExistenciaInicial,
                            p.ExistenciaActual,
                            p.PrecioCompra,
                            p.PrecioVenta,
                            p.Nota,
                            FechaEntrada = p.FechaEntrada.ToString("dd/MM/yy HH:mm")
                            //p.FechaEntrada
                        })
                        .ToListAsync();

            if (entradas == null || entradas.Count == 0)
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                return Ok(entradas);
            }
        }

        [HttpGet]
        [Route("Filtrar/IdCategoria")]
        public async Task<ActionResult<List<Entrada>>> FiltarPorIdCategoria(int idCategoria)
        {
            var entradas = await _context.Entradas
                .Where(p => p.IdCategoria == idCategoria)
                        .Include(c => c.Categoria)
                        .Include(c => c.Producto)
                        .Include(c => c.proveedor)
                        .Select(p => new {
                            p.Id,
                            p.IdCategoria,
                            NombreCategoria = p.Categoria.Nombre,
                            p.IdProducto,
                            NombreProducto = p.Producto.Nombre,
                            p.IdProveedor,
                            NombreProveedor = p.proveedor.Nombre,
                            p.ExistenciaInicial,
                            p.ExistenciaActual,
                            p.PrecioCompra,
                            p.PrecioVenta,
                            p.Nota,
                            FechaEntrada = p.FechaEntrada.ToString("dd/MM/yy HH:mm")
                        })
                .ToListAsync();

            if (entradas == null || entradas.Count == 0)
            {
                return NotFound("No se encontraron notas para la categoría especificada.");
            }
            else
            {

                return Ok(entradas);
            }
        }

        [HttpPut]
        [Route("SumarExistencias")]
        public async Task<IActionResult> SumarExistencias(int id, int nuevaExistencia)
        {
            Entrada entradaExistente = await _context.Entradas.FirstOrDefaultAsync(c => c.Id == id);

            if (entradaExistente == null)
            {
                return NotFound("No se encontro la entrada.");
            }
            else
            {
                entradaExistente.ExistenciaActual += nuevaExistencia;
                await _context.SaveChangesAsync();

                return Ok("Se sumo una cantidad de " + nuevaExistencia + " a la entrada: " + entradaExistente.Id + ", exitosamente.");
            }
        }

        [HttpPut]
        [Route("RestarExistencia")]
        public async Task<IActionResult> RestarExistencia(int id, int quitarExistencia)
        {
            Entrada entradaExistente = await _context.Entradas.FirstOrDefaultAsync(c => c.Id == id);

            if (entradaExistente == null)
            {
                return NotFound("No se encontro la entrada.");
            }
            else
            {
                entradaExistente.ExistenciaActual -= quitarExistencia;
                await _context.SaveChangesAsync();

                return Ok("Se quito una cantidad de " + quitarExistencia + " a la entrada: " + entradaExistente.Id + ", exitosamente.");
            }
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar(int id, EntradaDTO entradaDTO)
        {
            var entradaExistente = await _context.Entradas.FindAsync(id);

            if (entradaDTO == null)
            {
                return NotFound("No existes el dato que buscas");
            }
            else
            {
                entradaExistente!.IdCategoria = entradaDTO.IdCategoria;
                entradaExistente!.IdProducto = entradaDTO.IdProducto;
                entradaExistente!.IdProveedor = entradaDTO.IdProveedor;
                entradaExistente!.PrecioCompra = entradaDTO.PrecioCompra;
                entradaExistente!.PrecioVenta = entradaDTO.PrecioVenta;
                entradaExistente!.ExistenciaInicial = entradaExistente.ExistenciaInicial;
                entradaExistente!.Nota = entradaDTO.Nota;

                await _context.SaveChangesAsync();

                return Ok("Se actualizo exitosamente");
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var entradaEliminar = await _context.Entradas.FindAsync(id);

            if (entradaEliminar == null)
            {
                return NotFound("No existes el dato que buscas");
            }
            else
            {
                _context.Entradas.Remove(entradaEliminar!);

                await _context.SaveChangesAsync();

                return Ok("Se elimino exitosamente");
            }
        }
    }
}
