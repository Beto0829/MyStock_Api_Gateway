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
                FechaEntrada = DateTime.Now,
                Estado = "Activo"
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
                            FechaEntrada = p.FechaEntrada.ToString("dd/MM/yy HH:mm"),
                            p.Estado
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
                            FechaEntrada = p.FechaEntrada.ToString("dd/MM/yy HH:mm"),
                            p.Estado
                        })
                .ToListAsync();

            if (entradas == null || entradas.Count == 0)
            {
                return NotFound("No se encontraron existencias para la categoría especificada.");
            }
            else
            {

                return Ok(entradas);
            }
        }

        [HttpGet]
        [Route("Filtrar/IdProductos")]
        public async Task<ActionResult<List<Entrada>>> FiltarPorIdProductos(int idProductos)
        {
            var entradas = await _context.Entradas
                .Where(p => p.IdProducto == idProductos)
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
                            FechaEntrada = p.FechaEntrada.ToString("dd/MM/yy HH:mm"),
                            p.Estado
                        })
                .ToListAsync();

            if (entradas == null || entradas.Count == 0)
            {
                return NotFound("No se encontraron existencias para el productos especificado.");
            }
            else
            {

                return Ok(entradas);
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

        [HttpPut]
        [Route("ActualizarEstado")]
        public async Task<IActionResult> ActualizarEstadoEntradas()
        {
            var entradas = await _context.Entradas.ToListAsync();
            bool algunaEntradaActualizada = false;

            foreach (var entrada in entradas)
            {
                if (entrada.ExistenciaActual == 0 && entrada.Estado != "Inactivo")
                {
                    entrada.Estado = "Inactivo";
                    algunaEntradaActualizada = true;
                }
            }

            if (algunaEntradaActualizada)
            {
                await _context.SaveChangesAsync();
                return Ok("Se actualizaron las entradas sin existencias");
            }
            else
            {
                return Ok("NO se encontro entradas con existencias en cero");
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
