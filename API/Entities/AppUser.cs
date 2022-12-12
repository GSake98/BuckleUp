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
    }
}