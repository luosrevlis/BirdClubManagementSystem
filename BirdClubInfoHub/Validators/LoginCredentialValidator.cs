using BirdClubInfoHub.Models.Entities;
using BirdClubInfoHub.Models.Exceptions;

namespace BirdClubInfoHub.Validators
{
    public class LoginCredentialValidator
    {
        public static void Validate(LoginCredential loginCredential)
        {
            if (loginCredential == null)
            {
                throw new ArgumentNullException(nameof(loginCredential));
            }
            if (string.IsNullOrWhiteSpace(loginCredential.Email))
            {
                throw new FeedbackException($"{nameof(loginCredential.Email)} is null/empty/whitespace");
            }
            if (string.IsNullOrWhiteSpace(loginCredential.Password))
            {
                throw new FeedbackException($"{nameof(loginCredential.Password)} is null/empty/whitespace");
            }
        }
    }
}
