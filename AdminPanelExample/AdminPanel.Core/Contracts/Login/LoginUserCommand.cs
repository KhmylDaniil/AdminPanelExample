namespace AdminPanel.Core.Contracts.Login
{
    /// <summary>
    /// command for log in
    /// </summary>
    public class LoginUserCommand
    {
        /// <summary>
        /// Email as login
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}
