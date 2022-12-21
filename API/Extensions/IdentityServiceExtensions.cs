using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices
        (this IServiceCollection services, IConfiguration config)
        {
            // Request will be inspected and it's up to the framework to decide if it's accepted
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        // Here we specify all the rules on how our servers knows that it's a good token
                        ValidateIssuerSigningKey = true, // Essential
                                                         // We use the key we passed exactly on our TokenService
                        IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(config["TokenKey"])),
                        // API is the issuer but we haven't implemented it because we need the token
                        // in order to validate the issuer and we are creating the token here
                        ValidateIssuer = false,
                        ValidateAudience = false // Information not passed in our token as well
                    };
                });

            return services;
        }
    }
}