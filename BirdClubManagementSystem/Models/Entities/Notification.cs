using BirdClubManagementSystem.Models.Statuses;

namespace BirdClubManagementSystem.Models.Entities
{
    public class Notification
    {
        public IDictionary<string, bool> IsRoleSelected { get; set; } = new Dictionary<string, bool>()
        {
            { UserRoles.Admin, false },
            { UserRoles.Staff, false },
            { UserRoles.Member, false },
            { UserRoles.Custom, false }
        };

        public ICollection<string> Recipients { get; set; } = new HashSet<string>();

        public string Contents { get; set; } = string.Empty;
    }
}
