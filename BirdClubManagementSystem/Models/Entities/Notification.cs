namespace BirdClubManagementSystem.Models.Entities
{
    public class Notification
    {
        public IDictionary<string, bool> IsRoleSelected { get; set; } = new Dictionary<string, bool>()
        {
            { "Admin", false },
            { "Staff", false },
            { "Member", false },
            { "Custom", false }
        };

        public ICollection<string> Recipients { get; set; } = new HashSet<string>();

        public string Contents { get; set; } = string.Empty;
    }
}
