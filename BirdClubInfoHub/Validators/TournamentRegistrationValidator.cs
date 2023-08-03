using BirdClubInfoHub.Models.Entities;
using BirdClubInfoHub.Models.Exceptions;

namespace BirdClubInfoHub.Validators
{
    public class TournamentRegistrationValidator
    {
        public static void Validate(TournamentRegistration tournamentRegistration)
        {
            if (tournamentRegistration == null)
            {
                throw new ArgumentNullException(nameof(tournamentRegistration));
            }
            if (tournamentRegistration.Id < 1)
            {
                throw new FeedbackException($"{nameof(tournamentRegistration.Id)} is below 1");
            }
            if (tournamentRegistration.BirdId < 1)
            {
                throw new FeedbackException($"{nameof(tournamentRegistration.BirdId)} is below 1");
            }
            if (tournamentRegistration.TournamentId < 1)
            {
                throw new FeedbackException($"{nameof(tournamentRegistration.TournamentId)} is below 1");
            }
            if (tournamentRegistration.DateCreated > DateTime.Now)
            {
                throw new CommentException($"{nameof(tournamentRegistration.DateCreated)} is in the future");
            }
        }
    }
}
