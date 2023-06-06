namespace BirdClubManagementSystem.Models
{
    public class Post
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        public int CategoryID { get; set; }

        public string Contents { get; set; }

        public User User { get; set; }

        public PostCategory PostCategory { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
