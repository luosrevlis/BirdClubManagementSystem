namespace BirdClubInfoHub.Models.Statuses
{
    public class TournamentPlacements
    {
        public const string FirstPlace = "1ST";
        public const string SecondPlace = "2ND";
        public const string ThirdPlace = "3RD";
        public const string Participation = "PAR";

        public static string Convert (string code)
        {
            return code switch
            {
                FirstPlace => "First Place",
                SecondPlace => "Second Place",
                ThirdPlace => "Third Place",
                Participation => nameof(Participation),
                _ => throw new ArgumentException(code),
            };
        }
    }
}
