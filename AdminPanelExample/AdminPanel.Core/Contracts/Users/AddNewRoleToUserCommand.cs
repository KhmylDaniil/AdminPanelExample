namespace AdminPanel.Core.Contracts.Users
{
    /// <summary>
    /// Command for adding new role to user
    /// </summary>
    public class AddNewRoleToUserCommand
    {
        /// <summary>
        /// User id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Role type
        /// </summary>
        public RoleType Role { get; set; }
    }
}
