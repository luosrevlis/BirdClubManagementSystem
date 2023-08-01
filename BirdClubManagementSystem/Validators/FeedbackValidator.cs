using BirdClubManagementSystem.Models.Entities;
using BirdClubManagementSystem.Models.Exceptions;

namespace BirdClubManagementSystem.Validators
{
    public class FeedbackValidator
    {
        public static void Validate(Feedback feedback)
        {
            if (feedback == null)
            {
                throw new ArgumentNullException(nameof(feedback));
            }
            if (feedback.Id < 1)
            {
                throw new FeedbackException($"{nameof(feedback.Id)} is below 1");
            }
            if (feedback.UserId < 1)
            {
                throw new FeedbackException($"{nameof(feedback.UserId)} is below 1");
            }
            if (string.IsNullOrWhiteSpace(feedback.Title))
            {
                throw new FeedbackException($"{nameof(feedback.Title)} is null/empty/whitespace");
            }
            if (feedback.Title.Length > 255)
            {
                throw new FeedbackException($"{nameof(feedback.Title)} exceeds 255 characters");
            }
            if (string.IsNullOrWhiteSpace(feedback.Contents))
            {
                throw new FeedbackException($"{nameof(feedback.Contents)} is null/empty/whitespace");
            }
            if (feedback.Contents.Length > 1000)
            {
                throw new FeedbackException($"{nameof(feedback.Contents)} exceeds 1000 characters");
            }
        }
    }
}
