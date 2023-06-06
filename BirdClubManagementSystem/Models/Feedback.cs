using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public User User { get; set; } = new User();
    }
}
