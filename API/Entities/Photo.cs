using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")] // Specify what we want the table to be called in list attribute
    public class Photo
    {
        public int Id { get; set; } // Photo Id
        public string Url { get; set; } // Where to find photo
        public bool IsMain { get; set; } // Is the user's main photo?
        public string PublicId { get; set; }

        // To fully define the relationship with AppUser we must use AppUser's 
        // UserId and the class property as well for our parameters so we don't
        // get nullable AppUserId in our migration and we also get the onDelete behavior
        public int AppUserId {get;set;}
        public AppUser AppUser {get;set;}
    }
}