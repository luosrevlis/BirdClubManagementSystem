using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;

        public ICollection<Post> Posts { get; set; } = new List<Post>();

        public ICollection<Bird> Birds { get; set; } = new List<Bird>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<FieldTripRegistration> FieldTripRegistrations { get; set; } = new List<FieldTripRegistration>();
    }
}
