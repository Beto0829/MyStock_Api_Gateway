using Inventarios.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Server
{
    public class MigrateDbContextExtensions
    {
        public static void MigrateDbContextServices<TContext>(IApplicationBuilder app) where TContext : DbContext
        {
            var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetService<TContext>();

            try
            {
                if (!context!.Database.GetMigrations().Any()) return;
                if (!context!.Database.GetPendingMigrations().Any()) return;
                logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);
                context.Database.Migrate();
                logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
            }
        }

        public static void InitializeDatabase(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            if (scope == null) return;
            MigrateDbContextServices<MiDbContext>(app);
            var context = scope.ServiceProvider.GetRequiredService<MiDbContext>();
            if (!context.Database.CanConnect()) return;
            var start = new Start(context!);
            start.Seed().Wait();
        }
    }
}
