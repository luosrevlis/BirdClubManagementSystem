using BirdClubManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BirdClubManagementSystem.Data
{
    public class BcmsDbContext : DbContext
    {
        public BcmsDbContext(DbContextOptions<BcmsDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<PostCategory> PostCategories { get; set; }

        public DbSet<MembershipRequest> MembershipRequests { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Bird> Birds { get; set; }

        public DbSet<FieldTrip> FieldTrips { get; set; }

        public DbSet<Meeting> Meetings { get; set; }

        public DbSet<Tournament> Tournaments { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<FieldTripRegistration> FieldTripRegistrations { get; set; }

        public DbSet<MeetingRegistration> MeetingRegistrations { get; set; }

        public DbSet<TournamentRegistration> TournamentRegistrations { get; set; }

        public DbSet<TournamentStanding> TournamentStandings { get; set; }
    }
}
