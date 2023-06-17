namespace BirdClubManagementSystem.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public User User { get; set; } = new User();
    }
}
