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
            List<FieldTrip> fieldTrips = _dbContext.FieldTrips
                .Where(fieldTrip => fieldTrip.RegCloseDate <= DateTime.Now && fieldTrip.Status == "Open").ToList();
            foreach (FieldTrip fieldTrip in fieldTrips)
            {
                fieldTrip.Status = "Registration Closed";
                _dbContext.FieldTrips.Update(fieldTrip);
            }
            fieldTrips = _dbContext.FieldTrips.Where(fieldTrip => fieldTrip.StartDate <= DateTime.Now).ToList();
            foreach (FieldTrip fieldTrip in fieldTrips)
            {
                fieldTrip.Status = "Happening";
                _dbContext.FieldTrips.Update(fieldTrip);
            }
            await _dbContext.SaveChangesAsync();
        }

        private async Task UpdateMeetings()
        {
            List<Meeting> meetings = _dbContext.Meetings
                .Where(meeting => meeting.RegCloseDate <= DateTime.Now && meeting.Status == "Open").ToList();
            foreach (Meeting meeting in meetings)
            {
                meeting.Status = "Registration Closed";
                _dbContext.Meetings.Update(meeting);
            }
            meetings = _dbContext.Meetings.Where(meeting => meeting.StartDate <= DateTime.Now).ToList();
            foreach (Meeting meeting in meetings)
            {
                meeting.Status = "Happening";
                _dbContext.Meetings.Update(meeting);
            }
            await _dbContext.SaveChangesAsync();
        }

        private async Task UpdateTournaments()
        {
            List<Tournament> tournaments = _dbContext.Tournaments
                .Where(tournament => tournament.RegCloseDate <= DateTime.Now && tournament.Status == "Open").ToList();
            foreach (Tournament tournament in tournaments)
            {
                tournament.Status = "Registration Closed";
                _dbContext.Tournaments.Update(tournament);
            }
            tournaments = _dbContext.Tournaments.Where(tournament => tournament.StartDate <= DateTime.Now).ToList();
            foreach (Tournament tournament in tournaments)
            {
                tournament.Status = "Happening";
                _dbContext.Tournaments.Update(tournament);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
