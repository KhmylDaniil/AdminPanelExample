using AdminPanel.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Core.Interfaces
{
    /// <summary>
    /// Interface for DbContext class to avoid circular dependancy
    /// </summary>
    public interface IAppDbContext
    {
        /// <summary>
        /// Users
        /// </summary>
        DbSet<User> Users { get; }

        /// <summary>
        /// Roles
        /// </summary>
        DbSet<Role> Roles { get; }

        /// <summary>
        /// Save changes
        /// </summary>
        /// <param name="cancellationToken">cancellation token</param>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
