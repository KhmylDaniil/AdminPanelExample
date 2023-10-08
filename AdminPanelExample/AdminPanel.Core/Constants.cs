namespace AdminPanel.Core
{
    /// <summary>
    /// Static class for all constants
    /// </summary>
    public static class Constants
    {
        public const string SuperAdminRoleName = "SuperAdmin";

        public const string AdminRoleName = "Admin";

        public const string SupportRoleName = "Support";

        public const string UserRoleName = "User";

        public static readonly Guid SuperAdminRoleId = new("D88A5791-AF22-41B5-B83F-B4D5A661C875");

        public static readonly Guid AdminRoleId = new("5B387480-1FEC-44F1-9774-D18353C5E489");

        public static readonly Guid SupportRoleId = new("E061708D-919C-48B0-AD2A-25A6155D0521");

        public static readonly Guid UserRoleId = new("5B006ACC-C409-4437-B1DC-F81692D11FC8");
    }

    /// <summary>
    /// Role types for add and update user commands
    /// </summary>
    public enum RoleType
    {
        SuperAdmin,
        Admin,
        Support,
        User
    }
}
