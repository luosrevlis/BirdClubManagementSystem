using Microsoft.Build.Framework;

namespace BirdClubInfoHub.Models
{
    public class FieldTrip : IClubEvent
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Description { get; set; } = string.Empty;

        public int Fee { get; set; }

        public string Status { get; set; } = string.Empty;

        public ICollection<FieldTripRegistration> FieldTripRegistrations { get; set; } = new List<FieldTripRegistration>();
    }
}
