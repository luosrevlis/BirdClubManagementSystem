namespace BirdClubInfoHub.Models.Exceptions
{
    public class FeedbackException : Exception
    {
        public FeedbackException() { }
        public FeedbackException(string message) : base(message) { }
    }
}
