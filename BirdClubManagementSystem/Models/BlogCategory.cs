using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models
{
    public class BlogCategory
    {
        public int Id { get; set; }

        [Required, MinLength(1), MaxLength(255)]
        public string Name { get; set; } = null!;

        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
