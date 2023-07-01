using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BirdClubInfoHub.Models
{
    public class Comment
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }

        public int BlogId { get; set; }

        public string Contents { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public User User { get; set; } = new User();

        public Blog Blog { get; set; } = new Blog();
    }
}
