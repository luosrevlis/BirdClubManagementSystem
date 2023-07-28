namespace BirdClubManagementSystem.Models.DTOs
{
    public class FieldTripDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime RegOpenDate { get; set; }
        public DateTime RegCloseDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public string Address { get; set; } = null!;
        public int RegLimit { get; set; }
        public string Description { get; set; } = null!;
        public int Fee { get; set; }
        public string Status { get; set; } = null!;
        public string Highlights { get; set; } = string.Empty;
    }
}
