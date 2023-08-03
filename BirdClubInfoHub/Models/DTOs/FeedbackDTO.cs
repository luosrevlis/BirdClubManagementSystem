namespace BirdClubInfoHub.Models.DTOs
{
    public class FeedbackDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = "No title";
        public string Contents { get; set; } = "No content";
        public UserDTO User { get; set; } = new UserDTO();
    }
}
