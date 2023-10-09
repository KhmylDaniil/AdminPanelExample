using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Core.Contracts.Users
{
    /// <summary>
    /// Query for get users with filters and sorting
    /// </summary>
    public class GetUsersQuery
    {
        /// <summary>
        /// Search by name field
        /// </summary>
        public string SearchByName { get; set; } = string.Empty;

        /// <summary>
        /// Min age
        /// </summary>
        public int MinAge { get; set; } = 0;

        /// <summary>
        /// Max age
        /// </summary>
        public int MaxAge { get; set; } = int.MaxValue;

        /// <summary>
        /// Search by email field
        /// </summary>
        public string SearchByEmail { get; set; } = string.Empty;

        /// <summary>
        /// Search by role field
        /// </summary>
        public RoleType? SearchByRole { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        [Range(1, 100)]
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Page number
        /// </summary>
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Filter by property field
        /// </summary>
        public UserPropertyType SortBy { get; set; }

        /// <summary>
        /// Filter by ascention field
        /// </summary>
        public bool IsAscending { get; set; }
    }
}
