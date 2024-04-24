using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Usuarios.Server.Models;

namespace Usuarios.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly MiDbContext _context;

        public UsuariosController(MiDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return Ok("Se guardo exitosamente," + usuario.Email);
        }
        [HttpGet]
        [Route("AutenticacionRegistrar")]
        public async Task<ActionResult> AutenticacionRegistrar(string email)
        {
            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
            {
                return NotFound("El correo electronico proporcionado no esta registrado.");
            }
            else
            {
                return Ok(usuario);
            }
        }

        [HttpGet]
        [Route("Autenticacion")]
        public async Task<ActionResult> Autenticacion(string email, string password)
        {
            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
            {
                return NotFound("El correo electronico proporcionado no esta registrado.");
            }
            else
            {
                if (usuario.Password == password)
                {
                    return Ok(usuario);
                }
                else
                {
                    return BadRequest("La contraseña proporcionada es incorrecta.");
                }
            }
        }

        [HttpPut]
        [Route("CambiarPasswordPorEmail")]
        public async Task<IActionResult> CambiarContraseñaPorEmail(string email, string nuevaContraseña)
        {
            Usuario usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            if (usuarioExistente == null)
            {
                return NotFound("No se encontro un usuario con el correo electronico proporcionado.");
            }
            else
            {
                usuarioExistente.Password = nuevaContraseña;
                await _context.SaveChangesAsync();

                return Ok("Se actualizó la contraseña del usuario " + usuarioExistente.Email + ", exitosamente.");
            }
        }

        [HttpPut]
        [Route("CambiarPasswordPorUserName")]
        public async Task<IActionResult> CambiarContraseñaPorUserName(string userName, string nuevaContraseña)
        {
            Usuario usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.UserName == userName);

            if (usuarioExistente == null)
            {
                return NotFound("No se encontro un usuario con el nombre de usuario proporcionado.");
            }
            else
            {
                usuarioExistente.Password = nuevaContraseña;
                await _context.SaveChangesAsync();

                return Ok("Se actualizó la contraseña del usuario " + usuarioExistente.UserName + ", exitosamente.");
            }
        }

        [HttpDelete]
        [Route("EliminarCuentaPorEmail")]
        public async Task<IActionResult> EliminarEmail(string email)
        {
            Usuario usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            if (usuarioExistente == null)
            {
                return NotFound("No existes el usuario que buscas");
            }
            else
            {
                _context.Usuarios.Remove(usuarioExistente!);

                await _context.SaveChangesAsync();

                return Ok("Se elimino exitosamente, " + usuarioExistente.Email);
            }

        }

        [HttpDelete]
        [Route("EliminarCuentaPorUserName")]
        public async Task<IActionResult> EliminarUserName(string userName)
        {
            Usuario usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.UserName == userName);

            if (usuarioExistente == null)
            {
                return NotFound("No existes el usuario que buscas");
            }
            else
            {
                _context.Usuarios.Remove(usuarioExistente!);

                await _context.SaveChangesAsync();

                return Ok("Se elimino exitosamente, " + usuarioExistente.UserName);
            }
        }
    }
}
