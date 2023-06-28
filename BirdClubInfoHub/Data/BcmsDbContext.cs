using BirdClubInfoHub.Models;
using Microsoft.EntityFrameworkCore;

namespace BirdClubInfoHub.Data
{
    public class BcmsDbContext : DbContext
    {
        public BcmsDbContext(DbContextOptions<BcmsDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<BlogCategory> BlogCategories { get; set; }

        public DbSet<MembershipRequest> MembershipRequests { get; set; }

        public DbSet<Blog> Blogs { get; set; }

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

        public DbSet<BirdClubInfoHub.Models.Achievement> Achievement { get; set; } = default!;
    }
}
