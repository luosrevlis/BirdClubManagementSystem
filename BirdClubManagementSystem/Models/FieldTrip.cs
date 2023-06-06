namespace BirdClubManagementSystem.Models
{
    public class FieldTrip
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int Fee { get; set; }

        public bool IsAvailable { get; set; }

        public ICollection<FieldTripRegistration> FieldTripRegistrations { get; set; }
    }
}
