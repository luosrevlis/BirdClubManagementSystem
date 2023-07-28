namespace BirdClubInfoHub.Models.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BlogId { get; set; }
        public string Contents { get; set; } = "No content";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
    }
}
