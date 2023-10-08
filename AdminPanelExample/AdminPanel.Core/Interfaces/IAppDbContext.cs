using AdminPanel.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Core.Interfaces
{
    /// <summary>
    /// Interface for DbContext class to avoid circular dependancy
    /// </summary>
    public interface IAppDbContext
    {
        DbSet<User> Users { get; }

        DbSet<Role> Roles { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
