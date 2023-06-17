namespace BirdClubInfoHub.Models
{
    public interface IClubEvent
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int Fee { get; set; }

        public string Status { get; set; }
    }
}
