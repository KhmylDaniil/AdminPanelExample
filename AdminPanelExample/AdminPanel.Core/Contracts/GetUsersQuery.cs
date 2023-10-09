using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Core.Contracts
{
    public class GetUsersQuery
    {
        public string SearchByName { get; set; } = string.Empty;

        public int MinAge { get; set; } = 0;

        public int MaxAge { get; set; } = int.MaxValue;

        public string SearchByEmail { get; set; } = string.Empty;

        public RoleType? SearchByRole { get; set; }

        [Range(1, 100)]
        public int PageSize { get; set; } = 10;

        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        public UserPropertyType SortBy { get; set; }

        public bool IsAscending { get; set; }
    }
}
