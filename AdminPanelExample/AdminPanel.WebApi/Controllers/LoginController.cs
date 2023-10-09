using AdminPanel.Core.Contracts.Login;
using AdminPanel.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.WebApi.Controllers
{
    /// <summary>
    /// Login controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService) => _loginService = loginService;

        /// <summary>
        /// Log in
        /// </summary>
        /// <param name="command">Login user command</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <response code="200">
        /// Returns if succeeded
        /// </response>
        /// <response code="500">
        /// Returns if failed
        /// </response>
        /// <returns>Token</returns>
        [HttpPost]
        public async Task<Token> LoginUserAsync([FromQuery] LoginUserCommand command, CancellationToken cancellationToken)
            => await _loginService.LoginUserAsync(command, cancellationToken);
    }
}
