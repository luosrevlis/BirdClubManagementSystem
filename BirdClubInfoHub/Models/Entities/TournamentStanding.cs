using System.ComponentModel.DataAnnotations;

namespace BirdClubInfoHub.Models.Entities
{
    public class TournamentStanding
    {
        public int Id { get; set; }

        [Required]
        public int TournamentId { get; set; }

        [Required]
        public int BirdId { get; set; }

        [Required, MinLength(3), MaxLength(3)]
        public string Placement { get; set; } = string.Empty;

        public Tournament Tournament { get; set; } = new Tournament();

        public Bird Bird { get; set; } = new Bird();
    }
}