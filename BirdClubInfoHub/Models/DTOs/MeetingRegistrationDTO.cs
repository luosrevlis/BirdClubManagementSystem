namespace BirdClubInfoHub.Models.DTOs
{
    public class MeetingRegistrationDTO
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool PaymentReceived { get; set; } = true;
        public UserDTO User { get; set; } = new UserDTO();
        public MeetingDTO Meeting { get; set; } = new MeetingDTO();
    }
}
