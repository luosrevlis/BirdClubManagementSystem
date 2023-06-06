using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
