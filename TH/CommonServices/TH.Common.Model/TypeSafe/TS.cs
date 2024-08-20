namespace TH.Common.Model;

public static class TS
{
    public static class Controllers
    {
        public const string Company = "Company";
        public const string Branch = "Branch";
        public const string BranchUser = "BranchUser";
        public const string Role = "Role";
        public const string User = "User";
        public const string UserRole = "UserRole";
        public const string Permission = "Permission";
    }

    public static class Permissions
    {
        public const string None = "";
        public const string Read = "Read";
        public const string Write = "Write";
        public const string Update = "Update";
        public const string SoftDelete = "SoftDelete";
        public const string Delete = "Delete";
    }
}