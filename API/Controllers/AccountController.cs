using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseAPIController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        // It's the framework's job depending on what we specified in our service to look for it
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")] // POST Request -> api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            // User will be stopped by our [Required] fields in RegisterDto anyway
            // if they don't make sure they have the fields filled
            if (await UserExists(registerDto.Username))
            {
                return BadRequest("Username is taken."); // We pass what the error is
            }

            // This is how we hash the password if we don't do it custom
            // The using keyword means that after we are done with this class
            // it will dispose of it instantly without losing more memory
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                // We are saving the user's username to lower case to make sure if case-sensitive
                UserName = registerDto.Username.ToLower(),
                // GetBytes method can't get an argument that is null even if it's a string
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            // This "_context.Users.Add(user);" just tracks our new entity in memory
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user) // We create a token for the user
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            // We can either use FirstOrDefaultAsync or SingleOrDefaultAsync for the same job
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == loginDto.Username.ToLower());
            if (user == null)
            {
                return Unauthorized("Invalid username.");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            // ENUMERABLE COMPARE CODE

            if (Enumerable.SequenceEqual(computedHash, user.PasswordHash))
            {
                return new UserDto
                {
                    Username = user.UserName,
                    Token = _tokenService.CreateToken(user)
                };
            }

            return Unauthorized("Invalid password");

            /* FOR LOOP CODE - VERBOSE
                for(int i=0; i < computedHash.Length; i++)
                {
                    // If the element of computedHash isn't equal to PasswordHash element
                    if(computedHash[i] != user.PasswordHash[i])
                    {
                        // If any element doesn't match then it means the passwords aren't equal
                        return Unauthorized("Invalid password.");
                    }
                }

            // If we go through the for loop then we can return the user
            return user;
            /**/
        }

        private async Task<bool> UserExists(string username)
        {
            // If there is a user that has a username that exists in our
            // database return true else return false
            // Lambda syntax = 'u => u.UserName == username.ToLower()'
            return await _context.Users.AnyAsync(u => u.UserName == username.ToLower());
        }
    }
}