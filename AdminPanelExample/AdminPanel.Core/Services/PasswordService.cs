using AdminPanel.Core.Exceptions;
using AdminPanel.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AdminPanel.Core.Services
{
    /// <summary>
    /// Service for managing passwords and password hashing
    /// </summary>
    public sealed class PasswordService : IPasswordService
    {
        private readonly string _passwordSalt;

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordService"/>
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public PasswordService(IConfiguration configuration)
            => _passwordSalt = configuration["PasswordSalt"]
                ?? throw new ConfigurationException("PasswordSalt");

        /// <summary>
        /// Compares two password hashes to check if they are equal
        /// </summary>
        /// <param name="dbPasswordHash">The password hash stored in the database</param>
        /// <param name="password">The password entered by the user</param>
        /// <returns>Returns true if the password hashes match; otherwise, false</returns>
        public bool ComparePasswordHashes(string dbPasswordHash, string password)
            => CreateSha384(password).Equals(dbPasswordHash);

        /// <summary>
        /// Creates a password hash from the given password
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <returns>The hashed password</returns>
        public string CreatePasswordHash(string password)
            => CreateSha384(password);

        private string CreateSha384(string password)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(password + _passwordSalt);
            var hashBytes = System.Security.Cryptography.SHA384.HashData(inputBytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}
