using System.ComponentModel.DataAnnotations;

namespace BirdClubInfoHub.Models
{
    public class Tournament :IClubEvent
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        public int Fee { get; set; }

        public bool IsAvailable { get; set; }

        public ICollection<TournamentRegistration> TournamentRegistrations { get; set; } = new List<TournamentRegistration>();
    }
}
