using Microsoft.EntityFrameworkCore;
using Usuarios.Server.Models;

namespace Usuarios.Server
{
    public class Start
    {
        private MiDbContext _context { get; set; }
        public Start(DbContext context)
        {
            _context = (MiDbContext)context;
        }

        public async Task Seed()
        {
            try
            {
                if (!_context.Usuarios.Any()) await CrearDatosIniciales();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private async Task CrearDatosIniciales()
        {
            var usuarios = new List<Usuario>
        {
            new Usuario
            {
                UserName = "test1",
                Email = "test1@gmail.com",
                Password = "test123",
            },
            new Usuario
            {
                UserName = "test2",
                Email = "test2@gmail.com",
                Password = "test123",
            },
             new Usuario
            {
                UserName = "test3",
                Email = "test3@gmail.com",
                Password = "test123",
            },
        };

            await _context.AddRangeAsync(usuarios);
            await _context.SaveChangesAsync();
        }
    }
}
