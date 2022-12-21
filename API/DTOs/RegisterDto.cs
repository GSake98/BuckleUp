using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        // We have just the properties needed like the ones we want to use in
        // AccountController Register method
        // We use [Required] so we make sure they don't pass empty strings
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
        // These props will always go to lowercase because we pass them onto
        // JSON files which work with lowercase functionality
    }
}