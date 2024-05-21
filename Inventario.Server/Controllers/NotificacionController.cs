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
    }
}
