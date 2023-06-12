namespace BirdClubManagementSystem.Models
{
    public class TournamentStanding
    {
        public int Id { get; set; }

        public int TournamentId { get; set; }

        public int BirdId { get; set; }

        public string Placement { get; set; } = string.Empty;

        public Tournament Tournament { get; set; } = new Tournament();

        public Bird Bird { get; set; } = new Bird();
    }
}
