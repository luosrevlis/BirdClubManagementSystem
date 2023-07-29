namespace BirdClubInfoHub.Models.DTOs
{
    public class MeetingRegistrationDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MeetingId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool PaymentReceived { get; set; } = true;
        public MeetingDTO Meeting { get; set; } = null!;
    }
}
