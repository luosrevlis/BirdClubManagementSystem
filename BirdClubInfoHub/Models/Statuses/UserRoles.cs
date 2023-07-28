namespace BirdClubInfoHub.Models.Statuses
{
    public class UserRoles
    {
        public const string Admin = "ADM";
        public const string Member = "MEM";
        public const string Staff = "STF";

        public static string Convert(String code)
        {
            return code switch
            {
                Admin => nameof(Admin),
                Member => nameof(Member),
                Staff => nameof(Staff),
                _ => throw new ArgumentException(code),
            };
        }
    }
}
