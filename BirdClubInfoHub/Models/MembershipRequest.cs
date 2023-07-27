using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BirdClubInfoHub.Models
{
    public class MembershipRequest
    {
        public int Id { get; set; }

        [Required, MinLength(1), MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required, MinLength(1), MaxLength(255)]
        public string Address { get; set; } = string.Empty;

        [Required, MinLength(9), MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required, MinLength(3), MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(3), MaxLength(3)]
        public string Status { get; set; } = string.Empty;
    }
}
