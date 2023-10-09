using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Core.Contracts.Users
{
    /// <summary>
    /// Command for user creation
    /// </summary>
    public class CreateUserCommand
    {
        [Required]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int Age { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Password { get; set; }

        public RoleType[] RoleNames { get; set; } = Array.Empty<RoleType>();
    }

}
