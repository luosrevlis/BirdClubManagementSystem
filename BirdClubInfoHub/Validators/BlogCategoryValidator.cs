using BirdClubInfoHub.Models.Entities;
using BirdClubInfoHub.Models.Exceptions;

namespace BirdClubInfoHub.Validators
{
    public class BlogCategoryValidator
    {
        public static void Validate(BlogCategory blogCategory)
        {
            if (blogCategory == null)
            {
                throw new ArgumentNullException(nameof(blogCategory));
            }
            if (blogCategory.Id < 1)
            {
                throw new BlogCategoryException($"{nameof(blogCategory.Id)} is below 1");
            }
            if (string.IsNullOrWhiteSpace(blogCategory.Name))
            {
                throw new BlogCategoryException($"{nameof(blogCategory.Name)} is null/empty/whitespace");
            }
            if (blogCategory.Name.Length > 255)
            {
                throw new BlogCategoryException($"{nameof(blogCategory.Name)} exceeds 255 characters");
            }
        }
    }
}
