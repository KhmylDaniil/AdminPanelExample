namespace AdminPanel.Core.Contracts
{
    /// <summary>
    /// Command for adding new role to user
    /// </summary>
    public class AddNewRoleToUserCommand
    {
        public Guid Id { get; set; }

        public RoleType Role { get; set; }
    }
}
