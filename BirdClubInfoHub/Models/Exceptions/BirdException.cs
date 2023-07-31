namespace BirdClubInfoHub.Models.Exceptions
{
    public class BirdException : Exception
    {
        public BirdException() { }
        public BirdException(string message) : base(message) { }
    }
}
