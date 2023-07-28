﻿namespace BirdClubManagementSystem.Models.DTOs
{
    public class BirdDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = "No description";
        public string Species { get; set; } = "Unknown";
        public byte[] ProfilePicture { get; set; } = Array.Empty<byte>();
    }
}