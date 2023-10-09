using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Core.Contracts.Users
{
    /// <summary>
    /// Command for user creation
    /// </summary>
    public class CreateUserCommand
    {
        /// <summary>
        /// Name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Age { get; set; }

        /// <summary>
        /// Email must be unique
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Password { get; set; }

        /// <summary>
        /// List of roles
        /// </summary>
        public RoleType[] RoleNames { get; set; } = Array.Empty<RoleType>();
    }
}
