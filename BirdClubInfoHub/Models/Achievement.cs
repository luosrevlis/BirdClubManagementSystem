namespace BirdClubInfoHub.Models
{
    public class Achievement
    {
        public int Id { get; set; }

        public string TournamentName { get; set; } = string.Empty;

        public string Rank { get; set; } = string.Empty;

        public ICollection<Bird> Bird { get; set; } = new List<Bird>();
    }
}
