using Inventarios.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionController : ControllerBase
    {
        private readonly MiDbContext _context;

        public NotificacionController(MiDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar(Notificacion notificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fechaHoraActual = DateTime.Now;

            notificacion.Fecha = new DateTime(fechaHoraActual.Year, fechaHoraActual.Month, fechaHoraActual.Day, fechaHoraActual.Hour, fechaHoraActual.Minute, 0);

            notificacion.Estado = false;

            await _context.Notificaciones.AddAsync(notificacion);
            await _context.SaveChangesAsync();

            return Ok("Se guardo exitosamente");
        }

        [HttpGet]
        [Route("Filtrar/NotificacionUsuario")]
        public async Task<ActionResult<IEnumerable<Notificacion>>> FiltarPorUsuario(string email)
        {
            var notificacion = await _context.Notificaciones
                .Where(p => p.Email == email)
                        .Select(p => new {
                            p.Id,
                            p.Titulo,
                            p.Cuerpo,
                            p.Fecha,
                            p.Estado,
                            p.Email
                        })
                .ToListAsync();

            if (notificacion == null || notificacion.Count == 0)
            {
                return NotFound("No se encontraron empresa especificada.");
            }
            else
            {

                return Ok(notificacion);
            }
        }

        [HttpPut]
        [Route("ActualizarEstado")]
        public async Task<IActionResult> ActualizarEstadoEntradas(int id, string email)
        {
            var notificacion = await _context.Notificaciones.FindAsync(id);

           
            if (notificacion!.Id == id && notificacion.Email == email && notificacion.Estado == false)
            {
                notificacion.Estado = true;
                await _context.SaveChangesAsync();
                return Ok("Notificacion leida exitosamente.");
            }
            else
            {
                return Ok("Ya fue leida la notificacion");
            }
        }

        [HttpPost]
        [Route("GenerarNotificacionesBajoInventario")]
        public async Task<IActionResult> GenerarNotificacionesBajoInventario(string email)
        {
            var entradas = await _context.Entradas
                .Where(e => e.ExistenciaActual > 0 && e.ExistenciaActual < 0.2 * e.ExistenciaInicial)
                .Select(e => new
                {
                    e.Id,
                    e.Producto.Nombre,
                    e.ExistenciaInicial,
                    e.ExistenciaActual
                })
                .ToListAsync();

            var notificaciones = entradas.Select(e => new Notificacion
            {
                Titulo = "Bajo Inventario",
                Cuerpo = $"El producto {e.Nombre} tiene un inventario bajo. Existencia actual: {e.ExistenciaActual}",
                Fecha = DateTime.Now,
                Estado = false,
                Email = email
            }).ToList();

            await _context.Notificaciones.AddRangeAsync(notificaciones);
            await _context.SaveChangesAsync();

            return Ok("Notificaciones generadas exitosamente");
        }

        [HttpPost]
        [Route("GenerarNotificacionesInventarioAgotado")]
        public async Task<IActionResult> GenerarNotificacionesInventarioAgotado(string email)
        {
            var entradas = await _context.Entradas
                .Where(e => e.ExistenciaActual == 0)
                .Select(e => new
                {
                    e.Id,
                    e.Producto.Nombre,
                    e.ExistenciaInicial,
                    e.ExistenciaActual
                })
                .ToListAsync();

            var notificaciones = entradas.Select(e => new Notificacion
            {
                Titulo = "Inventario Agotado",
                Cuerpo = $"El producto {e.Nombre} está agotado. Existencia actual: {e.ExistenciaActual}",
                Fecha = DateTime.Now,
                Estado = false,
                Email = email
            }).ToList();

            await _context.Notificaciones.AddRangeAsync(notificaciones);
            await _context.SaveChangesAsync();

            return Ok("Notificaciones generadas exitosamente");
        }

        [HttpPost]
        [Route("GenerarNotificacionesCompras")]
        public async Task<IActionResult> GenerarNotificacionesCompras(Notificacion notificacion, string email, string id, string nombre, double precio, string nombreProveedor)
        {
            var fecha = DateTime.Now;
            fecha.ToString("dd/MM/yy HH:mm");

            notificacion.Titulo = "Compra creada exitosamente";
            notificacion.Cuerpo = $"Se realizo una compra con id {id} al producto {nombre}, con un precio total de {precio} y al proveedor {nombreProveedor} en la fecha {fecha}.";
            notificacion.Estado = false;
            notificacion.Email = email;

            await _context.Notificaciones.AddAsync(notificacion);
            await _context.SaveChangesAsync();

            return Ok("Notificaciones generadas exitosamente");
        }

        [HttpPost]
        [Route("GenerarNotificacionesVentas")]
        public async Task<IActionResult> GenerarNotificacionesVentas(Notificacion notificacion, string email, string id, string nombreCliente, double precioTotal, double cantidadProductos)
        {
            var fecha = DateTime.Now;
            fecha.ToString("dd/MM/yy HH:mm");

            notificacion.Titulo = "Compra creada exitosamente";
            notificacion.Cuerpo = $"Se realizo una venta con un id {id} a nombre del cliente {nombreCliente}, con un precio total de {precioTotal} y una cantidad total de {cantidadProductos} en la fecha {fecha}.";
            notificacion.Estado = false;
            notificacion.Email = email;

            await _context.Notificaciones.AddAsync(notificacion);
            await _context.SaveChangesAsync();

            return Ok("Notificaciones generadas exitosamente");
        }
    }
}
