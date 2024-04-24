using Microsoft.EntityFrameworkCore;

namespace Usuarios.Server.Models
{
    public class MiDbContext : DbContext
    {
        public MiDbContext(DbContextOptions<MiDbContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Usuario>().HasData(new Usuario { Id = 1, UserName = "Admin", Email = "admin@gmail.com", Password = "admin1" });
            modelBuilder.Entity<Usuario>().HasData(new Usuario { Id = 2, UserName = "Test", Email = "test@gmail.com", Password = "123456" });
        }
    }
}
