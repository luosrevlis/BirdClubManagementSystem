using Microsoft.Build.Framework;

namespace BirdClubManagementSystem.Models
{
    public class FieldTrip
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        public int Fee { get; set; }

        public bool IsAvailable { get; set; }

        public ICollection<FieldTripRegistration> FieldTripRegistrations { get; set; } = new List<FieldTripRegistration>();
    }
}
