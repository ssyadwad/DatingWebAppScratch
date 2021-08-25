using DatingWebAppScratch.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

namespace DatingWebAppScratch.Data
{
    public class Seed
    {
        private static int iterationCount = 10000;
        private const int _saltSize = 128;
        private const int _hashSize = 128;

        public static async Task SeedUsers(AppDbContext dbContext)
        {
            if(!await dbContext.Users.AnyAsync())
            {
                var userData = await System.IO.File.ReadAllTextAsync("Data/UserFeedData.json");
                var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

                byte[] Salt = new byte[_saltSize];
                var provider = new RNGCryptoServiceProvider();
                provider.GetNonZeroBytes(Salt);
                var rfc2898DeriveBytes = new Rfc2898DeriveBytes("Pa$$word", Salt, iterationCount);
                foreach (var user in users)
                {
                    user.UserName = user.UserName.ToLower();
                    user.Password = rfc2898DeriveBytes.GetBytes(_hashSize);
                    user.PasswordSalt = Salt;
                    dbContext.Users.Add(user);
                }
                dbContext.SaveChanges();
            }

        }
    }
}
