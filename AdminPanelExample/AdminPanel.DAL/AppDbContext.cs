using AdminPanel.Core.Entities;
using AdminPanel.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.DAL
{
    /// <summary>
    /// Db context
    /// </summary>
    public class AppDbContext: DbContext, IAppDbContext
    {
        /// <summary>
        /// Users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Roles
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// ctor for <see cref="AppDbContext"/>
        /// </summary>
        /// <param name="options">options</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Overridden method for gathering configurations
        /// </summary>
        /// <param name="modelBuilder">model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.HasDefaultSchema("adminPanel");
        }
    }
}
