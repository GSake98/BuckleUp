using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// We want to add the dbcontext of the class we want to connect with AppUser
builder.Services.AddDbContext<DataContext>(options => 
{
    // We pass a lambda expression of our parameter to connect with the SQLServer
    // Then we pass builder.Configuration.GetConnectionString and give our db's name
    // The connection string will be inside appsettings.Development.json
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Added to trust API Header so we can use Users database
builder.Services.AddCors();

builder.Services.AddControllers();

var app = builder.Build();

// ~~~ ORDER AND EXACT LETTERING MATTERS ~~~ 25-26L
// These are middleware we add so our http (last parameter) has access to anything prior
app.UseCors(builder => 
builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.UseHttpsRedirection(); 

app.UseAuthorization();

// This is the middleware that will tell our request which
// API endpoint it needs to go to or a controller
app.MapControllers();

app.Run();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Won't be using Swagger in Developer mode preferably so
// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }