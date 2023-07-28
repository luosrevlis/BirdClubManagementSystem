namespace BirdClubManagementSystem.Models.DTOs
{
    public class FeedbackDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = "No title";
        public string Contents { get; set; } = "No content";
    }
}
