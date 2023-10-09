using AdminPanel.Core.Contracts.Login;
using AdminPanel.Core.Entities;
using AdminPanel.Core.Exceptions;
using AdminPanel.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdminPanel.Core.Services
{
    /// <summary>
    ///  Service for JWT tokens.
    /// </summary>
    public sealed class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration provider as an <see cref="IConfiguration"/>.</param>
        public JwtService(IConfiguration configuration) => _configuration = configuration;

        /// <summary>
        /// Generates a JWT token for a user.
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>The token transfer object.</returns>
        public Token GenerateToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetJwtSecret()));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var nowUtc = DateTime.UtcNow;
            var expMinutes = GetExpiryMinutes();

            var claims = new List<Claim>
        {
            new (Constants.IdClaim, Convert.ToString(user.Id)),
            new (Constants.NameClaim, user.Name),
            new (Constants.RoleClaim, GetHighestRoleName(user))
        };

            var jwtToken = new JwtSecurityToken(
                issuer: GetJwtIssuer(),
                audience: GetJwtAudience(),
                claims,
                expires: nowUtc.AddMinutes(expMinutes).ToUniversalTime(),
                signingCredentials: credentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var refreshTokenValue = Guid.NewGuid();

            return new Token
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue
            };
        }

        private string GetJwtSecret()
            => _configuration["Token:JwtSecret"] ?? throw new ConfigurationException("Token:JwtSecret");

        private int GetExpiryMinutes()
        {
            var expiryMinutes = _configuration["Token:ExpiryMinutes"];

            if (!int.TryParse(expiryMinutes, out var minutes))
                throw new ConfigurationException("Token:ExpiryMinutes is not a valid integer.");

            return minutes;
        }

        private string GetJwtIssuer()
            => _configuration["Token:Issuer"] ?? throw new ConfigurationException("Token:Issuer");

        private string GetJwtAudience()
            => _configuration["Token:Audience"] ?? throw new ConfigurationException("Token:Audience");

        /// <summary>
        /// Find role name with max access for user
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>Role name with max access</returns>
        private string GetHighestRoleName(User user)
        {
            var role = user.Roles.FirstOrDefault(x => x.Id == Constants.SuperAdminRoleId)
                ?? user.Roles.FirstOrDefault(x => x.Id == Constants.AdminRoleId)
                ?? user.Roles.FirstOrDefault(x => x.Id == Constants.SupportRoleId)
                ?? user.Roles.FirstOrDefault(x => x.Id == Constants.UserRoleId);

            return role.Name;
        }
    }
}
