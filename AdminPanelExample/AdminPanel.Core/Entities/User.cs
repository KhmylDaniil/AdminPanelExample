
namespace AdminPanel.Core.Entities
{
    /// <summary>
    /// User
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// User roles
        /// </summary>
        public List<Role> Roles { get; set; } = new();

        public User()
        {
        }

        public User(string name, int age, string email, List<Role> roles)
        {
            Name = name;
            Age = age;
            Email = email;
            Roles = roles;
        }

        public void ChangeUser(string name, int age, string email, List<Role> roles)
        {
            Name = name;
            Age = age;
            Email = email;
            Roles = roles;
        }
    }
}
