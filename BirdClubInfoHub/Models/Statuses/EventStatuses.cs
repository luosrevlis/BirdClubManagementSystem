namespace BirdClubInfoHub.Models.Statuses
{
    public class EventStatuses
    {
        public const string RegOpened = "ROP";
        public const string RegClosed = "RCL";
        public const string Start = "STA";
        public const string Ended = "END";
        public const string Cancelled = "CAN";

        public static string Convert(string code)
        {
            return code switch
            {
                RegOpened => "Registration Opened",
                RegClosed => "Registration Closed",
                Start => "Event started",
                Ended => "Event ended",
                Cancelled => nameof(Cancelled),
                _ => throw new ArgumentException(code),
            };
        }
    }
}
