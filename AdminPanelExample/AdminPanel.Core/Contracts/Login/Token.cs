namespace AdminPanel.Core.Contracts.Login
{
    /// <summary>
    /// Dto representing a JWT token
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Access token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Refresh token
        /// </summary>
        public Guid RefreshToken { get; init; }
    }
}
