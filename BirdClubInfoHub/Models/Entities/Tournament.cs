﻿using System.ComponentModel.DataAnnotations;

namespace BirdClubInfoHub.Models.Entities
{
    public class Tournament : IClubEvent
    {
        public int Id { get; set; }

        [Required, MinLength(1), MaxLength(255)]
        public string Name { get; set; } = null!;

        public DateTime? RegOpenDate { get; set; } = null!;

        public DateTime? RegCloseDate { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime? ExpectedEndDate { get; set; } = null!;

        [Required, MinLength(1), MaxLength(255)]
        public string Address { get; set; } = null!;

        [Required, Range(1, 200)]
        public int RegLimit { get; set; }

        [Required, MinLength(1)]
        public string Description { get; set; } = null!;

        [Required, Range(1, 10000000)]
        public int Fee { get; set; }

        [Required, MinLength(3), MaxLength(3)]
        public string Status { get; set; } = null!;

        public string? Highlights { get; set; } = null!;

        public ICollection<TournamentRegistration> TournamentRegistrations { get; set; } = new List<TournamentRegistration>();

        public ICollection<TournamentStanding> TournamentStandings { get; set; } = new List<TournamentStanding>();
    }
}
