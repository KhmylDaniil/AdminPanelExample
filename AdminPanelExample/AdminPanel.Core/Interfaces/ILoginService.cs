using AdminPanel.Core.Contracts.Login;

namespace AdminPanel.Core.Interfaces
{
    /// <summary>
    /// Inteeface for login service
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="command">login user command</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>token</returns>
        Task<Token> LoginUserAsync(LoginUserCommand command, CancellationToken cancellationToken);
    }
}
