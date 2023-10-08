using AdminPanel.Core.Contracts;
using AdminPanel.Core.Contracts.DTO;
using AdminPanel.Core.Entities;
using AdminPanel.Core.Exceptions;
using AdminPanel.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>UserDTO list</returns>
        public async Task<List<UserDTO>> GetUsersAsync(CancellationToken cancellationToken)
        {
            return await _appDbContext.Users.Include(u => u.Roles).Select(u => new UserDTO(u)).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>UserDTO</returns>
        public async Task<UserDTO> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _appDbContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                ?? throw new EntityNotFoundException<User>(id);
            
            return new UserDTO(user);
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

            var roles = await _appDbContext.Roles.Where(r => command.RoleNames.Select(role => Enum.GetName(role)).Contains(r.Name)).ToListAsync(cancellationToken);

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
            var existingUser = await _appDbContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == command.Id, cancellationToken)
                 ?? throw new EntityNotFoundException<User>(id: command.Id);

            if (await _appDbContext.Users.AnyAsync(u => u.Email == command.Email && u.Id != command.Id, cancellationToken))
                throw new RequestValidationException("Not unique user name");

            var roles = await _appDbContext.Roles
                .Where(r => command.RoleNames
                .Select(role => Enum.GetName(role))
                .Contains(r.Name))
                .ToListAsync(cancellationToken);

            existingUser.ChangeUser(command.Name, command.Age, command.Email, roles);

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="cancellationToken">cancellation token</param>
        public async Task DeleteUserAsync(Guid id, CancellationToken cancellationToken)
        {
            var existingUser = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken)
                 ?? throw new EntityNotFoundException<User>(id);

            _appDbContext.Users.Remove(existingUser);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
