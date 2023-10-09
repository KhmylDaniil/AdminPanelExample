namespace AdminPanel.Core.Interfaces
{
    /// <summary>
    /// Interface for password hashing and compare
    /// </summary>
    public interface IPasswordService
    {
        /// <summary>
        /// Compares two password hashes to check if they are equal
        /// </summary>
        /// <param name="dbPasswordHash">The password hash stored in the database</param>
        /// <param name="password">The password entered by the user</param>
        /// <returns>Returns true if the password hashes match; otherwise, false</returns>
        bool ComparePasswordHashes(string dbPasswordHash, string password);

        /// <summary>
        /// Creates a password hash from the given password
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <returns>The hashed password</returns>
        string CreatePasswordHash(string password);
    }
}
