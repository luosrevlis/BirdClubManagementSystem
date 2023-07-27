using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BirdClubInfoHub.Models
{
    public class TournamentRegistration
    {
        public int Id { get; set; }

        [Required]
        public int BirdId { get; set; }

        [Required]
        public int TournamentId { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required]
        public bool PaymentReceived { get; set; } = false;

        public Bird Bird { get; set; } = new Bird();

        public Tournament Tournament { get; set; } = new Tournament();
    }
}
