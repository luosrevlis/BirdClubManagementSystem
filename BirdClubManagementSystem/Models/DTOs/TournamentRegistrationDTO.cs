namespace BirdClubManagementSystem.Models.DTOs
{
    public class TournamentRegistrationDTO
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool PaymentReceived { get; set; } = false;
        public BirdDTO Bird { get; set; } = new BirdDTO();
        public TournamentDTO Tournament { get; set; } = new TournamentDTO();
    }
}
