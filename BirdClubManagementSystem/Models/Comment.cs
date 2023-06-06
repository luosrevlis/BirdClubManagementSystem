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
        public string Contents { get; set; }

        public User User { get; set; }

        public Post Post { get; set; }
    }
}
