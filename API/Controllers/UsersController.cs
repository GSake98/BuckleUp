using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize] // Everything is accessible only if the user returns a JWT token to us
    // Every [AllowAnonymous] will get bypassed if [Authorize] is at the root level

    public class UsersController : BaseAPIController
    {
        // This is just a readonly property that reads from DataContext
        private readonly DataContext _context;

        // Whenever we create an instance of DataContext we will have a session
        // from DataContext available to our database
        public UsersController(DataContext context)
        {
            // The argument we pass here is assigned so we have access to it
            _context = context;
        }
        // 21-34 Commented code about async-sync and API end points as well as HttpGet
        /*/
        To get a resource from an API end point we have to use [HttpGet]
        To get a list of our users we must '<>' them inside an IEnumerable
        to make sure it finds and counts all of them
        NEW //
        We want to make our code asynchronous so we wrap around a Task<>
        our return value and we use the keywords 'async' and 'await'
    
        Real life reference to async - sync code:
        Sync = Order taken, Waiter goes to Chef, Chef calls
        waiter as he is waiting, waiter gives to us
        Async = Order taken, Waiter goes to Chef, Chef calls waiter whenever waiter
        is done with the previous orders or already made ones, waiter gives to us
        /**/

        [AllowAnonymous] // Anyone can access this method
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            // If we don't wrap the return type into ActionResult we won't have
            // access to certain requests which would be significantly helpful later on
            // This will return a list of our users in the database and store them
            var users = await _context.Users.ToListAsync();
            return users;
        }

    // To get a certain user we need a parameter inside [HttpGet]
        [HttpGet("{id}")]

        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            // We can find now one specific user by their id
            // This only works if we use a primary key as a parameter
            var user = await _context.Users.FindAsync(id);
            return user;
        }
    }
}