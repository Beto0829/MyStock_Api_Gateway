﻿using Inventarios.Server.Models;
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

        //Nuevas tarjetas

        [HttpGet]
        [Route("TarjetaIngresos")]
        public async Task<ActionResult<decimal>> ConsultarIngresos()
        {
            var salida = await _context.Salidas.ToListAsync();

            if (salida == null || !salida.Any())
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                decimal ingresos = salida.Sum(e => e.TotalPagarConDescuento);
                return Ok(ingresos);
            }
        }

        [HttpGet]
        [Route("TarjetaInversion")]
        public async Task<ActionResult<decimal>> ConsultarInversion()
        {
            var entrada = await _context.Entradas.ToListAsync();

            if (entrada == null || !entrada.Any())
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                decimal inversion = entrada.Sum(e => (e.PrecioCompra * e.ExistenciaInicial));
                return Ok(inversion);
            }
        }

        [HttpGet]
        [Route("TarjetaMargenUtilidadGlobal")]
        public async Task<ActionResult<string>> ConsultarMargenUtilidad()
        {
            var entrada = await _context.Entradas.ToListAsync();

            var salida = await _context.Salidas.ToArrayAsync(); 

            decimal ingresosTotales = salida.Sum(s => s.TotalPagarConDescuento);

            decimal costosTotales = entrada.Sum(e => e.PrecioCompra * e.ExistenciaInicial);

            decimal utilidadGlobal = ((ingresosTotales - costosTotales)/ingresosTotales) * 100;

            int utilidadGlobalRedondeada = (int)Math.Round(utilidadGlobal);

            string utilidadGlobalFormateada = $"{utilidadGlobalRedondeada}%";

            return Ok(utilidadGlobalFormateada);
        }

        ///Graficas

        [HttpGet]
        [Route("Grafica/UtilidadPorProducto")]
        public async Task<ActionResult<IEnumerable<string>>> ConsultarUtilidadPorProducto()
        {
            var entradas = await _context.Entradas.Include(e => e.Producto).ToListAsync();

            var utilidadesPorProducto = entradas.Select(e => new
            {
                NombreProducto = e.Producto != null ? e.Producto.Nombre : "Producto Desconocido",
                Utilidad = Math.Round(((e.PrecioVenta - e.PrecioCompra) / e.PrecioCompra) * 100, 2)
            })
            .Select(up => $"{up.NombreProducto}: {up.Utilidad}%")
            .ToList();

            return Ok(utilidadesPorProducto);
        }

        [HttpGet("Grafica/ConsultarClienteSalidas")]
        public async Task<ActionResult<IEnumerable<ClienteSalidas>>> ConsultarClienteSalida()
        {
            var salidasClientes = await _context.Salidas
                .GroupBy(s => s.IdCliente)
                .Select(g => new ClienteSalidas
                {
                    IdCliente = g.Key,
                    CantidadSalidas = g.Count()
                })
                .ToListAsync();

            foreach (var clienteSalidaCount in salidasClientes)
            {
                var cliente = await _context.Clientes
                    .Where(c => c.Id == clienteSalidaCount.IdCliente)
                    .FirstOrDefaultAsync();

                if (cliente != null)
                {
                    clienteSalidaCount.NombreCliente = cliente.Nombre;
                }
            }

            return salidasClientes;
        }

        [HttpGet("Grafica/ConsultarCategoriaProductos")]
        public async Task<ActionResult<IEnumerable<CategoriaProductos>>> ConsultarCategoriaProductos()
        {
            var productosCategorias = await _context.Productos
                .GroupBy(p => p.IdCategoria)
                .Select(g => new CategoriaProductos
                {
                    IdCategoria = g.Key,
                    CantidadProductos = g.Count()
                })
                .ToListAsync();

            foreach (var categoriaProductosCount in productosCategorias)
            {
                var categoria = await _context.Categorias
                    .Where(c => c.Id == categoriaProductosCount.IdCategoria)
                    .FirstOrDefaultAsync();

                if (categoria != null)
                {
                    categoriaProductosCount.NombreCategoria = categoria.Nombre;
                }
            }

            return productosCategorias;
        }

        [HttpGet("Grafica/TopProductosMenosVendidos")]
        public async Task<ActionResult<IEnumerable<TopProducto>>> ConsultarTopProductosMenosVendidos()
        {
            var topProductos = await _context.ProductoSalidas
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

        [HttpGet("Grafica/MesesConMasVentas")]
        public async Task<ActionResult<IEnumerable<TopMeses>>> ConsultarMesesConMasSalidas()
        {
            var mesesConSalidas = await _context.Salidas
                .GroupBy(s => s.FechaFactura.Month)
                .Select(g => new TopMeses
                {
                    Mes = Utilidades.ObtenerNombreMes(g.Key),
                    CantidadSalidas = g.Count()
                })
                .OrderByDescending(m => m.CantidadSalidas)
                .Take(12)
                .ToListAsync();

            return mesesConSalidas;
        }

        //se van a usar estas

        [HttpGet("Grafica/VentasPorMes")]
        public ActionResult<IEnumerable<SalidasPorMes>> ConsultarSalidasPorMes()
        {
            var salidasPorMes = (from mes in Enumerable.Range(1, 12)
                                 join salida in _context.Salidas
                                 on mes equals salida.FechaFactura.Month into gj
                                 from subSalida in gj.DefaultIfEmpty()
                                 group subSalida by mes into g
                                 select new SalidasPorMes
                                 {
                                     Mes = Utilidades.ObtenerNombreMes(g.Key),
                                     CantidadSalidas = g.Count(s => s != null)
                                 }).ToList();

            return salidasPorMes;
        }

        [HttpGet("Grafica/ComprasPorMes")]
        public ActionResult<IEnumerable<EntradasPorMes>> ConsultarComprasPorMes()
        {
            var comprasPorMes = (from mes in Enumerable.Range(1, 12)
                                 join entrada in _context.Entradas
                                 on mes equals entrada.FechaEntrada.Month into gj
                                 from subEntrada in gj.DefaultIfEmpty()
                                 group subEntrada by mes into g
                                 select new EntradasPorMes
                                 {
                                     Mes = Utilidades.ObtenerNombreMes(g.Key),
                                     CantidadEntradas = g.Count(e => e != null)
                                 }).ToList();

            return comprasPorMes;
        }

        [HttpGet("Grafica/GananciaPorProducto")]
        public async Task<ActionResult<IEnumerable<GananciaPorProducto>>> ConsultarGananciaPorProducto()
        {
            var gananciasPorProducto = await _context.Entradas
                .GroupBy(ps => ps.IdProducto)
                .Select(g => new
                {
                    IdProducto = g.Key,
                    Cantidad = g.Sum(ps => ps.ExistenciaInicial),
                    PrecioVenta = g.Average(ps => ps.PrecioVenta),
                    PrecioCompra = g.Average(ps => ps.PrecioCompra)
                })
                .ToListAsync();

            var listaGananciaPorProducto = new List<GananciaPorProducto>();

            foreach (var gananciaPorProducto in gananciasPorProducto)
            {
                var producto = await _context.Productos.FindAsync(gananciaPorProducto.IdProducto);

                decimal ganancia = producto != null ? (gananciaPorProducto.PrecioVenta - gananciaPorProducto.PrecioCompra) : 0;

                listaGananciaPorProducto.Add(new GananciaPorProducto
                {
                    IdProducto = gananciaPorProducto.IdProducto,
                    NombreProducto = producto != null ? producto.Nombre : null,
                    Ganancia = ganancia
                });
            }

            return listaGananciaPorProducto;
        }

        [HttpGet("Grafica/TodosLosProductosConVentas")]
        public async Task<ActionResult<IEnumerable<TopProducto>>> ConsultarTodosLosProductosConVentas()
        {
            var productos = await _context.Entradas
                .Select(e => new
                {
                    e.IdProducto,
                    e.Producto.Nombre,
                    CantidadVentas = e.ExistenciaInicial - e.ExistenciaActual
                })
                .ToListAsync();

            var productosAgrupados = productos
                .GroupBy(p => new { p.IdProducto, p.Nombre })
                .Select(g => new TopProducto
                {
                    IdProducto = g.Key.IdProducto,
                    NombreProducto = g.Key.Nombre,
                    CantidadVentas = g.Sum(p => p.CantidadVentas)
                })
                .OrderByDescending(tp => tp.CantidadVentas)
                .ToList();

            int contadorTop = 1;
            foreach (var producto in productosAgrupados)
            {
                producto.Top = contadorTop++;
            }

            return productosAgrupados;
        }

        [HttpGet("Grafica/TopProductosMasVendidos")]
        public async Task<ActionResult<IEnumerable<TopProducto>>> ConsultarTopProductosMasVendidos()
        {
            var topProductos = await _context.ProductoSalidas
                .GroupBy(ps => ps.IdProducto)
                .Select(g => new TopProducto
                {
                    IdProducto = g.Key,
                    CantidadVentas = g.Sum(ps => ps.Cantidad)
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

        [HttpGet("Grafica/TopMejoresClientes")]
        public async Task<ActionResult<IEnumerable<TopClientes>>> TopMejoresClientes()
        {
            var topClientes = await _context.Salidas
                .GroupBy(s => s.IdCliente)
                .Select(g => new TopClientes
                {
                    IdCliente = g.Key,
                    CantidadCompras = g.Count()
                })
                .OrderByDescending(tp => tp.CantidadCompras)
                .Take(5)
                .ToListAsync();

            int contadorTop = 1;

            foreach (var cliente in topClientes)
            {
                cliente.Top = contadorTop++;

                var infoCliente = await _context.Clientes
                    .Where(c => c.Id == cliente.IdCliente)
                    .FirstOrDefaultAsync();

                if (infoCliente != null)
                {
                    cliente.NombreCliente = infoCliente.Nombre;
                }
            }

            return topClientes;
        }
    }
    public class ClienteSalidas
    {
        public int IdCliente { get; set; }
        public string ? NombreCliente { get; set; }
        public int CantidadSalidas { get; set; }
    }

    public class CategoriaProductos
    {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public int CantidadProductos { get; set; }
    }
    public class TopProducto
    {
        public int Top { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int CantidadVentas { get; set; }
    }

    public class TopClientes
    {
        public int Top { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public int CantidadCompras { get; set; }
    }

    public class TopMeses
    {
        public string Mes { get; set; }
        public int CantidadSalidas { get; set; }
    }

    public class SalidasPorMes
    {
        public string Mes { get; set; }
        public int CantidadSalidas { get; set; }
    }

    public class EntradasPorMes
    {
        public string Mes { get; set; }
        public int CantidadEntradas { get; set; }
    }

    public class GananciaPorProducto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal Ganancia { get; set; }
    }

    public static class Utilidades
    {
        public static string ObtenerNombreMes(int numeroMes)
        {
            switch (numeroMes)
            {
                case 1:
                    return "Enero";
                case 2:
                    return "Febrero";
                case 3:
                    return "Marzo";
                case 4:
                    return "Abril";
                case 5:
                    return "Mayo";
                case 6:
                    return "Junio";
                case 7:
                    return "Julio";
                case 8:
                    return "Agosto";
                case 9:
                    return "Septiembre";
                case 10:
                    return "Octubre";
                case 11:
                    return "Noviembre";
                case 12:
                    return "Diciembre";
                default:
                    return "Desconocido";
            }
        }
    }
}
