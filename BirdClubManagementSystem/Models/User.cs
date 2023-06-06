using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; }
    }
}
