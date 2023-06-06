using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models
{
    public class Post
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        public int CategoryID { get; set; }

        [Required]
        public string Contents { get; set; } = string.Empty;

        public User User { get; set; } = new User();

        public PostCategory PostCategory { get; set; } = new PostCategory();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
