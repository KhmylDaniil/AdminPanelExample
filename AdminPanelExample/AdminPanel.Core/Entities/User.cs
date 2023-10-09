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
        public string Name { get; private set; } = string.Empty;

        /// <summary>
        /// Age
        /// </summary>
        public int Age { get; private set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; private set; } = string.Empty;

        /// <summary>
        /// Hashed password
        /// </summary>
        public string Password { get; private set; } = string.Empty;

        /// <summary>
        /// User roles
        /// </summary>
        public List<Role> Roles { get; private set; } = new();

        /// <summary>
        /// ctor for EF for <see cref="User"/>
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// ctor for <see cref="User"/>
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="age">Age</param>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <param name="roles">Roles</param>
        public User(string name, int age, string email, string password, List<Role> roles)
        {
            Name = name;
            Age = age;
            Email = email;
            Password = password;
            Roles = roles;
        }

        /// <summary>
        /// Method for change user
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="age">Age</param>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <param name="roles">Roles</param>
        public void ChangeUser(string name, int age, string email, string password, List<Role> roles)
        {
            Name = name;
            Age = age;
            Email = email;
            Password = password;
            Roles = roles;
        }
    }
}
