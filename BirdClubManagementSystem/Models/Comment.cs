using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models
{
    public class Comment
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }

        public int PostId { get; set; }

        [Required]
        public string Contents { get; set; } = string.Empty;

        public User User { get; set; } = new User();

        public Post Post { get; set; } = new Post();
    }
}
