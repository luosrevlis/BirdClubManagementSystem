﻿using System.ComponentModel.DataAnnotations;

namespace BirdClubInfoHub.Models.Entities
{
    public class Meeting : IClubEvent
    {
        public int Id { get; set; }

        [Required, MinLength(1), MaxLength(255)]
        public string Name { get; set; } = null!;

        public DateTime? RegOpenDate { get; set; }

        public DateTime? RegCloseDate { get; set; }

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime? ExpectedEndDate { get; set; }

        [Required, MinLength(1), MaxLength(255)]
        public string Address { get; set; } = null!;

        [Required, Range(1, 200)]
        public int RegLimit { get; set; }

        [Required, MinLength(1)]
        public string Description { get; set; } = "No description";

        [Required]
        public int Fee { get; set; } = 0;

        [Required, MinLength(3), MaxLength(3)]
        public string Status { get; set; } = null!;

        public string? Highlights { get; set; }

        public ICollection<MeetingRegistration> MeetingRegistrations { get; set; } = new List<MeetingRegistration>();
    }
}
