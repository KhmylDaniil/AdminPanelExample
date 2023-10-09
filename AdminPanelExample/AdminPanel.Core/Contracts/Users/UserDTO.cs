using AdminPanel.Core.Entities;

namespace AdminPanel.Core.Contracts.Users
{
    /// <summary>
    /// DTO for user
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Age
        /// </summary>
        public int Age { get; init; }

        /// <summary>
        /// email
        /// </summary>
        public string Email { get; init; }

        /// <summary>
        /// Roles as joined string
        /// </summary>
        public string Roles { get; init; }

        /// <summary>
        /// Constructor for mapping User entity to UserDTO
        /// </summary>
        /// <param name="user">User</param>
        public UserDTO(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Age = user.Age;
            Email = user.Email;
            Roles = string.Join(", ", user.Roles.Select(x => x.Name));
        }
    }
}
