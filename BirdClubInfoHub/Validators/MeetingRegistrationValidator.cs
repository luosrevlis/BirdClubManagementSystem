using BirdClubInfoHub.Models.Entities;
using BirdClubInfoHub.Models.Exceptions;

namespace BirdClubInfoHub.Validators
{
    public class MeetingRegistrationValidator
    {
        public static void Validate(MeetingRegistration meetingRegistration)
        {
            if (meetingRegistration == null)
            {
                throw new ArgumentNullException(nameof(meetingRegistration));
            }
            if (meetingRegistration.Id < 1)
            {
                throw new FeedbackException($"{nameof(meetingRegistration.Id)} is below 1");
            }
            if (meetingRegistration.UserId < 1)
            {
                throw new FeedbackException($"{nameof(meetingRegistration.UserId)} is below 1");
            }
            if (meetingRegistration.MeetingId < 1)
            {
                throw new FeedbackException($"{nameof(meetingRegistration.MeetingId)} is below 1");
            }
            if (meetingRegistration.DateCreated > DateTime.Now)
            {
                throw new CommentException($"{nameof(meetingRegistration.DateCreated)} is in the future");
            }
        }
    }
}
