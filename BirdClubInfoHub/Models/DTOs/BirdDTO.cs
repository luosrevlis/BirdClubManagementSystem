namespace BirdClubInfoHub.Models.DTOs
{
    public class BirdDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = "No description";
        public string Species { get; set; } = "Unknown";
        public UserDTO User { get; set; } = new UserDTO();
    }
}
