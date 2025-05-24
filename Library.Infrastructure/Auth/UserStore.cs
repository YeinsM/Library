using Library.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Library.Infrastructure.Auth
{
    public static class UserStore
    {
        private static readonly PasswordHasher<User> _hasher = new();

        private static readonly List<User> Users = [
                new User
        {
            Username = "admin",
            PasswordHash = _hasher.HashPassword(null!, "admin123"),
            Role = "admin"
        },
        new User
        {
            Username = "user",
            PasswordHash = _hasher.HashPassword(null!, "user123"),
            Role = "user"
        }
            ];

        public static User? FindByUsername(string username) =>
            Users.FirstOrDefault(u => u.Username == username);

        public static bool VerifyPassword(User user, string password)
        {
            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}