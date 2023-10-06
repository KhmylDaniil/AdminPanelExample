using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdminPanel.DAL
{
    /// <summary>
    /// Entry point to DAL project
    /// </summary>
    public static class EntryDbProject
    {
        /// <summary>
        /// Add Db support
        /// </summary>
        /// <param name="services">service collection</param>
        /// <param name="configuration">configuration</param>
        /// <returns>service collction</returns>
        public static IServiceCollection AddDbSupport(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var connectionString = configuration.GetConnectionString("Default")
                ?? throw new ArgumentException("Connection string not found.");

            services.AddDbContext<AppDbContext>(o =>
            {
                o.UseNpgsql(
                    connectionString: connectionString,
                    npgsqlOptionsAction: sqlOptionsBuilder =>
                    {
                        sqlOptionsBuilder.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "adminPanel");
                    });
            });

            return services;
        }

        /// <summary>
        /// Db auto-migration
        /// </summary>
        /// <param name="serviceProvider">service provider</param>
        public static void MigrateDB(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<AppDbContext>()
                ?? throw new ArgumentException("This should never happen, the DbContext couldn't resolve!");

            dbContext.Database.Migrate();
        }
    }
}