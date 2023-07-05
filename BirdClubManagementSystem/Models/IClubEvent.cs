namespace BirdClubManagementSystem.Models
{
    public interface IClubEvent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime RegistrationCloseDate { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int Fee { get; set; }

        public string Status { get; set; }

        public string Highlights { get; set; }
    }
}
