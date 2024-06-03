using Inventario.Server.Models;
using Inventarios.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Server
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
                if (!_context.Categorias.Any()) await CrearDatosIniciales();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private async Task CrearDatosIniciales()
        {
            var categorias = new List<Categoria>
        {
            new Categoria
            {
                Nombre = "Deportes",
            },
             new Categoria
            {
                Nombre = "UPC",
            },
              new Categoria
            {
                Nombre = "GYM",
            },
        };

            await _context.AddRangeAsync(categorias);
            await _context.SaveChangesAsync();
        }
    }
}
