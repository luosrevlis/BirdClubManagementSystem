using BirdClubManagementSystem.Models.Entities;
using BirdClubManagementSystem.Models.Exceptions;

namespace BirdClubManagementSystem.Validators
{
    public class ClubEventValidator
    {
#pragma warning disable CS8629 // Nullable value type may be null.
        public static void Validate(IClubEvent clubEvent)
        {
            if (clubEvent == null)
            {
                throw new ArgumentNullException(nameof(clubEvent));
            }
            if (clubEvent.Id < 1)
            {
                throw new ClubEventException($"{nameof(clubEvent.Id)} is below 1");
            }
            if (string.IsNullOrWhiteSpace(clubEvent.Name))
            {
                throw new ClubEventException($"{nameof(clubEvent.Name)} is null/empty/whitespace");
            }
            if (clubEvent.Name.Length > 255)
            {
                throw new ClubEventException($"{nameof(clubEvent.Name)} exceeds 255 characters");
            }
            if (clubEvent.RegOpenDate.HasValue)
            {
                if (clubEvent.RegOpenDate.Value > DateTime.Now.AddMonths(4))
                {
                    throw new ClubEventException($"{nameof(clubEvent.RegOpenDate)} is over 4 months in the future");
                }
                if (clubEvent.RegOpenDate > clubEvent.StartDate)
                {
                    throw new ClubEventException($"{nameof(clubEvent.RegOpenDate)} is after {nameof(clubEvent.StartDate)}");
                }
            }
            if (clubEvent.RegCloseDate.HasValue)
            {
                if (!clubEvent.RegOpenDate.HasValue)
                {
                    throw new ClubEventException($"{nameof(clubEvent.RegCloseDate)} exists while {nameof(clubEvent.RegOpenDate)} is null");
                }
                if (clubEvent.RegCloseDate.Value < clubEvent.RegOpenDate.Value)
                {
                    throw new ClubEventException($"{nameof(clubEvent.RegCloseDate)} ends before {nameof(clubEvent.RegOpenDate)}");
                }
                if (clubEvent.RegCloseDate > clubEvent.RegOpenDate.Value.AddMonths(4))
                {
                    throw new ClubEventException($"{nameof(clubEvent.RegCloseDate)} is over 4 months compare to {nameof(clubEvent.RegOpenDate)}");
                }
                if (clubEvent.RegCloseDate > clubEvent.StartDate)
                {
                    throw new ClubEventException($"{nameof(clubEvent.RegOpenDate)} is after {nameof(clubEvent.StartDate)}");
                }
            }
            if (clubEvent.StartDate > DateTime.Now.AddMonths (4))
            {
                throw new ClubEventException($"{nameof(clubEvent.StartDate)} is over 4 months in the future");
            }
            if (clubEvent.ExpectedEndDate.HasValue)
            {
                if (clubEvent.ExpectedEndDate.Value < clubEvent.StartDate)
                {
                    throw new ClubEventException($"{nameof(clubEvent.ExpectedEndDate)} ends before {nameof(clubEvent.StartDate)}");
                }
                if (clubEvent.ExpectedEndDate.Value > DateTime.Now.AddMonths(4))
                {
                    throw new ClubEventException($"{nameof(clubEvent.ExpectedEndDate)} is over 4 months compare to {nameof(clubEvent.StartDate)}");
                }
            }
            if (string.IsNullOrWhiteSpace(clubEvent.Address))
            {
                throw new ClubEventException($"{nameof(clubEvent.Address)} is null/empty/whitespace");
            }
            if (clubEvent.Address.Length > 255)
            {
                throw new ClubEventException($"{nameof(clubEvent.Address)} exceeds 255 characters");
            }
            if (clubEvent.RegLimit < 1 || clubEvent.RegLimit > 200)
            {
                throw new ClubEventException($"{nameof(clubEvent.RegLimit)} is below 1 or above 200");
            }
            if (string.IsNullOrWhiteSpace(clubEvent.Description))
            {
                throw new ClubEventException($"{nameof(clubEvent.Description)} is null/empty/whitespace");
            }
            if (clubEvent.Fee < 1 || clubEvent.Fee > 10000000)
            {
                throw new ClubEventException($"{nameof(clubEvent.Fee)} is below 1 or above 10000000");
            }
            if (string.IsNullOrWhiteSpace(clubEvent.Status))
            {
                throw new ClubEventException($"{nameof(clubEvent.Status)} is null/empty/whitespace");
            }
            if (clubEvent.Status.Length != 3)
            {
                throw new ClubEventException($"{nameof(clubEvent.Status)} has no length of 3");
            }
            if (clubEvent.Highlights != null && string.IsNullOrWhiteSpace(clubEvent.Highlights))
            {
                throw new ClubEventException($"{nameof(clubEvent.Address)} is null/empty/whitespace");
            }
        }
    }
}
