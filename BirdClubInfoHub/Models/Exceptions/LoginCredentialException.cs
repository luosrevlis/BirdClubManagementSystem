namespace BirdClubInfoHub.Models.Exceptions
{
    public class LoginCredentialException : Exception
    {
        public LoginCredentialException() { }
        public LoginCredentialException(string message) : base(message) { }
    }
}
