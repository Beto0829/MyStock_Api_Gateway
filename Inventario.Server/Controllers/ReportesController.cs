﻿using Inventarios.Server.Models;
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

        //VENTAS

        [HttpGet]
        [Route("VentasEntreDosFechas")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarVentasEntreDosFechas(DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                fechaInicio = fechaInicio.Date;
                fechaFinal = fechaFinal.Date.AddDays(1).AddTicks(-1);

                var salidas = await _context.Salidas
                    .Include(s => s.ProductoSalidas)
                        .ThenInclude(ps => ps.Categoria)
                    .Include(s => s.ProductoSalidas)
                        .ThenInclude(ps => ps.Producto)
                    .Include(s => s.Cliente)
                    .Where(s => s.FechaFactura >= fechaInicio && s.FechaFactura <= fechaFinal)
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar las salidas entre las fechas: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("VentasDiaActual")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarVentasActuales()
        {
            try
            {
                DateTime fechaActual = DateTime.Now.Date;

                var salidas = await _context.Salidas
                    .Include(s => s.ProductoSalidas)
                        .ThenInclude(ps => ps.Categoria)
                    .Include(s => s.ProductoSalidas)
                        .ThenInclude(ps => ps.Producto)
                    .Include(s => s.Cliente)
                    .Where(s => s.FechaFactura.Date == fechaActual)
                    .ToListAsync();

                if (salidas == null || salidas.Count == 0)
                {
                    return Ok("No existen facturaciones registradas para el dia actual.");
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar las salidas del día actual: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("ClientesSalidasDiaActual")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarClientesSalidasActuales()
        {
            try
            {
                DateTime fechaActual = DateTime.Now.Date;

                var salidas = await _context.Salidas
                    .Include(s => s.Cliente)
                    .Where(s => s.FechaFactura.Date == fechaActual)
                    .ToListAsync();

                if (salidas == null || salidas.Count == 0)
                {
                    return Ok("No existen facturaciones registradas para el dia actual.");
                }

                var resultados = salidas.Select(s => new
                {
                    ClienteId = s.IdCliente,
                    ClienteNombre = s.Cliente != null ? s.Cliente.Nombre : "Sin cliente",
                    FechaFactura = s.FechaFactura
                }).ToList();

                return Ok(resultados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar los clientes de las salidas del dia actual: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("ClientesSalidasEntreFechas")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarClientesSalidasEntreFechas(DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                fechaInicio = fechaInicio.Date;
                fechaFinal = fechaFinal.Date.AddDays(1).AddTicks(-1);

                var salidas = await _context.Salidas
                    .Include(s => s.Cliente)
                    .Where(s => s.FechaFactura >= fechaInicio && s.FechaFactura <= fechaFinal)
                    .ToListAsync();

                if (salidas == null || salidas.Count == 0)
                {
                    return Ok("No existen facturaciones registradas para las fechas especificadas.");
                }

                var resultados = salidas.Select(s => new
                {
                    ClienteId = s.IdCliente,
                    ClienteNombre = s.Cliente != null ? s.Cliente.Nombre : "Sin cliente",
                    FechaFactura = s.FechaFactura
                }).ToList();

                return Ok(resultados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar los clientes de las salidas entre las fechas especificadas: " + ex.Message);
            }
        }

        //FIN
        /////////////////////
        //COMPRAS
        [HttpGet]
        [Route("ComprasDiaActual")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarComprasActuales()
        {
            try
            {
                DateTime fechaActual = DateTime.Now.Date;

                var entradas = await _context.Entradas
                         .Include(c => c.Categoria)
                         .Include(c => c.Producto)
                         .Include(c => c.Proveedor)
                         .Where(e => e.FechaEntrada.Date == fechaActual)
                         .ToListAsync();

                if (entradas == null || entradas.Count == 0)
                {
                    return Ok("No existen compras registradas para las fechas digitadas.");
                }

                var resultados = entradas.Select(p => new
                {
                    p.Id,
                    p.IdCategoria,
                    NombreCategoria = p.Categoria.Nombre,
                    p.IdProducto,
                    NombreProducto = p.Producto.Nombre,
                    p.IdProveedor,
                    NombreProveedor = p.Proveedor.Nombre,
                    p.ExistenciaInicial,
                    p.ExistenciaActual,
                    p.PrecioCompra,
                    p.PrecioVenta,
                    p.Nota,
                    FechaEntrada = p.FechaEntrada.ToString("dd/MM/yy HH:mm"),
                    p.Estado
                }).ToList();

                return Ok(resultados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar las compras del día actual: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("ComprasEntreFechas")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarComprasEntreFechas(DateTime fechaInicio, DateTime fechaFinal)
        {
            try 
            {
                fechaInicio = fechaInicio.Date;
                fechaFinal = fechaFinal.Date.AddDays(1).AddTicks(-1);

                var entradas = await _context.Entradas
                           .Include(c => c.Categoria)
                           .Include(c => c.Producto)
                           .Include(c => c.Proveedor)
                           .Where(e => e.FechaEntrada >= fechaInicio && e.FechaEntrada <= fechaFinal)
                           .ToListAsync();

                if (entradas == null || entradas.Count == 0)
                {
                    return Ok("No existen compras registradas para las fechas digitadas.");
                }

                var resultados = entradas.Select(p => new
                {
                    p.Id,
                    p.IdCategoria,
                    NombreCategoria = p.Categoria.Nombre,
                    p.IdProducto,
                    NombreProducto = p.Producto.Nombre,
                    p.IdProveedor,
                    NombreProveedor = p.Proveedor.Nombre,
                    p.ExistenciaInicial,
                    p.ExistenciaActual,
                    p.PrecioCompra,
                    p.PrecioVenta,
                    p.Nota,
                    FechaEntrada = p.FechaEntrada.ToString("dd/MM/yy HH:mm"),
                    p.Estado
                }).ToList();

                return Ok(resultados);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar las compras entre las fechas: " + ex.Message);
            }

        }

        [HttpGet]
        [Route("ProductosExistenciaNula")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarProductosExistenciaNula()
        {
            try
            {
                var productosBajoStock = await _context.Entradas
                    .Where(e => e.ExistenciaActual == 0)
                    .Include(e => e.Categoria)
                    .Include(e => e.Producto)
                    .Include(e => e.Proveedor)
                    .Select(e => new
                    {
                        e.Id,
                        e.IdCategoria,
                        CategoriaNombre = e.Categoria.Nombre,
                        e.IdProducto,
                        ProductoNombre = e.Producto.Nombre,
                        e.IdProveedor,
                        ProveedorNombre = e.Proveedor.Nombre,
                        e.ExistenciaInicial,
                        e.ExistenciaActual,
                        e.PrecioCompra,
                        e.PrecioVenta,
                        e.Nota,
                        FechaEntrada = e.FechaEntrada.ToString("dd/MM/yy HH:mm"),
                        e.Estado
                    })
                    .ToListAsync();

                if (productosBajoStock == null || productosBajoStock.Count == 0)
                {
                    return Ok("No hay productos con existencias en cero");
                }

                return Ok(productosBajoStock);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar productos con existencias en cero: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("ProductosBajaExistencia")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarProductosBajaExistencia()
        {
            try
            {
                var productosBajoStock = await _context.Entradas
                    .Where(e => ((double)e.ExistenciaActual / e.ExistenciaInicial) < 0.2)
                    .Include(e => e.Categoria)
                    .Include(e => e.Producto)
                    .Include(e => e.Proveedor)
                    .Select(e => new
                    {
                        e.Id,
                        e.IdCategoria,
                        CategoriaNombre = e.Categoria.Nombre,
                        e.IdProducto,
                        ProductoNombre = e.Producto.Nombre,
                        e.IdProveedor,
                        ProveedorNombre = e.Proveedor.Nombre,
                        e.ExistenciaInicial,
                        e.ExistenciaActual,
                        e.PrecioCompra,
                        e.PrecioVenta,
                        e.Nota,
                        FechaEntrada = e.FechaEntrada.ToString("dd/MM/yy HH:mm"),
                        e.Estado
                    })
                    .ToListAsync();

                if (productosBajoStock == null || productosBajoStock.Count == 0)
                {
                    return Ok("No hay productos con existencias por debajo del 20% de su existencia inicial.");
                }

                return Ok(productosBajoStock);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar productos con existencias por debajo del 20%: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("ProveedoresEntradasDiaActual")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarProveedoresEntradasDiaActual()
        {
            try
            {
                DateTime fechaActual = DateTime.Now.Date;

                var proveedoresEntradas = await _context.Entradas
                    .Where(e => e.FechaEntrada.Date == fechaActual)
                    .Include(e => e.Proveedor)
                    .Select(e => new
                    {
                        ProveedorId = e.IdProveedor,
                        NombreProveedor = e.Proveedor.Nombre,
                        FechaEntrada = e.FechaEntrada
                    })
                    .ToListAsync();

                return Ok(proveedoresEntradas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar los proveedores de las entradas: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("ProveedoresEntradasEntreFechas")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarProveedoresEntradasEntreFechas(DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                fechaInicio = fechaInicio.Date;
                fechaFinal = fechaFinal.Date.AddDays(1).AddTicks(-1);

                var entradas = await _context.Entradas
                    .Include(c => c.Categoria)
                    .Include(c => c.Producto)
                    .Include(c => c.Proveedor)
                    .Where(e => e.FechaEntrada >= fechaInicio && e.FechaEntrada <= fechaFinal)
                    .ToListAsync();

                if (entradas == null || entradas.Count == 0)
                {
                    return Ok("No existen compras registradas para las fechas digitadas.");
                }

                var resultados = entradas.Select(p => new
                {
                    p.IdProveedor,
                    NombreProveedor = p.Proveedor.Nombre,
                    FechaEntrada = p.FechaEntrada.ToString("dd/MM/yy HH:mm"),
                }).ToList();

                return Ok(resultados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar las compras entre las fechas: " + ex.Message);
            }
        }
        //FIN
        ////////////////////
        //PRODUCTOSVEDIDOS
        [HttpGet]
        [Route("ProdcutosVendidosEntreFechas")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultaProductosVendidosEntreFechas(DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                fechaInicio = fechaInicio.Date;
                fechaFinal = fechaFinal.Date.AddDays(1).AddTicks(-1);

                var productoSalidas = await _context.ProductoSalidas
                    .Include(ps => ps.Categoria)
                    .Include(ps => ps.Producto)
                    .Where(ps => ps.Salida.FechaFactura >= fechaInicio && ps.Salida.FechaFactura <= fechaFinal)
                    .Select(ps => new
                    {
                        ps.Id,
                        SalidaId = ps.IdSalida,
                        ps.Salida.FechaFactura,
                        ProductoId = ps.IdProducto,
                        ProductoNombre = ps.Producto != null ? ps.Producto.Nombre : "Sin producto",
                        CategoriaId = ps.IdCategoria,
                        CategoriaNombre = ps.Categoria != null ? ps.Categoria.Nombre : "Sin categoria",
                        ps.Precio,
                        ps.Cantidad,
                        ps.Descuento,
                        ps.ValorDescuento,
                        ps.Total
                    })
                    .ToListAsync();
                if (productoSalidas == null || productoSalidas.Count == 0)
                {
                    return NotFound();
                }

                return Ok(productoSalidas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar los productos vendidos entre las fechas: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("ProductosSalidaDiaActual")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarProductosSalidaDiaActual()
        {
            try
            {
                DateTime fechaActual = DateTime.Now.Date;

                var productosSalida = await _context.ProductoSalidas
                    .Include(ps => ps.Salida)
                    .Include(ps => ps.Categoria)
                    .Include(ps => ps.Producto)
                    .Where(ps => ps.Salida.FechaFactura.Date == fechaActual)
                    .Select(ps => new
                    {
                        ps.Id,
                        ps.IdSalida,
                        ps.IdCategoria,
                        CategoriaNombre = ps.Categoria.Nombre,
                        ps.IdProducto,
                        ProductoNombre = ps.Producto.Nombre,
                        ps.Precio,
                        ps.Cantidad,
                        ps.Descuento,
                        ps.ValorDescuento,
                        ps.Total
                    })
                    .ToListAsync();

                if (productosSalida == null || productosSalida.Count == 0)
                {
                    return Ok("No existen productos de salida registrados para el día actual.");
                }

                return Ok(productosSalida);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar los productos de salida del día actual: " + ex.Message);
            }
        }
        //FIN
        ///////////////////
        //NUEVOS
        [HttpGet("TopProductosMasVendidosEntreDosFechas")]
        public async Task<ActionResult<IEnumerable<TopProducto>>> ConsultarTopProductosMasVendidos(DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                fechaInicio = fechaInicio.Date;
                fechaFinal = fechaFinal.Date.AddDays(1).AddTicks(-1);

                var topProductos = await _context.ProductoSalidas
                    .Include(ps => ps.Producto)
                    .Where(ps => ps.Salida.FechaFactura >= fechaInicio && ps.Salida.FechaFactura <= fechaFinal)
                    .GroupBy(ps => ps.IdProducto)
                    .Select(g => new TopProducto
                    {
                        IdProducto = g.Key,
                        CantidadVentas = g.Count()
                    })
                    .OrderByDescending(tp => tp.CantidadVentas)
                    .Take(5)
                    .ToListAsync();

                int contadorTop = 1;

                foreach (var producto in topProductos)
                {
                    producto.Top = contadorTop++;

                    var infoProducto = await _context.Productos
                        .Where(p => p.Id == producto.IdProducto)
                        .FirstOrDefaultAsync();

                    if (infoProducto != null)
                    {
                        producto.NombreProducto = infoProducto.Nombre;
                    }
                }

                return topProductos;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar los productos más vendidos entre las fechas: " + ex.Message);
            }
        }

        [HttpGet("TopProductosMasVendidosDiaActual")]
        public async Task<ActionResult<IEnumerable<TopProducto>>> ConsultarTopProductosMasVendidosDiaActual()
        {
            try
            {
                DateTime fechaActual = DateTime.Now.Date;

                var topProductos = await _context.ProductoSalidas
                    .Include(ps => ps.Producto)
                    .Where(ps => ps.Salida.FechaFactura.Date == fechaActual)
                    .GroupBy(ps => ps.IdProducto)
                    .Select(g => new TopProducto
                    {
                        IdProducto = g.Key,
                        CantidadVentas = g.Count()
                    })
                    .OrderByDescending(tp => tp.CantidadVentas)
                    .Take(5)
                    .ToListAsync();

                int contadorTop = 1;

                foreach (var producto in topProductos)
                {
                    producto.Top = contadorTop++;

                    var infoProducto = await _context.Productos
                        .Where(p => p.Id == producto.IdProducto)
                        .FirstOrDefaultAsync();

                    if (infoProducto != null)
                    {
                        producto.NombreProducto = infoProducto.Nombre;
                    }
                }

                return topProductos;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar los productos más vendidos entre las fechas: " + ex.Message);
            }
        }

        [HttpGet("TopProductosMenosVendidosEntreFechas")]
        public async Task<ActionResult<IEnumerable<TopProducto>>> ConsultarTopProductosMenosVendidos(DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                fechaInicio = fechaInicio.Date;
                fechaFinal = fechaFinal.Date.AddDays(1).AddTicks(-1);

                var topProductos = await _context.ProductoSalidas
                    .Include(ps => ps.Producto)
                    .Where(ps => ps.Salida.FechaFactura >= fechaInicio && ps.Salida.FechaFactura <= fechaFinal)
                    .GroupBy(ps => ps.IdProducto)
                    .Select(g => new TopProducto
                    {
                        IdProducto = g.Key,
                        CantidadVentas = g.Count()
                    })
                    .OrderBy(tp => tp.CantidadVentas)
                    .Take(5)
                    .ToListAsync();

                int contadorTop = 1;

                foreach (var producto in topProductos)
                {
                    producto.Top = contadorTop++;

                    var infoProducto = await _context.Productos
                        .Where(p => p.Id == producto.IdProducto)
                        .FirstOrDefaultAsync();

                    if (infoProducto != null)
                    {
                        producto.NombreProducto = infoProducto.Nombre;
                    }
                }

                return topProductos;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar los productos menos vendidos entre las fechas: " + ex.Message);
            }
        }

        [HttpGet("TopProductosMenosVendidosDiaActual")]
        public async Task<ActionResult<IEnumerable<TopProducto>>> ConsultarTopProductosMenosVendidosDiaActual()
        {
            try
            {
                DateTime fechaActual = DateTime.Now.Date;

                var topProductos = await _context.ProductoSalidas
                    .Include(ps => ps.Producto)
                    .Where(ps => ps.Salida.FechaFactura.Date == fechaActual)
                    .GroupBy(ps => ps.IdProducto)
                    .Select(g => new TopProducto
                    {
                        IdProducto = g.Key,
                        CantidadVentas = g.Count()
                    })
                    .OrderBy(tp => tp.CantidadVentas)
                    .Take(5)
                    .ToListAsync();

                int contadorTop = 1;

                foreach (var producto in topProductos)
                {
                    producto.Top = contadorTop++;

                    var infoProducto = await _context.Productos
                        .Where(p => p.Id == producto.IdProducto)
                        .FirstOrDefaultAsync();

                    if (infoProducto != null)
                    {
                        producto.NombreProducto = infoProducto.Nombre;
                    }
                }

                return topProductos;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar los productos menos vendidos entre las fechas: " + ex.Message);
            }
        }
        //FIN
        ///////////////////

        //OPCIONALES
        [HttpGet]
        [Route("ProductosSalidaDiaActualOpcional")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarProductosSalidaDiaActualOpcional(int? idCategoria = null, int? idProducto = null, int? idCliente = null)
        {
            try
            {
                DateTime fechaActual = DateTime.Now.Date;

                var query = _context.ProductoSalidas
                    .Include(ps => ps.Salida)
                    .Include(ps => ps.Categoria)
                    .Include(ps => ps.Producto)
                    .Where(ps => ps.Salida.FechaFactura.Date == fechaActual);

                if (idCategoria.HasValue)
                {
                    query = query.Where(ps => ps.IdCategoria == idCategoria.Value);
                }

                if (idProducto.HasValue)
                {
                    query = query.Where(ps => ps.IdProducto == idProducto.Value);
                }

                if (idCliente.HasValue)
                {
                    query = query.Where(ps => ps.Salida.IdCliente == idCliente.Value);
                }

                var resultados = query.Select(s => new
                {
                    s.Salida.Id,
                    s.Salida.FechaFactura,
                    ClienteId = s.Salida.IdCliente,
                    ClienteNombre = s.Salida.Cliente != null ? s.Salida.Cliente.Nombre : "Sin cliente",
                    s.Salida.CantidadProductos,
                    s.Salida.TotalPagarConDescuento,
                    s.Salida.TotalPagarSinDescuento,
                    s.Salida.TotalDescuento,
                    ProductoSalidas = s.Salida.ProductoSalidas.Select(ps => new
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

                if (resultados == null || resultados.Count == 0)
                {
                    return Ok("No existen productos de salida registrados para el día actual.");
                }

                return Ok(resultados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar los productos de salida del día actual: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("ComprasDiaActualOpcional")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarComprasActualesOpcional(int? idCategoria = null, int? idProducto = null, int? idProveedor = null)
        {
            try
            {
                DateTime fechaActual = DateTime.Now.Date;

                var query = _context.Entradas
                         .Include(e => e.Categoria)
                         .Include(e => e.Producto)
                         .Include(e => e.Proveedor)
                         .Where(e => e.FechaEntrada.Date == fechaActual);

                if (idCategoria.HasValue)
                {
                    query = query.Where(e => e.IdCategoria == idCategoria.Value);
                }

                if (idProducto.HasValue)
                {
                    query = query.Where(e => e.IdProducto == idProducto.Value);
                }

                if (idProveedor.HasValue)
                {
                    query = query.Where(e => e.IdProveedor == idProveedor.Value);
                }

                var entradas = query.Select(p => new
                {
                    p.Id,
                    p.IdCategoria,
                    NombreCategoria = p.Categoria.Nombre,
                    p.IdProducto,
                    NombreProducto = p.Producto.Nombre,
                    p.IdProveedor,
                    NombreProveedor = p.Proveedor.Nombre,
                    p.ExistenciaInicial,
                    p.ExistenciaActual,
                    p.PrecioCompra,
                    p.PrecioVenta,
                    p.Nota,
                    FechaEntrada = p.FechaEntrada.ToString("dd/MM/yy HH:mm"),
                    p.Estado
                }).ToList();

                if (entradas == null || entradas.Count == 0)
                {
                    return Ok("No existen compras registradas para las fechas digitadas.");
                }

                return Ok(entradas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar las compras del día actual: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("VentasDiaActualOpcional")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarVentasActualesOpcional(int? idCategoria = null, int? idProducto = null, int? idCliente = null)
        {
            try
            {
                DateTime fechaActual = DateTime.Now.Date;

                var query = _context.ProductoSalidas
                   .Include(ps => ps.Salida)
                   .Include(ps => ps.Categoria)
                   .Include(ps => ps.Producto)
                   .Where(ps => ps.Salida.FechaFactura.Date == fechaActual);

                if (idCategoria.HasValue)
                {
                    query = query.Where(e => e.IdCategoria == idCategoria.Value);
                }

                if (idProducto.HasValue)
                {
                    query = query.Where(e => e.IdProducto == idProducto.Value);
                }

                if (idCliente.HasValue)
                {
                    query = query.Where(e => e.Salida.IdCliente == idCliente.Value);
                }

                var resultados = query.Select(s => new
                {
                    s.Salida.Id,
                    s.Salida.FechaFactura,
                    ClienteId = s.Salida.IdCliente,
                    ClienteNombre = s.Salida.Cliente != null ? s.Salida.Cliente.Nombre : "Sin cliente",
                    s.Salida.CantidadProductos,
                    s.Salida.TotalPagarConDescuento,
                    s.Salida.TotalPagarSinDescuento,
                    s.Salida.TotalDescuento,
                    ProductoSalidas = s.Salida.ProductoSalidas.Select(ps => new
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

                if (resultados == null || resultados.Count == 0)
                {
                    return Ok("No existen facturaciones registradas para el dia actual.");
                }

                return Ok(resultados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar las salidas del día actual: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("ProductosVendidosEntreFechasOpcional")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultaProductosVendidosEntreFechasOpcional(DateTime fechaInicio, DateTime fechaFinal, int? idCategoria = null, int? idProducto = null, int? idCliente = null)
        {
            try
            {
                fechaInicio = fechaInicio.Date;
                fechaFinal = fechaFinal.Date.AddDays(1).AddTicks(-1);

                var query = _context.ProductoSalidas
                    .Include(ps => ps.Salida)
                    .Include(ps => ps.Categoria)
                    .Include(ps => ps.Producto)
                    .Where(ps => ps.Salida.FechaFactura >= fechaInicio && ps.Salida.FechaFactura <= fechaFinal);

                if (idCategoria.HasValue)
                {
                    query = query.Where(ps => ps.IdCategoria == idCategoria.Value);
                }

                if (idProducto.HasValue)
                {
                    query = query.Where(ps => ps.IdProducto == idProducto.Value);
                }

                if (idCliente.HasValue)
                {
                    query = query.Where(ps => ps.Salida.IdCliente == idCliente.Value);
                }

                var resultados = await query.Select(s => new
                {
                    s.Salida.Id,
                    s.Salida.FechaFactura,
                    ClienteId = s.Salida.IdCliente,
                    ClienteNombre = s.Salida.Cliente != null ? s.Salida.Cliente.Nombre : "Sin cliente",
                    s.Salida.CantidadProductos,
                    s.Salida.TotalPagarConDescuento,
                    s.Salida.TotalPagarSinDescuento,
                    s.Salida.TotalDescuento,
                    ProductoSalidas = s.Salida.ProductoSalidas.Select(ps => new
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
                }).ToListAsync();

                if (resultados == null || resultados.Count == 0)
                {
                    return Ok("No existen productos vendidos en el rango de fechas especificado.");
                }

                return Ok(resultados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar los productos vendidos entre las fechas: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("ComprasEntreFechasOpcional")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarComprasEntreFechasOpcional(DateTime fechaInicio, DateTime fechaFinal, int? idCategoria = null, int? idProducto = null, int? idProveedor = null)
        {
            try
            {
                fechaInicio = fechaInicio.Date;
                fechaFinal = fechaFinal.Date.AddDays(1).AddTicks(-1);

                var query = _context.Entradas
                    .Include(e => e.Categoria)
                    .Include(e => e.Producto)
                    .Include(e => e.Proveedor)
                    .Where(e => e.FechaEntrada >= fechaInicio && e.FechaEntrada <= fechaFinal);

                if (idCategoria.HasValue)
                {
                    query = query.Where(e => e.IdCategoria == idCategoria.Value);
                }

                if (idProducto.HasValue)
                {
                    query = query.Where(e => e.IdProducto == idProducto.Value);
                }

                if (idProveedor.HasValue)
                {
                    query = query.Where(e => e.IdProveedor == idProveedor.Value);
                }

                var entradas = await query.Select(p => new
                {
                    p.Id,
                    p.IdCategoria,
                    NombreCategoria = p.Categoria != null ? p.Categoria.Nombre : "Sin categoria",
                    p.IdProducto,
                    NombreProducto = p.Producto != null ? p.Producto.Nombre : "Sin producto",
                    p.IdProveedor,
                    NombreProveedor = p.Proveedor != null ? p.Proveedor.Nombre : "Sin proveedor",
                    p.ExistenciaInicial,
                    p.ExistenciaActual,
                    p.PrecioCompra,
                    p.PrecioVenta,
                    p.Nota,
                    FechaEntrada = p.FechaEntrada.ToString("dd/MM/yy HH:mm"),
                    p.Estado
                }).ToListAsync();

                if (entradas == null || entradas.Count == 0)
                {
                    return Ok("No existen compras registradas para el rango de fechas especificado.");
                }

                return Ok(entradas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar las compras entre las fechas especificadas: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("VentasEntreFechasOpcional")]
        public async Task<ActionResult<IEnumerable<object>>> ConsultarVentasEntreFechasOpcional(DateTime fechaInicio, DateTime fechaFinal, int? idCategoria = null, int? idProducto = null, int? idCliente = null)
        {
            try
            {
                fechaInicio = fechaInicio.Date;
                fechaFinal = fechaFinal.Date.AddDays(1).AddTicks(-1);

                var query = _context.ProductoSalidas
                    .Include(ps => ps.Salida)
                    .Include(ps => ps.Categoria)
                    .Include(ps => ps.Producto)
                    .Where(ps => ps.Salida.FechaFactura >= fechaInicio && ps.Salida.FechaFactura <= fechaFinal);

                if (idCategoria.HasValue)
                {
                    query = query.Where(ps => ps.IdCategoria == idCategoria.Value);
                }

                if (idProducto.HasValue)
                {
                    query = query.Where(ps => ps.IdProducto == idProducto.Value);
                }

                if (idCliente.HasValue)
                {
                    query = query.Where(ps => ps.Salida.IdCliente == idCliente.Value);
                }

                var resultados = await query.Select(s => new
                {
                    s.Salida.Id,
                    s.Salida.FechaFactura,
                    ClienteId = s.Salida.IdCliente,
                    ClienteNombre = s.Salida.Cliente != null ? s.Salida.Cliente.Nombre : "Sin cliente",
                    s.Salida.CantidadProductos,
                    s.Salida.TotalPagarConDescuento,
                    s.Salida.TotalPagarSinDescuento,
                    s.Salida.TotalDescuento,
                    ProductoSalidas = s.Salida.ProductoSalidas.Select(ps => new
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
                }).ToListAsync();

                if (resultados == null || resultados.Count == 0)
                {
                    return Ok("No existen facturaciones registradas para el rango de fechas especificado.");
                }

                return Ok(resultados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar las ventas entre las fechas especificadas: " + ex.Message);
            }
        }

        //FIN
        ///////////////////
    }
}
