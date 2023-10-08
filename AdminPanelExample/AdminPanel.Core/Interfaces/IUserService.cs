using AdminPanel.Core.Contracts;
using AdminPanel.Core.Contracts.DTO;

namespace AdminPanel.Core.Interfaces
{
    /// <summary>
    /// interface for userService
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get user list
        /// </summary>
        /// <param name="query">Filter query</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>UserDTO list</returns>
        Task<List<UserDTO>> GetUsersAsync(GetUsersQuery query, CancellationToken cancellationToken);

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>UserDTO</returns>
        Task<UserDTO> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="command">Create user command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task CreateUserAsync(CreateUserCommand command, CancellationToken cancellationToken);

        /// <summary>
        /// Update existing user
        /// </summary>
        /// <param name="command"><UpdateUserCommand/param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task UpdateUserAsync(UpdateUserCommand command, CancellationToken cancellationToken);

        /// <summary>
        /// Add new role to user
        /// </summary>
        /// <param name="command">AddNewRoleToUserCommand</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task AddNewRoleToUser(AddNewRoleToUserCommand command, CancellationToken cancellationToken);

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="cancellationToken">cancellation token</param>
        Task DeleteUserAsync(Guid id, CancellationToken cancellationToken);
    }
}
