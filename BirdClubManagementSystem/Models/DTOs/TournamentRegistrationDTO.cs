﻿namespace BirdClubManagementSystem.Models.DTOs
{
    public class TournamentRegistrationDTO
    {
        public int Id { get; set; }
        public int BirdId { get; set; }
        public int TournamentId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool PaymentReceived { get; set; } = false;
    }
}
