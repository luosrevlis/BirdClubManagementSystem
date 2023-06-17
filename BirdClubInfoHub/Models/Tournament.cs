﻿using System.ComponentModel.DataAnnotations;

namespace BirdClubInfoHub.Models
{
    public class Tournament :IClubEvent
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Description { get; set; } = string.Empty;

        public int Fee { get; set; }

        public string Status { get; set; } = string.Empty;

        public ICollection<TournamentRegistration> TournamentRegistrations { get; set; } = new List<TournamentRegistration>();
    }
}
