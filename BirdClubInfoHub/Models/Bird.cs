using System.ComponentModel.DataAnnotations;

namespace BirdClubInfoHub.Models
{
    public class Bird
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        public string Species { get; set; } = string.Empty;

        public User User { get; set; } = new User();

        public ICollection<TournamentRegistration> TournamentRegistrations { get; set; } = new List<TournamentRegistration>();
    }
}
