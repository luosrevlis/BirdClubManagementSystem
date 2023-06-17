using System.ComponentModel.DataAnnotations;

namespace BirdClubInfoHub.Models
{
    public class Meeting : IClubEvent
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Description { get; set; } = string.Empty;

        public int Fee { get; set; } = 0;

        public string Status { get; set; } = string.Empty;

        public ICollection<MeetingRegistration> MeetingRegistrations { get; set; } = new List<MeetingRegistration>();
    }
}
