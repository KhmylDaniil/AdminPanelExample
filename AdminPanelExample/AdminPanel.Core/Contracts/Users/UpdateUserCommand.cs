namespace AdminPanel.Core.Contracts.Users
{
    /// <summary>
    /// Command for update user
    /// </summary>
    public class UpdateUserCommand : CreateUserCommand
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
    }
}
