using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ErrorController : BaseAPIController
    {
        private readonly DataContext _context;

        public ErrorController(DataContext context)
        {
            _context = context;
        }

        // The purpose of this is to make sure we return a 401 not authorized user when he's not authenticated
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            // We want to find a user that doesn't exist and its impossible for id to be -1
            var thing = _context.Users.Find(-1);
            if (thing == null)
            {
                return NotFound(); // If its null return NotFound
            }
            else
            {
                return thing; // If it has user return the user
            }
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var thing = _context.Users.Find(-1);

            // This is a sneaky way to ensure without errors that the program is going to work
            // We can't make a null var toString so we ensure it will throw a no reference exception
            var thingToReturn = thing.ToString();

            return thingToReturn;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was a bad request.");
        }
    }
}