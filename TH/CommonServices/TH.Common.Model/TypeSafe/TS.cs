namespace TH.Common.Model;

public static class TS
{
    public static class Controllers
    {
        public const string Company = "Company";
        public const string CompanySetting = "CompanySetting";
        public const string Branch = "Branch";
        public const string BranchUser = "BranchUser";
        public const string Role = "Role";
        public const string User = "User";
        public const string UserRole = "UserRole";
        public const string Permission = "Permission";
        public const string UserCompany = "UserCompany";
    }

    public static class Permissions
    {
        public const string None = "";
        public const string Read = "Read";
        public const string Write = "Write";
        public const string Update = "Update";
        public const string Archive = "Archive";
        public const string Delete = "Delete";
    }

    public static class Providers
    {
        public const string LOCAL = "LOCAL";
        public const string GOOGLE = "GOOGLE";
        public const string FACEBOOK = "FACEBOOK";
    }
}