using AdminPanel.Core.Contracts.Login;
using AdminPanel.Core.Entities;

namespace AdminPanel.Core.Interfaces
{
    public interface IJwtService
    {
        /// <summary>
        /// Generates a JWT token for a user.
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>The token transfer object.</returns>
        Token GenerateToken(User user);
    }
}
