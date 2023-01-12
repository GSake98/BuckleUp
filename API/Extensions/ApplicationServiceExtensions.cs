using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    // This class is made so our Program.cs doesn't get messy
    // builder.Services replaced with services and Configuration with config

    // To create an extension we make the class static
    public static class ApplicationServiceExtensions
    {
        // We need to specify what we are extending hence the 'this'
        public static IServiceCollection AddApplicationServices
        (this IServiceCollection services, IConfiguration config)
        {
            // We want to add the dbcontext of the class we want to connect with AppUser
            services.AddDbContext<DataContext>(options =>
            {
                // We pass a lambda expression of our parameter to connect with the SQLServer
                // Then we pass builder.Configuration.GetConnectionString and give our db's name
                // The connection string will be inside appsettings.Development.json
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            // Added to trust API Header so we can use Users database
            services.AddCors();

            // To get the behavior we want (to not dispose after the request) we use 'AddScoped'
            // 1 AddTransient (disposes instantly) 2 AddScoped (for every request) 3 AddSingleton (never disposes)
            services.AddScoped<ITokenService, TokenService>();
            // Always add the other Interface service to our ApplicationServiceExtensions
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}