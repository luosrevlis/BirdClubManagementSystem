﻿namespace BirdClubInfoHub.Models
{
    public interface IClubEvent
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public DateTime Date { get; set; }
    }
}
