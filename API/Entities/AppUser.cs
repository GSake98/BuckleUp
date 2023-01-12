using System.ComponentModel.DataAnnotations;
using API.Extensions;

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
        // New type of datatype that allows us only to use the date of something
        public DateOnly DateOfBirth { get; set; }
        public string KnownAs { get; set; } // Can be different from username obviously
        // Utc knows what time zone each location is so we prefer to use that
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        // Various strings for informations regarding location, hobbies and matches
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        // List of the user photos, also its advisable to initialize it so instead of
        // null we get an empty array as the result
        public List<Photo> Photos {get;set;} = new();

        /* CAUSES AUTOMAPPER ERRORS
        public int GetAge()
        {
            return DateOfBirth.CalculateAge(); // Extension Method
        }
        /**/
    }
}