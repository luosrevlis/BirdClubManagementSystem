namespace BirdClubInfoHub.Models.DTOs
{
    public class TournamentDTO : IClubEventDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? RegOpenDate { get; set; }
        public DateTime? RegCloseDate { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? ExpectedEndDate { get; set; }
        public string Address { get; set; } = null!;
        public int RegLimit { get; set; }
        public string Description { get; set; } = "No description";
        public int Fee { get; set; }
        public string Status { get; set; } = null!;
        public string? Highlights { get; set; }
        public ICollection<TournamentRegistrationDTO> TournamentRegistrations { get; set; }
            = new List<TournamentRegistrationDTO>();
        public ICollection<TournamentStandingDTO> TournamentStandings { get; set; }
            = new List<TournamentStandingDTO>();
    }
}
