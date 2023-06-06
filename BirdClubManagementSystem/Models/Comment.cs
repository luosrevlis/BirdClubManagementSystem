using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models
{
    public class Comment
    {
        public int UserId { get; set; }

        public int PostId { get; set; }

        [Required]
        public string Contents { get; set; }
    }
}
