using AdminPanel.Core.Contracts.Login;
using AdminPanel.Core.Entities;

namespace AdminPanel.Core.Interfaces
{
    /// <summary>
    ///  Interface for working with JWT tokens
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Generates a JWT token for a user
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Token transfer object</returns>
        Token GenerateToken(User user);
    }
}
