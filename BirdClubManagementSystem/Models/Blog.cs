﻿using System.ComponentModel.DataAnnotations;

namespace BirdClubManagementSystem.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int BlogCategoryId { get; set; }

        [Required]
        // TO-DO: assign this field when user submit
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required, MinLength(1), MaxLength(255)]
        public string Title { get; set; } = "No title";

        [Required, MinLength(1)]
        public string Contents { get; set; } = string.Empty;

        [Required, MinLength(3), MaxLength(3)]
        public string Status { get; set; } = string.Empty;

        public byte[] Thumbnail { get; set; } = Array.Empty<byte>();

        public User User { get; set; } = new User();

        public BlogCategory BlogCategory { get; set; } = new BlogCategory();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
