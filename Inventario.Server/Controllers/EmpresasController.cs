using Inventario.Server.Models;
using Inventarios.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly MiDbContext _context;

        public EmpresasController(MiDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar(Empresa empresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Empresas.AddAsync(empresa);
            await _context.SaveChangesAsync();

            return Ok("Se guardo exitosamente");
        }

        [HttpGet]
        [Route("Filtrar/EmpresaUsuario")]
        public async Task<ActionResult<IEnumerable<Empresa>>> FiltarPorUsuario(string email)
        {
            var empresa = await _context.Empresas
                .Where(p => p.Usuario == email)
                        .Select(p => new {
                            p.Id,
                            p.Nombre,
                            p.Telefono,
                            p.Direccion,
                            p.Usuario
                        })
                .ToListAsync();

            if (empresa == null || empresa.Count == 0)
            {
                return NotFound("No se encontraron existencias para la categoría especificada.");
            }
            else
            {

                return Ok(empresa);
            }
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar(string email, Empresa empresa)
        {
            var empresaExistente = await _context.Empresas.FindAsync(email);

            if (empresaExistente == null)
            {
                return NotFound("No existes el dato que buscas");
            }
            else
            {
                empresaExistente!.Nombre = empresa.Nombre;
                empresaExistente!.Direccion = empresa.Direccion;
                empresaExistente!.Telefono = empresa.Telefono;

                await _context.SaveChangesAsync();

                return Ok("Se actualizo exitosamente");
            }
        }

    }
}
