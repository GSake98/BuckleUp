using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        /* ENCRYPTION DECRYPTION COMMENTS
        // AsymmetricKey is when your server needs to encrypt something and the client
        // needs to decrypt something
        // Private key stays on server and Public key can be used to decrypt the data
        // SymmetricKey is used to encrypt as used to decrypt the data
        // We use Symmetric because even if our token is not encrypted our key will be
        // Also it will stay on the server and never go to the client because the client
        // doesn't need to decrypt the key
        /**/
        private readonly SymmetricSecurityKey _key;

        // This is where we will store our secret JWT key
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        // This will be created to the class it's injected into, after the controller is created
        // and will be desposed after the request reaches out of scope (this case HTTP closed)
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>()
            {
                // This is a claim. It's like someone claims that his UserName is 'George'
                // Users have a token that claim their UserName is what it's set in the token
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            // What key to encrypt and what algorithm to use for the encryption of that key
            var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // This is how we pass our claims
                Expires = DateTime.Now.AddDays(7), // Should be shorter but in Dev mode all good
                SigningCredentials = creds // Which credentials to pass in the tokenDecriptor
            };

            // After we described what our token is going to include we need a tokenHandler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Then we create our token and pass in our claims, exp etc.
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // We return our token through tokenHandler methods
            return tokenHandler.WriteToken(token);
        }
    }
}