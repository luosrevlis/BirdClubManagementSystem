namespace BirdClubManagementSystem.Models
{
    public class Post
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        public int CategoryID { get; set; }

        public string Contents { get; set; }
    }
}
