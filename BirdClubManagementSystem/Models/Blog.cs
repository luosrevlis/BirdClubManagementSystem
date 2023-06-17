namespace BirdClubManagementSystem.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        public int CategoryID { get; set; }

        public string Contents { get; set; } = string.Empty;

        public User User { get; set; } = new User();

        public BlogCategory BlogCategory { get; set; } = new BlogCategory();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
