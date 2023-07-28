using BirdClubInfoHub.Models.Entities;

namespace BirdClubInfoHub.Services
{
    public interface IAccountGenerationService
    {
        User GenerateAccount(MembershipRequest request, out LoginCredential credential);
    }
}
