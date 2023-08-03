namespace BirdClubInfoHub.Models.Exceptions
{
    public class TournamentRegistrationException : Exception
    {
        public TournamentRegistrationException() { }
        public TournamentRegistrationException(string message) : base(message) { }
    }
}
