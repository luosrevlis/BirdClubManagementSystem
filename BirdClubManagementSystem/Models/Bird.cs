﻿using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models
{
    public class Bird
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required, MinLength(1), MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = "No description";

        public string Species { get; set; } = "Unknown";

        public byte[] ProfilePicture { get; set; } = Array.Empty<byte>();

        public User User { get; set; } = new User();

        public ICollection<TournamentRegistration> TournamentRegistrations { get; set; } = new List<TournamentRegistration>();

        public ICollection<TournamentStanding> TournamentStandings { get; set; } = new List<TournamentStanding>();
    }
}
