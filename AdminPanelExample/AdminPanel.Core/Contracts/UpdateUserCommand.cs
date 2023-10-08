
namespace AdminPanel.Core.Contracts
{
    /// <summary>
    /// Command for update user
    /// </summary>
    public class UpdateUserCommand : CreateUserCommand
    {
        public Guid Id { get; set; }
    }
}
