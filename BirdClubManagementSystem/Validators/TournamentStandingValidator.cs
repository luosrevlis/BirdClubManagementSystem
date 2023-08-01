using BirdClubManagementSystem.Models.Entities;
using BirdClubManagementSystem.Models.Exceptions;

namespace BirdClubManagementSystem.Validators
{
    public class TournamentStandingValidator
    {
        public static void Validate(TournamentStanding tournamentStanding)
        {
            if (tournamentStanding == null)
            {
                throw new ArgumentNullException(nameof(tournamentStanding));
            }
            if (tournamentStanding.Id < 1)
            {
                throw new FeedbackException($"{nameof(tournamentStanding.Id)} is below 1");
            }
            if (tournamentStanding.BirdId < 1)
            {
                throw new FeedbackException($"{nameof(tournamentStanding.BirdId)} is below 1");
            }
            if (tournamentStanding.TournamentId < 1)
            {
                throw new FeedbackException($"{nameof(tournamentStanding.TournamentId)} is below 1");
            }
            if (string.IsNullOrWhiteSpace(tournamentStanding.Placement))
            {
                throw new FeedbackException($"{nameof(tournamentStanding.Placement)} is null/empty/whitespace");
            }
            if (tournamentStanding.Placement.Length != 3)
            {
                throw new FeedbackException($"{nameof(tournamentStanding.Placement)} has no length of 3");
            }
        }
    }
}
