using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    // Hover on DataContext to create the constructor
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        { }

        // Database name will be "Users" and have the columns are inside AppUser
        public DbSet<AppUser> Users {get;set;}
    }
}