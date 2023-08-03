using BirdClubManagementSystem.Models.Entities;
using BirdClubManagementSystem.Models.Exceptions;

namespace BirdClubManagementSystem.Validators
{
    public class UserValidator
    {
        public static void Validate(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (user.Id < 1)
            {
                throw new FeedbackException($"{nameof(user.Id)} is below 1");
            }
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new ClubEventException($"{nameof(user.Email)} is null/empty/whitespace");
            }
            if (user.Email.Length > 50)
            {
                throw new ClubEventException($"{nameof(user.Email)} exceeds 50 characters");
            }
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                throw new ClubEventException($"{nameof(user.Password)} is null/empty/whitespace");
            }
            if (user.Password.Length > 86)
            {
                throw new ClubEventException($"{nameof(user.Password)} exceeds 86 characters");
            }
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                throw new ClubEventException($"{nameof(user.Name)} is null/empty/whitespace");
            }
            if (user.Name.Length > 255)
            {
                throw new ClubEventException($"{nameof(user.Name)} exceeds 255 characters");
            }
            if (string.IsNullOrWhiteSpace(user.Address))
            {
                throw new ClubEventException($"{nameof(user.Address)} is null/empty/whitespace");
            }
            if (user.Address.Length > 255)
            {
                throw new ClubEventException($"{nameof(user.Address)} exceeds 255 characters");
            }
            if (string.IsNullOrWhiteSpace(user.Phone))
            {
                throw new ClubEventException($"{nameof(user.Phone)} is null/empty/whitespace");
            }
            if (user.Phone.Length > 15)
            {
                throw new ClubEventException($"{nameof(user.Phone)} exceeds 15 characters");
            }
            if (string.IsNullOrWhiteSpace(user.Role))
            {
                throw new ClubEventException($"{nameof(user.Role)} is null/empty/whitespace");
            }
            if (user.Role.Length != 3)
            {
                throw new ClubEventException($"{nameof(user.Role)} has no length of 3");
            }
            if (user.JoinDate > DateTime.Now)
            {
                throw new ClubEventException($"{nameof(user.JoinDate)} is in the future");
            }
            if (user.LastLogin.HasValue && user.LastLogin.Value > DateTime.Now)
            {
                throw new ClubEventException($"{nameof(user.LastLogin)} is in the future");
            }
            if (user.ResetPasswordRequestTime.HasValue && user.ResetPasswordRequestTime.Value > DateTime.Now)
            {
                throw new ClubEventException($"{nameof(user.ResetPasswordRequestTime)} is in the future");
            }
            if (user.ResetPasswordCode != null)
            {
                if (string.IsNullOrWhiteSpace(user.ResetPasswordCode))
                {
                    throw new ClubEventException($"{nameof(user.ResetPasswordCode)} is null/empty/whitespace");
                }
                if (user.ResetPasswordCode.Length != 6)
                {
                    throw new ClubEventException($"{nameof(user.Phone)} has no length of 6");
                }
            }
        }
    }
}
