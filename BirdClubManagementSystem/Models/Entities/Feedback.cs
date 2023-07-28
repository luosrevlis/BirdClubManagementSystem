using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models.Entities
{
    public class Feedback
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required, MinLength(1), MaxLength(255)]
        // TO-DO: fe check max
        public string Title { get; set; } = "No title";

        [Required, MinLength(1), MaxLength(1000)]
        public string Contents { get; set; } = "No content";

        public User User { get; set; } = new User();
    }
}
