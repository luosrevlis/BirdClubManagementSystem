using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BirdClubInfoHub.Models
{
    public class MembershipRequest
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}
