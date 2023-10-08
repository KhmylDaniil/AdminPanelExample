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
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.HasDefaultSchema("adminPanel");
        }
    }
}
