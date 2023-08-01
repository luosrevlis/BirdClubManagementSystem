using BirdClubManagementSystem.Models.Entities;
using BirdClubManagementSystem.Models.Exceptions;

namespace BirdClubManagementSystem.Validators
{
    public class BirdValidator
    {
        public static void Validate(Bird bird)
        {
            if (bird == null)
            {
                throw new ArgumentNullException(nameof(bird));
            }
            if (bird.Id <= 1)
            {
                throw new BirdException($"{nameof(bird.Id)} is below 1");
            }
            if (bird.UserId <= 1)
            {
                throw new BirdException($"{nameof(bird.UserId)} is below 1");
            }
            if (string.IsNullOrWhiteSpace(bird.Name))
            {
                throw new BirdException($"{nameof(bird.Name)} is null/empty/whitespace");
            }
            if (string.IsNullOrWhiteSpace(bird.Description))
            {
                throw new BirdException($"{nameof(bird.Description)} is null/empty/whitespace");
            }
            if (string.IsNullOrWhiteSpace(bird.Species))
            {
                throw new BirdException($"{nameof(bird.Species)} is null/empty/whitespace");
            }
        }
    }
}
