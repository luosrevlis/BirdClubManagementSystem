using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BirdClubInfoHub.Models
{
    public class MembershipRequest
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        [DisplayName ("Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

    }
}
