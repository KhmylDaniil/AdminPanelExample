using AdminPanel.Core.Entities;

namespace AdminPanel.Core.Contracts.DTO
{
    /// <summary>
    /// DTO for user
    /// </summary>
    public class UserDTO
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Email { get; init; }

        public string Roles { get; init; }

        /// <summary>
        /// Constructor for mapping User entity to UserDTO
        /// </summary>
        /// <param name="user">User</param>
        public UserDTO(User user)
        {
            Id = user.Id; 
            Name = user.Name; 
            Email = user.Email;
            Roles = string.Join(", ", user.Roles.Select(x => x.Name));
        }
    }
}
