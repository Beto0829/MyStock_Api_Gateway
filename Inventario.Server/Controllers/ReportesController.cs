using Inventarios.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly MiDbContext _context;

        public ReportesController(MiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("FacturasGeneral")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarFacturasGeneral(int idReporte)
        {
            try
            {
                var reporte = await _context.Reportes.FindAsync(idReporte);

                if (reporte == null)
                {
                    return NotFound("No existe el Reporte");
                }
                else if (reporte.Id == 1)
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
                else
                {
                    return NotFound("Error: No coincide el reporte que deseas hacer con el proceso a realizar");
                }              
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar las salidas: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("FacturasEntreDosFechas")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarFacturasEntreDosFechas(DateTime fechaInicio, DateTime fechaFinal)
        {
            return Ok();
        }
    }
}
