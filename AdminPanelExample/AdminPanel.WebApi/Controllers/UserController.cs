using AdminPanel.Core;
using AdminPanel.Core.Contracts.Users;
using AdminPanel.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.WebApi.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// UserController ctor
        /// </summary>
        /// <param name="userService">user service</param>
        public UserController(IUserService userService) => _userService = userService;

        /// <summary>
        /// Get list of users
        /// </summary>
        /// <param name="query">Query</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <response code="200">
        /// Returns if succeeded
        /// </response>
        /// <response code="500">
        /// Returns in case of other errors
        /// </response>
        /// <returns>Filtered and sorted list of users</returns>
        [HttpGet]
        public async Task<List<UserDTO>> GetUsersAsync([FromQuery] GetUsersQuery query, CancellationToken cancellationToken)
            => await _userService.GetUsersAsync(query, cancellationToken);

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <response code="200">
        /// Returns if succeeded
        /// </response>
        /// <response code="404">
        /// Returns if not found
        /// </response>
        /// <response code="500">
        /// Returns in case of other errors
        /// </response>
        /// <returns>User</returns>
        [HttpGet("{id}")]
        public async Task<UserDTO> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
            => await _userService.GetUserByIdAsync(id, cancellationToken);

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="command">Create user command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <response code="200">
        /// Returns if succeeded
        /// </response>
        /// <response code="404">
        /// Returns in case of not founding necessary entities
        /// </response>
        /// <response code="422">
        /// Returns in case of validation error
        /// </response>
        /// <response code="500">
        /// Returns in case of other errors
        /// </response>
        [HttpPost]
        public async Task Post([FromQuery] CreateUserCommand command, CancellationToken cancellationToken)
            => await _userService.CreateUserAsync(command, cancellationToken);

        /// <summary>
        /// Edit user
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="command">Create user command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <response code="200">
        /// Returns if succeeded
        /// </response>
        /// <response code="404">
        /// Returns in case of not founding necessary entities
        /// </response>
        /// <response code="422">
        /// Returns in case of validation error
        /// </response>
        /// <response code="500">
        /// Returns in case of other errors
        /// </response>
        [HttpPut("{id}")]
        public async Task UpdateUserAsync(Guid id, [FromQuery] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var updateUserCommand = (UpdateUserCommand)command;
            updateUserCommand.Id = id;
            await _userService.UpdateUserAsync(updateUserCommand, cancellationToken);
        }

        /// <summary>
        /// Add new role to user
        /// </summary>
        /// <param name="command">Add new role to user command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <response code="200">
        /// Returns if succeeded
        /// </response>
        /// <response code="401">
        /// Returns when not authorized
        /// </response>
        /// <response code="404">
        /// Returns in case of not founding necessary entities
        /// </response>
        /// <response code="422">
        /// Returns in case of validation error
        /// </response>
        /// <response code="500">
        /// Returns in case of other errors
        /// </response>
        [HttpPatch]
        [Authorize]
        [CustomAuthorization(Constants.RoleClaim, Constants.AdminRoleName)]
        public async Task AddNewRoleToUserAsync([FromQuery] AddNewRoleToUserCommand command, CancellationToken cancellationToken)
            => await _userService.AddNewRoleToUser(command, cancellationToken);

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <response code="200">
        /// Returns if succeeded
        /// </response>
        /// <response code="404">
        /// Returns in case of not founding necessary entities
        /// </response>
        /// <response code="500">
        /// Returns in case of other errors
        /// </response>
        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
            => await _userService.DeleteUserAsync(id, cancellationToken);
    }
}
