namespace BirdClubManagementSystem.Models.DTOs
{
    public class TournamentStandingDTO
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int BirdId { get; set; }
        public string Placement { get; set; } = string.Empty;
    }
}
