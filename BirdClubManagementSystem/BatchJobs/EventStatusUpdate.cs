using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Models;
using Coravel.Invocable;

namespace BirdClubManagementSystem.BatchJobs
{
    public class EventStatusUpdate : IInvocable
    {
        private readonly BcmsDbContext _dbContext;

        public EventStatusUpdate(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Invoke()
        {
            await UpdateFieldTrips();
            await UpdateMeetings();
            await UpdateTournaments();
        }

        private async Task UpdateFieldTrips()
        {
            List<FieldTrip> fieldTrips = _dbContext.FieldTrips.Where(fieldTrip => fieldTrip.Date <= DateTime.Now).ToList();
            foreach (var fieldTrip in fieldTrips)
            {
                fieldTrip.Status = "Happening";
                _dbContext.FieldTrips.Update(fieldTrip);
            }
            await _dbContext.SaveChangesAsync();
        }

        private async Task UpdateMeetings()
        {
            List<Meeting> meetings = _dbContext.Meetings.Where(meeting => meeting.Date <= DateTime.Now).ToList();
            foreach (Meeting meeting in meetings)
            {
                meeting.Status = "Happening";
                _dbContext.Meetings.Update(meeting);
            }
            await _dbContext.SaveChangesAsync();
        }

        private async Task UpdateTournaments()
        {
            List<Tournament> tournaments = _dbContext.Tournaments.Where(tournament => tournament.Date <= DateTime.Now).ToList();
            foreach (Tournament tournament in tournaments)
            {
                tournament.Status = "Happening";
                _dbContext.Tournaments.Update(tournament);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
