namespace BirdClubInfoHub.Models.DTOs
{
    public class TournamentStandingDTO
    {
        public int Id { get; set; }
        public int BirdId { get; set; }
        public string Placement { get; set; } = string.Empty;
        public TournamentDTO Tournament { get; set; } = null!;
    }
}
