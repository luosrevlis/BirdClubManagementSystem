using Microsoft.EntityFrameworkCore;

namespace BirdClubManagementSystem.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public byte[] ProfilePicture { get; set; } = Array.Empty<byte>();

        public DateTime JoinDate { get; set; }

        public DateTime LastLogin { get; set; } = DateTime.Now;

        public DateTime ResetPasswordRequestTime { get; set; } = DateTime.Now;

        public string ResetPasswordCode { get; set; } = string.Empty;

        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();

        public ICollection<Bird> Birds { get; set; } = new List<Bird>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<FieldTripRegistration> FieldTripRegistrations { get; set; } = new List<FieldTripRegistration>();

        public ICollection<MeetingRegistration> MeetingRegistrations { get; set; } = new List<MeetingRegistration>();
    }
}
