using System.Text.Json.Serialization;
using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container. // We add the extensions we created
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

// Exception handling we created
app.UseMiddleware<ExceptionMiddleware>();

// ~~~ ORDER AND EXACT LETTERING MATTERS ~~~
// These are middleware we add so our http (last parameter) has access to anything prior
app.UseCors(builder =>
builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
app.UseHttpsRedirection();

// Authentication middleware must be AFTER UseCors and BEFORE MapControllers
app.UseAuthentication(); // Asks if i have a valid token
app.UseAuthorization(); // Sees i have a valid token and now says what we are allowed to do

// This is the middleware that will tell our request which
// API endpoint it needs to go to or a controller
app.MapControllers();

// This will give us all of the services we have access inside this program class
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
// Even though we have Exception handling middleware this is not an HTTP request
try
{
    var context = services.GetRequiredService<DataContext>();
    // Asynchronously applies any pending migrations for the context to the database
    await context.Database.MigrateAsync();
    // Call the method inside Seed class and pass the parameter
    await Seed.SeedUsers(context);
}
catch (Exception ex)
{
    // Logger needs to specify depending on where it runs in this case it is Program
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();

