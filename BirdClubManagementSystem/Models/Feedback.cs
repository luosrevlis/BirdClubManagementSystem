using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public User User { get; set; }
    }
}
