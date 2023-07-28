using System.ComponentModel.DataAnnotations;

namespace BirdClubInfoHub.Models.Entities
{
    public class FieldTripRegistration
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int FieldTripId { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public bool PaymentReceived { get; set; } = false;

        public User User { get; set; } = new User();

        public FieldTrip FieldTrip { get; set; } = new FieldTrip();
    }
}
