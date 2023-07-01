namespace BirdClubManagementSystem.Models
{
    public class Bird
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Species { get; set; } = string.Empty;

        public byte[] ProfilePicture { get; set; } = Array.Empty<byte>();

        public User User { get; set; } = new User();

        public ICollection<TournamentRegistration> TournamentRegistrations { get; set; } = new List<TournamentRegistration>();

        public ICollection<TournamentStanding> TournamentStandings { get; set; } = new List<TournamentStanding>();
    }
}
