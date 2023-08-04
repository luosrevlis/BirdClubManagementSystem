using BirdClubInfoHub.Models.Entities;
using BirdClubInfoHub.Models.Statuses;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

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
                Role = UserRoles.Member,
                JoinDate = DateTime.Now
            };
            string randomString = Convert.ToBase64String(RandomNumberGenerator.GetBytes(4)).Remove(6);
            credential = new LoginCredential
            {
                Email = request.Email,
                Password = $"{randomString}@{new Random().Next(10000)}"
            };
            PasswordHasher<User> passwordHasher = new();
            user.Password = passwordHasher.HashPassword(user, credential.Password);
            return user;
        }
    }
}
