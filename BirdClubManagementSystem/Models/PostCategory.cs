using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models
{
    public class PostCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Post> Posts { get; set; }
    }
}
