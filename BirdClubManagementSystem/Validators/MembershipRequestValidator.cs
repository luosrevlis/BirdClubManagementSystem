using BirdClubManagementSystem.Models.Entities;
using BirdClubManagementSystem.Models.Exceptions;

namespace BirdClubManagementSystem.Validators
{
    public class MembershipRequestValidator
    {
        public static void Validate(MembershipRequest membershipRequest)
        {
            if (membershipRequest == null)
            {
                throw new ArgumentNullException(nameof(membershipRequest));
            }
            if (membershipRequest.Id < 1)
            {
                throw new FeedbackException($"{nameof(membershipRequest.Id)} is below 1");
            }
            if (string.IsNullOrWhiteSpace(membershipRequest.Name))
            {
                throw new FeedbackException($"{nameof(membershipRequest.Name)} is null/empty/whitespace");
            }
            if (membershipRequest.Name.Length > 255)
            {
                throw new FeedbackException($"{nameof(membershipRequest.Name)} exceeds 255 characters");
            }
            if (string.IsNullOrWhiteSpace(membershipRequest.Address))
            {
                throw new FeedbackException($"{nameof(membershipRequest.Address)} is null/empty/whitespace");
            }
            if (membershipRequest.Address.Length > 255)
            {
                throw new FeedbackException($"{nameof(membershipRequest.Address)} exceeds 255 characters");
            }
            if (string.IsNullOrWhiteSpace(membershipRequest.PhoneNumber))
            {
                throw new FeedbackException($"{nameof(membershipRequest.PhoneNumber)} is null/empty/whitespace");
            }
            if (membershipRequest.PhoneNumber.Length > 15)
            {
                throw new FeedbackException($"{nameof(membershipRequest.PhoneNumber)} exceeds 255 characters");
            }
            if (string.IsNullOrWhiteSpace(membershipRequest.Email))
            {
                throw new FeedbackException($"{nameof(membershipRequest.Email)} is null/empty/whitespace");
            }
            if (membershipRequest.Email.Length > 50)
            {
                throw new FeedbackException($"{nameof(membershipRequest.Email)} exceeds 255 characters");
            }
            if (string.IsNullOrWhiteSpace(membershipRequest.Status))
            {
                throw new FeedbackException($"{nameof(membershipRequest.Status)} is null/empty/whitespace");
            }
            if (membershipRequest.Status.Length != 3)
            {
                throw new FeedbackException($"{nameof(membershipRequest.Status)} has no length of 3");
            }
        }
    }
}
