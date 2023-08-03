namespace BirdClubManagementSystem.Models.DTOs
{
    public class TournamentStandingDTO
    {
        public int Id { get; set; }
        public string Placement { get; set; } = string.Empty;
        public TournamentDTO Tournament { get; set; } = new TournamentDTO();
        public BirdDTO Bird { get; set; } = new BirdDTO();
    }
}
