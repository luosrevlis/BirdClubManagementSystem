using BirdClubManagementSystem.Models.Entities;
using BirdClubManagementSystem.Models.Exceptions;

namespace BirdClubManagementSystem.Validators
{
    public class FieldTripRegistrationValidator
    {
        public static void Validate(FieldTripRegistration fieldTripRegistration)
        {
            if (fieldTripRegistration == null)
            {
                throw new ArgumentNullException(nameof(fieldTripRegistration));
            }
            if (fieldTripRegistration.Id < 1)
            {
                throw new FeedbackException($"{nameof(fieldTripRegistration.Id)} is below 1");
            }
            if (fieldTripRegistration.UserId < 1)
            {
                throw new FeedbackException($"{nameof(fieldTripRegistration.UserId)} is below 1");
            }
            if (fieldTripRegistration.FieldTripId < 1)
            {
                throw new FeedbackException($"{nameof(fieldTripRegistration.FieldTripId)} is below 1");
            }
            if (fieldTripRegistration.DateCreated > DateTime.Now)
            {
                throw new CommentException($"{nameof(fieldTripRegistration.DateCreated)} is in the future");
            }
        }
    }
}
