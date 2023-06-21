using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Models;
using Coravel.Invocable;
using FluentEmail.Core;

namespace BirdClubManagementSystem.BatchJobs
{
    public class EventReminder : IInvocable
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IFluentEmailFactory _emailFactory;

        public EventReminder(BcmsDbContext dbContext, IFluentEmailFactory emailFactory)
        {
            _dbContext = dbContext;
            _emailFactory = emailFactory;
        }

        public async Task Invoke()
        {
            await SendFieldTripReminder();
            await SendMeetingReminder();
            await SendTournamentReminder();
        }

        private async Task SendFieldTripReminder()
        {
            DateTime date = DateTime.Now.Date.AddDays(1);
            var mailList = from ftr in _dbContext.FieldTripRegistrations
                           join ft in _dbContext.FieldTrips on ftr.FieldTripId equals ft.Id
                           join user in _dbContext.Users on ftr.UserId equals user.Id
                           where ft.Date.Date == date
                           select (new
                           {
                               userName = user.Name,
                               userEmail = user.Email,
                               fieldTripName = ft.Name
                           });
            foreach (var mail in mailList)
            {
                IFluentEmail email = _emailFactory
                    .Create()
                    .To(mail.userEmail)
                    .Subject("Field trip reminder")
                    .Body($"Dear {mail.userName}, this is an automatic reminder that {mail.fieldTripName} is happening tomorrow");
                await email.SendAsync();
            }
        }

        private async Task SendMeetingReminder()
        {
            DateTime date = DateTime.Now.Date.AddDays(1);
            var mailList = from mr in _dbContext.MeetingRegistrations
                           join m in _dbContext.Meetings on mr.MeetingId equals m.Id
                           join user in _dbContext.Users on mr.UserId equals user.Id
                           where m.Date.Date == date
                           select (new
                           {
                               userName = user.Name,
                               userEmail = user.Email,
                               meetingName = m.Name
                           });
            foreach (var mail in mailList)
            {
                IFluentEmail email = _emailFactory
                    .Create()
                    .To(mail.userEmail)
                    .Subject("Meeting reminder")
                    .Body($"Dear {mail.userName}, this is an automatic reminder that {mail.meetingName} is happening tomorrow");
                await email.SendAsync();
            }
        }

        private async Task SendTournamentReminder()
        {
            DateTime date = DateTime.Now.Date.AddDays(1);
            var mailList = from tr in _dbContext.TournamentRegistrations
                           join t in _dbContext.Tournaments on tr.TournamentId equals t.Id
                           join bird in _dbContext.Birds on tr.BirdId equals bird.Id
                           join user in _dbContext.Users on bird.UserId equals user.Id
                           where t.Date.Date == date
                           select (new
                           {
                               userName = user.Name,
                               userEmail = user.Email,
                               tournamentName = t.Name
                           });
            mailList = mailList.Distinct();
            foreach (var mail in mailList)
            {
                IFluentEmail email = _emailFactory
                    .Create()
                    .To(mail.userEmail)
                    .Subject("Tournament reminder")
                    .Body($"Dear {mail.userName}, this is an automatic reminder that {mail.tournamentName} is happening tomorrow");
                await email.SendAsync();
            }
        }
    }
}
