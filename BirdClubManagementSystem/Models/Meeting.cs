namespace BirdClubManagementSystem.Models
{
    public class Meeting
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public bool IsAvailable { get; set; }
    }
}
