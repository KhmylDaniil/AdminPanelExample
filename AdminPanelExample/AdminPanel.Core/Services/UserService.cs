using AdminPanel.Core.Contracts;
using AdminPanel.Core.Contracts.DTO;
using AdminPanel.Core.Entities;
using AdminPanel.Core.Exceptions;
using AdminPanel.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace AdminPanel.Core.Services
{
    /// <summary>
    /// User service
    /// </summary>
    public class UserService: IUserService
    {
        private readonly IAppDbContext _appDbContext;

        public UserService(IAppDbContext appDbContext) => _appDbContext = appDbContext;

        /// <summary>
        /// Get user list
        /// </summary>
        /// <param name="query">Filter query</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>UserDTO list</returns>
        public async Task<List<UserDTO>> GetUsersAsync(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var filter = _appDbContext.Users.Include(u => u.Roles)
                .Where(u => string.IsNullOrEmpty(query.SearchByName) || u.Name.Contains(query.SearchByName))
                .Where(u => string.IsNullOrEmpty(query.SearchByEmail) || u.Email.Contains(query.SearchByEmail))
                .Where(u => u.Age >= query.MinAge && u.Age <= query.MaxAge)
                .Where(u => query.SearchByRole == null || u.Roles.Any(r => r.Name.Contains(Enum.GetName(query.SearchByRole.Value))));
            
            return await filter.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).Select(u => new UserDTO(u)).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>UserDTO</returns>
        public async Task<UserDTO> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var existingUser = await FindUserAsync(id, cancellationToken);

            return new UserDTO(existingUser);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="command">Create user command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task CreateUserAsync(CreateUserCommand command, CancellationToken cancellationToken)
        {
            if (await _appDbContext.Users.AnyAsync(u => u.Email == command.Email, cancellationToken))
                throw new RequestValidationException("Not unique user name");

            var roles = await FindRolesAsync(command.RoleNames, cancellationToken);

            _appDbContext.Users.Add(new User(command.Name, command.Age, command.Email, roles));
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Update existing user
        /// </summary>
        /// <param name="command"><UpdateUserCommand/param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task UpdateUserAsync(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await FindUserAsync(command.Id, cancellationToken);

            if (await _appDbContext.Users.AnyAsync(u => u.Email == command.Email && u.Id != command.Id, cancellationToken))
                throw new RequestValidationException("Not unique user name");

            var roles = await FindRolesAsync(command.RoleNames, cancellationToken);

            existingUser.ChangeUser(command.Name, command.Age, command.Email, roles);

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Add new role to user
        /// </summary>
        /// <param name="command">AddNewRoleToUserCommand</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task AddNewRoleToUser(AddNewRoleToUserCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await FindUserAsync(command.Id, cancellationToken);

            var roles = await FindRolesAsync(new List<RoleType>() { command.Role }, cancellationToken);

            existingUser.Roles.AddRange(roles);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="cancellationToken">cancellation token</param>
        public async Task DeleteUserAsync(Guid id, CancellationToken cancellationToken)
        {
            var existingUser = await FindUserAsync(id, cancellationToken);

            _appDbContext.Users.Remove(existingUser);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Finding user by Id in Db 
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Not-null user</returns>
        private async Task<User> FindUserAsync(Guid id, CancellationToken cancellationToken)
            => await _appDbContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == id, cancellationToken)
                 ?? throw new EntityNotFoundException<User>(id);

        /// <summary>
        /// Finding necessary roles in Db
        /// </summary>
        /// <param name="roles">Roles as enum</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>list of roles</returns>
        private async Task<List<Role>> FindRolesAsync(IEnumerable<RoleType> roles, CancellationToken cancellationToken)
        {
            var rolesFromDb = await _appDbContext.Roles.ToListAsync(cancellationToken);

            var result = new List<Role>();

            foreach (var roleName in roles.Select(role => Enum.GetName(role)))
            {
                var role = rolesFromDb.FirstOrDefault(r => r.Name.Equals(roleName))
                    ?? throw new EntityNotFoundException<Role>(roleName);

                result.Add(role);
            }

            return result;
        }
    }
}
