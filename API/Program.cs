using API.Extensions;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container. // We add the extensions we created
builder.Services.AddApplicationServices(builder.Configuration); 
builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

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

app.Run();

