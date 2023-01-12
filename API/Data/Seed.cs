using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        // We use a static method here so we don't need to use an instance of the
        // Seed class and create an instance variable
        public static async Task SeedUsers(DataContext context)
        {
            // If we have any users in database return
            if (await context.Users.AnyAsync())
            {
                return;
            }
            else
            {
                var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

                // Just in case we made mistakes involving casing
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                // What type of thing we want to deserialize the Json into
                var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

                // Users need passwords generated
                foreach (var user in users)
                {
                    using var hmac = new HMACSHA512();

                    user.UserName = user.UserName.ToLower();
                    // IMPORTANT PASSWORD TO REMEMBER ---> c0mpl3x! <---
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("c0mpl3x!"));
                    user.PasswordSalt = hmac.Key;

                    // After the foreach loop add the user to the users list
                    context.Users.Add(user);
                }

                // Basically saves the changes
                await context.SaveChangesAsync();
            }
        }
    }
}