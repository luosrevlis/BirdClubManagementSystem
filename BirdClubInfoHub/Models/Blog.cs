namespace BirdClubInfoHub.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BlogCategoryId { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string Contents { get; set; } = string.Empty;

        public User User { get; set; } = new User();

        public BlogCategory BlogCategory { get; set; } = new BlogCategory();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
