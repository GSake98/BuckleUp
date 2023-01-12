using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize] // Everything is accessible only if the user returns a JWT token to us
    // Every [AllowAnonymous] will get bypassed if [Authorize] is at the root level

    public class UsersController : BaseAPIController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetMembersAsync();

            // If we wrap it with Ok(); we get 200 response and it returns with no errors
            return Ok(users);
        }

        // To get a certain user we need a parameter inside [HttpGet]
        // If we don't wrap the return type into ActionResult we won't have
        // access to certain requests which would be significantly helpful later on
        // This will return a list of our users in the database and store them

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            // We can find now one specific user by their username
            return await _userRepository.GetMemberAsync(username);
        }
    }
}