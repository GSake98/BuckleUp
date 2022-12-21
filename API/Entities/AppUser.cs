using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        // To not make strings non-nullable and get no warning you have to
        // go to API.csproj and instead of enable we disable nullables
        public string UserName { get; set; } 
        // A way to make the password more secure (this is not ASP.NET Core Identity)
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}