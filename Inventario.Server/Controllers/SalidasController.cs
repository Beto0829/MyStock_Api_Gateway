using Inventarios.Server.Models;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<Salida>> AgregarSalida(Salida salida)
        {
            try
            {
                DateTime fechaHoraActual = DateTime.Now;

                salida.FechaFactura = new DateTime(fechaHoraActual.Year, fechaHoraActual.Month, fechaHoraActual.Day, fechaHoraActual.Hour, fechaHoraActual.Minute, 0);

                _context.Salidas.Add(salida);
                await _context.SaveChangesAsync();

                int nuevaSalidaId = salida.Id;

                if (salida.ProductoSalidas != null && salida.ProductoSalidas.Any())
                {
                    foreach (var productoSalida in salida.ProductoSalidas)
                    {
                        productoSalida.IdSalida = nuevaSalidaId;
                        _context.ProductoSalidas.Add(productoSalida);
                    }
                    await _context.SaveChangesAsync();
                }

                return Ok(salida);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al agregar la salida: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("ConsultarTodo")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarSalidas()
        {
            try
            {
                var salidas = await _context.Salidas
                    .Include(s => s.ProductoSalidas)
                        .ThenInclude(ps => ps.Categoria) // Incluir la información de la categoría del producto
                    .Include(s => s.ProductoSalidas)
                        .ThenInclude(ps => ps.Producto) // Incluir la información del producto
                    .Include(s => s.Cliente) // Incluir la información del cliente
                    .ToListAsync();

                if (salidas == null || salidas.Count == 0)
                {
                    return NotFound();
                }

                // Proyectar los resultados a un objeto anónimo con la información deseada
                var resultados = salidas.Select(s => new
                {
                    s.Id,
                    s.FechaFactura,
                    ClienteId = s.IdCliente, // Mantener el ID del cliente
                    ClienteNombre = s.Cliente != null ? s.Cliente.Nombre : "Sin cliente",
                    s.CantidadProductos,
                    s.TotalPagarConDescuento,
                    s.TotalPagarSinDescuento,
                    s.TotalDescuento,
                    ProductoSalidas = s.ProductoSalidas.Select(ps => new
                    {
                        ps.Id,
                        ps.IdSalida,
                        CategoriaId = ps.IdCategoria, // Mantener el ID de la categoría
                        CategoriaNombre = ps.Categoria != null ? ps.Categoria.Nombre : "Sin categoría",
                        ProductoId = ps.IdProducto, // Mantener el ID del producto
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
    }
}
