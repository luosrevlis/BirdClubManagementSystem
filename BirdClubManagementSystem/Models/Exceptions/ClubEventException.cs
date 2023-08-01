namespace BirdClubManagementSystem.Models.Exceptions
{
    public class ClubEventException : Exception
    {
        public ClubEventException() { }
        public ClubEventException(string message) : base(message) { }
    }
}
