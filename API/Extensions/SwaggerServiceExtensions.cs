namespace API.Extensions
{
    // Need to install this nuget package to move on (latest version)
    // dotnet add TodoApi.csproj package Swashbuckle.AspNetCore -v 6.2.3
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerServices
        (this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            // Won't be using Swagger in Developer mode preferably so
            // if (WebApplication.CreateBuilder().Environment.IsDevelopment())
            // {
            //     services.UseSwagger();
            //     services.UseSwaggerUI();
            // }
            return services;
        }
    }
}