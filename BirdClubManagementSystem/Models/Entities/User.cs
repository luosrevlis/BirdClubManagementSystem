using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }

        [Required, MinLength(3), MaxLength(50)]
        public string Email { get; set; } = null!;

        [Required, MinLength(84), MaxLength(86)]
        public string Password { get; set; } = null!;

        [Required, MinLength(1), MaxLength(255)]
        public string Name { get; set; } = null!;

        [Required, MinLength(1), MaxLength(255)]
        public string Address { get; set; } = null!;

        [Required, MinLength(9), MaxLength(15)]
        public string Phone { get; set; } = null!;

        [Required, MinLength(3), MaxLength(3)]
        public string Role { get; set; } = null!;

        public byte[] ProfilePicture { get; set; } = Array.Empty<byte>();

        [Required]
        public DateTime JoinDate { get; set; }

        public DateTime? LastLogin { get; set; }

        public DateTime? ResetPasswordRequestTime { get; set; }

        [MaxLength(6)]
        public string ResetPasswordCode { get; set; } = string.Empty;

        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();

        public ICollection<Bird> Birds { get; set; } = new List<Bird>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<FieldTripRegistration> FieldTripRegistrations { get; set; } = new List<FieldTripRegistration>();

        public ICollection<MeetingRegistration> MeetingRegistrations { get; set; } = new List<MeetingRegistration>();
    }
}
