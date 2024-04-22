using Inventarios.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly MiDbContext _context;

        public DashboardController(MiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("TarjetaClientes")]
        public async Task<ActionResult<int>> ConsultarClientes()
        {
            var clientes = await _context.Clientes.ToListAsync();
            int tarjetaClientes = clientes.Count;

            if (clientes == null || tarjetaClientes == 0)
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                return Ok(tarjetaClientes);
            }
        }

        [HttpGet]
        [Route("TarjetaProveedors")]
        public async Task<ActionResult<int>> ConsultarProveedors()
        {
            var proveedors = await _context.Proveedors.ToListAsync();
            int tarjetaProveedors = proveedors.Count;

            if (proveedors == null || tarjetaProveedors == 0)
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                return Ok(tarjetaProveedors);
            }
        }

        [HttpGet]
        [Route("TarjetaCategorias")]
        public async Task<ActionResult<int>> ConsultarCategorias()
        {
            var categorias = await _context.Categorias.ToListAsync();
            int tarjetaCategorias = categorias.Count;

            if (categorias == null || tarjetaCategorias == 0)
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                return Ok(tarjetaCategorias);
            }
        }


        [HttpGet]
        [Route("TarjetaProductos")]
        public async Task<ActionResult<int>> ConsultarProductos()
        {
            var productos = await _context.Productos.ToListAsync();
            int tarjetaProducto = productos.Count;

            if (productos == null || tarjetaProducto == 0)
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                return Ok(tarjetaProducto);
            }
        }

        [HttpGet]
        [Route("TarjetaFacturas")]
        public async Task<ActionResult<int>> ConsultarFacturas()
        {
            var facturas = await _context.Salidas.ToListAsync();
            int tarjetafactura = facturas.Count;

            if (facturas == null || tarjetafactura == 0)
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                return Ok(tarjetafactura);
            }
        }

        [HttpGet]
        [Route("TarjetaExistenciasTotales")]
        public async Task<ActionResult<int>> ConsultarExistenciasTotales()
        {
            var existencias = await _context.Entradas.ToListAsync();

            if (existencias == null || !existencias.Any())
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                int sumaExistenciaInicial = existencias.Sum(e => e.ExistenciaInicial);
                return Ok(sumaExistenciaInicial);
            }
        }

        [HttpGet]
        [Route("TarjetaExistenciasVendidas")]
        public async Task<ActionResult<int>> ConsultarExistenciasVendidas()
        {
            var existencias = await _context.ProductoSalidas.ToListAsync();

            if (existencias == null || !existencias.Any())
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                int sumaExistenciaVendidas = existencias.Sum(e => e.Cantidad);
                return Ok(sumaExistenciaVendidas);
            }
        }

        [HttpGet]
        [Route("TarjetaExistenciasActuales")]
        public async Task<ActionResult<int>> ConsultarExistenciasActuales()
        {
            var existencias = await _context.Entradas.ToListAsync();

            if (existencias == null || !existencias.Any())
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                int sumaExistenciaActuales = existencias.Sum(e => e.ExistenciaActual);
                return Ok(sumaExistenciaActuales);
            }
        }

        [HttpGet]
        [Route("TarjetaImporteVendido")]
        public async Task<ActionResult<decimal>> ConsultarImporteVendido()
        {
            var importes = await _context.Salidas.ToListAsync();

            if (importes == null || !importes.Any())
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                decimal sumaImporteVendido = importes.Sum(e => e.TotalPagarSinDescuento);
                return Ok(sumaImporteVendido);
            }
        }

        [HttpGet]
        [Route("TarjetaImportePagado")]
        public async Task<ActionResult<decimal>> ConsultarImportePagado()
        {
            var importes = await _context.Salidas.ToListAsync();

            if (importes == null || !importes.Any())
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                decimal sumaImporteVendido = importes.Sum(e => e.TotalPagarConDescuento);
                return Ok(sumaImporteVendido);
            }
        }

        [HttpGet]
        [Route("TarjetaBeneficioBruto")]
        public async Task<ActionResult<decimal>> ConsultarBeneficioBruto()
        {
            try
            {
                var salidas = await _context.Salidas
                    .Include(s => s.ProductoSalidas)
                    .ToListAsync();

                if (salidas == null || !salidas.Any())
                {
                    return NotFound("No existen los datos que buscas");
                }

                decimal ingresosTotales = salidas.Sum(s => s.TotalPagarConDescuento);

                decimal costoBienesVendidos = await _context.Entradas.SumAsync(e => e.PrecioCompra * (e.ExistenciaInicial - e.ExistenciaActual));

                decimal beneficioBruto = ingresosTotales - costoBienesVendidos;

                return Ok(beneficioBruto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al calcular el beneficio bruto: " + ex.Message);
            }
        }


        [HttpGet]
        [Route("TarjetaBeneficioNeto")]
        public async Task<ActionResult<decimal>> ConsultarBeneficioNeto()
        {
            try
            {
                var salidas = await _context.Salidas
                    .Include(s => s.ProductoSalidas)
                    .ToListAsync();

                if (salidas == null || !salidas.Any())
                {
                    return NotFound("No existen los datos que buscas");
                }

                decimal ingresosTotales = salidas.Sum(s => s.TotalPagarConDescuento);

                decimal costosTotales = await _context.Entradas.SumAsync(e => e.PrecioCompra * (e.ExistenciaInicial - e.ExistenciaActual));

                decimal beneficioNeto = ingresosTotales - costosTotales;

                return Ok(beneficioNeto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al calcular el beneficio neto: " + ex.Message);
            }
        }
    }
}
