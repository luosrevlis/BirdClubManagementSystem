using Microsoft.EntityFrameworkCore;

namespace BirdClubManagementSystem.Models
{
    public class TournamentRegistration
    {
        public int Id { get; set; }

        public int BirdId { get; set; }

        public int TournamentId { get; set; }

        public bool PaymentReceived { get; set; }

        public Bird Bird { get; set; }

        public Tournament Tournament { get; set; }
    }
}
