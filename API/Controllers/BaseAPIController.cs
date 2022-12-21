using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // [ApiController] is also needed to make a connection with the DTOs
    [ApiController]
    // Controller will be accessed like localhost/...GET.../api/users
    [Route("api/[controller]")]
    // the [controller] part is replaced with our class name minus the Controller
    // so it's https://localhost:5001/api/users

    public class BaseAPIController : ControllerBase
    {
        
    }
}