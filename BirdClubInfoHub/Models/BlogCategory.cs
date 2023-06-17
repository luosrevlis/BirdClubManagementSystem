using System.ComponentModel.DataAnnotations;

namespace BirdClubInfoHub.Models
{
    public class BlogCategory
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
