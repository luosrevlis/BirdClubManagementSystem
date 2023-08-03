namespace BirdClubInfoHub.Models.DTOs
{
    public class FieldTripRegistrationDTO
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool PaymentReceived { get; set; } = false;
        public UserDTO User { get; set; } = new UserDTO();
        public FieldTripDTO FieldTrip { get; set; } = new FieldTripDTO();
    }
}
