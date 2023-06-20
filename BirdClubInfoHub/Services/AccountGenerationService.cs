using BirdClubInfoHub.Models;
using Microsoft.AspNetCore.Identity;

namespace BirdClubInfoHub.Services
{
    public class AccountGenerationService : IAccountGenerationService
    {
        public User GenerateAccount(MembershipRequest request, out LoginCredential credential)
        {
            User user = new()
            {
                Email = request.Email,
                Name = request.Name,
                Address = request.Address,
                Phone = request.PhoneNumber,
                Role = "Member"
            };
            credential = new LoginCredential
            {
                Email = request.Email,
                Password = $"{user.Name}@{user.Id}"
            };
            PasswordHasher<User> passwordHasher = new();
            user.Password = passwordHasher.HashPassword(user, credential.Password);
            return user;
        }
    }
}
