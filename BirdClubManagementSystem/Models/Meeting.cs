using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models
{
    public class Meeting
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }
    }
}
