﻿namespace BirdClubInfoHub.Models.DTOs
{
    public class BlogDTO
    {
        public int Id { get; set; }
        public string BlogCategory { get; set; } = "Khác";
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string Title { get; set; } = "No title";
        public string Contents { get; set; } = "No content";
        public string Status { get; set; } = null!;
        public byte[] Thumbnail { get; set; } = Array.Empty<byte>();
        public UserDTO User { get; set; } = null!;
        public BlogCategoryDTO Category { get; set; } = null!;
        public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    }
}
