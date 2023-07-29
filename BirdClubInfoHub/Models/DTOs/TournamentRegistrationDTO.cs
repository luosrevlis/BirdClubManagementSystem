namespace BirdClubInfoHub.Models.DTOs
{
    public class TournamentRegistrationDTO
    {
        public int Id { get; set; }
        public int BirdId { get; set; }
        public int TournamentId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool PaymentReceived { get; set; } = false;
        public TournamentDTO Tournament { get; set; } = null!;
        public BirdDTO Bird { get; set; } = null!;
    }
}
