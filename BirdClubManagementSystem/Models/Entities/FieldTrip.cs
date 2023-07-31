using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models.Entities
{
    public class FieldTrip : IClubEvent
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
        public string Address { get; set; } = "At Club";

        [Required, Range(1, 200)]
        public int RegLimit { get; set; }

        [Required, MinLength(1)]
        public string Description { get; set; } = "No description";

        [Required, Range(1, 10000000)]
        public int Fee { get; set; }

        [Required, MinLength(3), MaxLength(3)]
        public string Status { get; set; } = null!;

        public string? Highlights { get; set; }

        public ICollection<FieldTripRegistration> FieldTripRegistrations { get; set; } = new List<FieldTripRegistration>();
    }
}
