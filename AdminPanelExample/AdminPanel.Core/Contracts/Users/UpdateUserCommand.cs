using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Core.Contracts.Users
{
    /// <summary>
    /// Command for update user
    /// </summary>
    public class UpdateUserCommand : CreateUserCommand
    {
        public Guid Id { get; set; }
    }
}
