﻿namespace BirdClubInfoHub.Models.DTOs
{
    public class TournamentRegistrationDTO
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool PaymentReceived { get; set; } = false;
        public TournamentDTO Tournament { get; set; } = new TournamentDTO();
        public BirdDTO Bird { get; set; } = new BirdDTO();
    }
}
