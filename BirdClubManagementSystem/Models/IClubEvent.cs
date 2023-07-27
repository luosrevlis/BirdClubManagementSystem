namespace BirdClubManagementSystem.Models
{
    public interface IClubEvent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime RegOpenDate { get; set; }

        public DateTime RegCloseDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ExpectedEndDate { get; set; }

        public string Address { get; set; }

        public int RegLimit { get; set; }

        public string Description { get; set; }

        public int Fee { get; set; }

        public string Status { get; set; }

        public string Highlights { get; set; }
    }
}
