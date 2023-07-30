namespace BirdClubManagementSystem.Models.DTOs
{
    public class BlogDTO
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string Title { get; set; } = "No title";
        public string Contents { get; set; } = "No content";
        public string Status { get; set; } = null!;
        public UserDTO User { get; set; } = null!;
        public BlogCategoryDTO BlogCategory { get; set; } = null!;
    }
}
