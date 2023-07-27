using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models
{
    public class MeetingRegistration
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MeetingId { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public bool PaymentReceived { get; set; } = true;

        public User User { get; set; } = new User();

        public Meeting Meeting { get; set; } = new Meeting();
    }
}
