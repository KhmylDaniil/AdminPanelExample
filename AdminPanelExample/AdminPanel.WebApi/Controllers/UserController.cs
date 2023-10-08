using AdminPanel.Core.Contracts;
using AdminPanel.Core.Contracts.DTO;
using AdminPanel.Core.Interfaces;
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
        /// Get all users
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <response code="200">
        /// Returns if succeeded
        /// </response>
        /// <response code="500">
        /// Returns if failed
        /// </response>
        /// <returns>List of users</returns>
        [HttpGet]
        public async Task<List<UserDTO>> GetUsersAsync(CancellationToken cancellationToken)
            => await _userService.GetUsersAsync(cancellationToken);

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <response code="200">
        /// Returns if succeeded
        /// </response>
        /// <response code="204">
        /// Returns if not found
        /// </response>
        /// <response code="500">
        /// Returns if failed
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
        /// <response code="500">
        /// Returns if failed
        /// </response>
        /// <returns></returns>
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
        /// <response code="500">
        /// Returns if failed
        /// </response>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task UpdateUserAsync(Guid id, [FromQuery] UpdateUserCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            await _userService.UpdateUserAsync(command, cancellationToken);
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <response code="200">
        /// Returns if succeeded
        /// </response>
        /// <response code="500">
        /// Returns if failed
        /// </response>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
            => await _userService.DeleteUserAsync(id, cancellationToken);
    }
}
