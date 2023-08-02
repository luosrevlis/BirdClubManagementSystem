namespace BirdClubManagementSystem.Models.Statuses
{
    public class UserRoles
    {
        public const string Admin = "ADM";
        public const string Member = "MEM";
        public const string Staff = "STF";
        public const string Custom = "CTM";

        public static string Convert(string code)
        {
            return code switch
            {
                Admin => nameof(Admin),
                Member => nameof(Member),
                Staff => nameof(Staff),
                Custom => nameof(Custom),
                _ => throw new ArgumentException(code),
            };
        }
    }
}
