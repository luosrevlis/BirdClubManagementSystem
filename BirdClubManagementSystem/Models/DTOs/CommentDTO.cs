namespace BirdClubManagementSystem.Models.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Contents { get; set; } = "No content";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public UserDTO User { get; set; } = new UserDTO();
        public BlogDTO Blog { get; set; } = new BlogDTO();
    }
}
