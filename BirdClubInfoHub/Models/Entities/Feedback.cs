using System.ComponentModel.DataAnnotations;

namespace BirdClubInfoHub.Models.Entities
{
    public class Feedback
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        // TO-DO: fe check max
        [Required, MinLength(1), MaxLength(255)]
        public string Title { get; set; } = "No title";

        [Required, MinLength(1), MaxLength(1000)]
        public string Contents { get; set; } = "No content";

        public User User { get; set; } = new User();
    }
}
