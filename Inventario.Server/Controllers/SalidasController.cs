using Inventarios.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidasController : ControllerBase
    {
        private readonly MiDbContext _context;

        public SalidasController(MiDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> AgregarSalida(Salida salida)
        {

            DateTime fechaHoraActual = DateTime.Now;

            salida.FechaFactura = new DateTime(fechaHoraActual.Year, fechaHoraActual.Month, fechaHoraActual.Day, fechaHoraActual.Hour, fechaHoraActual.Minute, 0);

            _context.Salidas.Add(salida);

            int nuevaSalidaId = salida.Id;

            if (salida.ProductoSalidas != null && salida.ProductoSalidas.Any())
            {
                foreach (var productoSalida in salida.ProductoSalidas)
                {
                    productoSalida.IdSalida = nuevaSalidaId;
                    _context.ProductoSalidas.Add(productoSalida);
                }
            }

            await _context.SaveChangesAsync();

            return Ok("Se guardó exitosamente");
            //return Ok(salida);
        }

        private async Task ProcedimientoDescontarExistencia(int idSalida)
        {
            var productosSalida = await _context.ProductoSalidas
                .Where(ps => ps.IdSalida == idSalida)
                .ToListAsync();

            foreach (var productoSalida in productosSalida)
            {
                var entrada = await _context.Entradas.FindAsync(productoSalida.IdEntrada);

                if (entrada != null)
                {
                    // Verificar si la cantidad del ProductoSalida es menor que la existencia actual de la entrada
                    if (productoSalida.Cantidad > entrada.ExistenciaActual)
                    {
                        // Si la cantidad es mayor, lanzar una excepción o manejar el error según sea necesario
                        throw new Exception($"La cantidad del producto ({productoSalida.Id}) es mayor que la existencia actual de la entrada ({entrada.Id})");
                    }

                    entrada.ExistenciaActual -= productoSalida.Cantidad;
                }
            }

            await _context.SaveChangesAsync();
        }


        [HttpPost]
        [Route("DescontarExistencia")]
        public async Task<IActionResult> DescontarExistencia(int idSalida)
        {
            var salidaExistente = await _context.Salidas.AnyAsync(s => s.Id == idSalida);

            if (!salidaExistente)
            {
                return NotFound("No se encontró ninguna registro al cual descontar");
            }

            await ProcedimientoDescontarExistencia(idSalida);

            return Ok("Existencia descontada exitosamente");
        }

        [HttpGet]
        [Route("ConsultarTodo")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarSalidas()
        {
            try
            {
                var salidas = await _context.Salidas
                    .Include(s => s.ProductoSalidas)
                        .ThenInclude(ps => ps.Categoria)
                    .Include(s => s.ProductoSalidas)
                        .ThenInclude(ps => ps.Producto)
                    .Include(s => s.Cliente)
                    .ToListAsync();

                if (salidas == null || salidas.Count == 0)
                {
                    return NotFound();
                }

                var resultados = salidas.Select(s => new
                {
                    s.Id,
                    s.FechaFactura,
                    ClienteId = s.IdCliente,
                    ClienteNombre = s.Cliente != null ? s.Cliente.Nombre : "Sin cliente",
                    s.CantidadProductos,
                    s.TotalPagarConDescuento,
                    s.TotalPagarSinDescuento,
                    s.TotalDescuento,
                    ProductoSalidas = s.ProductoSalidas.Select(ps => new
                    {
                        ps.Id,
                        ps.IdSalida,
                        CategoriaId = ps.IdCategoria, 
                        CategoriaNombre = ps.Categoria != null ? ps.Categoria.Nombre : "Sin categoria",
                        ProductoId = ps.IdProducto,
                        ProductoNombre = ps.Producto != null ? ps.Producto.Nombre : "Sin producto",
                        ps.Precio,
                        ps.Cantidad,
                        ps.Descuento,
                        ps.ValorDescuento,
                        ps.Total
                    }).ToList()
                }).ToList();

                return Ok(resultados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar las salidas: " + ex.Message);
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> EliminarSalida(int id)
        {
            var salida = await _context.Salidas.FindAsync(id);

            if (salida == null)
            {
                return NotFound("No se encuentra la factura que deseas eliminar");
            }

            var productoSalidas = _context.ProductoSalidas.Where(ps => ps.IdSalida == id);
            _context.ProductoSalidas.RemoveRange(productoSalidas);

            _context.Salidas.Remove(salida);

            await _context.SaveChangesAsync();

            return Ok("Se elimino la salida exitosamente");
        }

    }
}
