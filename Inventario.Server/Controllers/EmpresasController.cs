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
                .Where(p => p.Email == email)
                        .Select(p => new {
                            p.Id,
                            p.Nombre,
                            p.Telefono,
                            p.Direccion,
                            p.Email
                        })
                .ToListAsync();

            if (empresa == null || empresa.Count == 0)
            {
                return NotFound("No se encontraron empresa especificada.");
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
            var empresaExistente = await _context.Empresas.FirstOrDefaultAsync(e => e.Email == email);


            if (empresaExistente == null)
            {
                return NotFound("No existes el dato que buscas");
            }
            else
            {
                if (empresaExistente.Email != email)
                {
                    return BadRequest("El email no coincide con el usuario de la empresa");
                }

                empresaExistente!.Nombre = empresa.Nombre;
                empresaExistente!.Direccion = empresa.Direccion;
                empresaExistente!.Telefono = empresa.Telefono;

                await _context.SaveChangesAsync();

                return Ok("Se actualizo exitosamente");
            }
        }

    }
}
