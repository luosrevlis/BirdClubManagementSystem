using System.ComponentModel.DataAnnotations;

namespace BirdClubInfoHub.Models.Entities
{
    public class MeetingRegistration
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int MeetingId { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public bool PaymentReceived { get; set; } = true;

        public User User { get; set; } = new User();

        public Meeting Meeting { get; set; } = new Meeting();
    }
}
