using BirdClubManagementSystem.Models.Entities;
using BirdClubManagementSystem.Models.Exceptions;

namespace BirdClubManagementSystem.Validators
{
    public class BlogValidator
    {
        public static void Validate(Blog blog)
        {
            if (blog == null)
            {
                throw new ArgumentNullException(nameof(blog));
            }
            if (blog.Id < 1)
            {
                throw new BlogException($"{nameof(blog.Id)} is below 1");
            }
            if (blog.UserId < 1)
            {
                throw new BlogException($"{nameof(blog.UserId)} is below 1");
            }
            if (blog.DateCreated > DateTime.Now)
            {
                throw new BlogException($"{nameof(blog.DateCreated)} is in the future");
            }
            if (string.IsNullOrWhiteSpace(blog.Title))
            {
                throw new BlogException($"{nameof(blog.Title)} is null/empty/whitespace");
            }
            if (blog.Title.Length > 225)
            {
                throw new BlogException($"{nameof(blog.Title)} exceeds 255 characters");
            }
            if (string.IsNullOrWhiteSpace(blog.Contents))
            {
                throw new BlogException($"{nameof(blog.Contents)} is null/empty/whitespace");
            }
            if (string.IsNullOrWhiteSpace(blog.Status))
            {
                throw new BlogException($"{nameof(blog.Status)} is null/empty/whitespace");
            }
            if (blog.Status.Length != 3)
            {
                throw new BlogException($"{nameof(blog.Status)} has no length of 3");
            }
        }
    }
}
