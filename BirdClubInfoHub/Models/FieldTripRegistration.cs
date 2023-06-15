using Microsoft.EntityFrameworkCore;

namespace BirdClubInfoHub.Models
{
    public class FieldTripRegistration
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int FieldTripId { get; set; }

        public bool PaymentReceived { get; set; } = false;

        public User User { get; set; } = new User();

        public FieldTrip FieldTrip { get; set; } = new FieldTrip();
    }
}
