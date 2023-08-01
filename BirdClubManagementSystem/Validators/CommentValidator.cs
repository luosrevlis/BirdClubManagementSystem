using BirdClubManagementSystem.Models.Entities;
using BirdClubManagementSystem.Models.Exceptions;

namespace BirdClubManagementSystem.Validators
{
    public class CommentValidator
    {
        public static void Validate(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }
            if (comment.Id < 1)
            {
                throw new BlogException($"{nameof(comment.Id)} is below 1");
            }
            if (comment.BlogId < 1)
            {
                throw new BlogException($"{nameof(comment.BlogId)} is below 1");
            }
            if (comment.UserId < 1)
            {
                throw new BlogException($"{nameof(comment.UserId)} is below 1");
            }
            if (string.IsNullOrWhiteSpace(comment.Contents))
            {
                throw new CommentException($"{nameof(comment.Contents)} is null/empty/whitespace");
            }
            if (comment.Contents.Length > 1000)
            {
                throw new CommentException($"{nameof(comment.Contents)} exceeds 1000 characters");
            }
            if (comment.CreatedDate > DateTime.Now)
            {
                throw new CommentException($"{nameof(comment.CreatedDate)} is in the future");
            }
            if (comment.ModifiedDate.HasValue && comment.ModifiedDate.Value > DateTime.Now)
            {
                throw new CommentException($"{nameof(comment.ModifiedDate)} is in the future");
            }
        }
    }
}
