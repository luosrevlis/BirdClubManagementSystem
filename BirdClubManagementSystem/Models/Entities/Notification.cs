namespace BirdClubManagementSystem.Models.Entities
{
    public class Notification
    {
        public IDictionary<string, bool> IsRoleSelected { get; set; } = new Dictionary<string, bool>()
        {
            { "ADM", false },
            { "STF", false },
            { "MEM", false },
            { "CUS", false }
        };

        public ICollection<string> Recipients { get; set; } = new HashSet<string>();

        public string Contents { get; set; } = string.Empty;
    }
}
