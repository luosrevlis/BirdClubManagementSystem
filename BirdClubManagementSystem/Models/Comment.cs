using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int BlogId { get; set; }

        [Required, MinLength(1), MaxLength(1000)]
        // TO-DO: fe check max
        public string Contents { get; set; } = "No content";

        [Required]
        // TO-DO: be assign this field when submit
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; }

        public User User { get; set; } = new User();

        public Blog Blog { get; set; } = new Blog();
    }
}
