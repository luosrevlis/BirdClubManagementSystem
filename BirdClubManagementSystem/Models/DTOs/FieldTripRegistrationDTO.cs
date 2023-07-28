namespace BirdClubManagementSystem.Models.DTOs
{
    public class FieldTripRegistrationDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FieldTripId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool PaymentReceived { get; set; } = false;
    }
}
