namespace BirdClubManagementSystem.Models.Statuses
{
    public class BlogStatuses
    {
        public const string Pending = "PEN";
        public const string Accepted = "ACC";
        public const string Rejected = "REJ";

        public static string Convert(String code)
        {
            return code switch
            {
                Pending => nameof(Pending),
                Accepted => nameof(Accepted),
                Rejected => nameof(Rejected),
                _ => throw new ArgumentException(code),
            };
        }
    }
}
