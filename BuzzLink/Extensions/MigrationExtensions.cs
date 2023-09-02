using Microsoft.EntityFrameworkCore;

namespace BuzzLink.Extensions
{
    public static class MigrationExtensions
    {

        /// <summary>
        /// Intention is for thix to be a developer experience, not runtime.  Migrations will be applied out of band from the application code.
        /// </summary>
        /// <param name="app"></param>
        public static void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            dbContext.Database.Migrate();
         }

    }
}
