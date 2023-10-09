namespace AdminPanel.Core.Entities
{
    /// <summary>
    /// Role in project
    /// </summary>
    public class Role: BaseEntity
    {
        /// <summary>
        /// Role name
        /// </summary>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Users
        /// </summary>
        public List<User> Users { get; init; } = new();
    }
}
