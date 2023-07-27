using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models
{
    public class BlogCategory
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(1), MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
