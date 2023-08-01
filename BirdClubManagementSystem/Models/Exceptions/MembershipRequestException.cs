namespace BirdClubManagementSystem.Models.Exceptions
{
    public class MembershipRequestException : Exception
    {
        public MembershipRequestException() { }
        public MembershipRequestException(string message) : base(message) { }
    }
}
