using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required, MinLength(1), MaxLength(100)]
        // TO-DO: fe check max
        public string Title { get; set; } = string.Empty;

        [Required, MinLength(1)]
        public string Contents { get; set; } = string.Empty;

        public User User { get; set; } = new User();
    }
}
