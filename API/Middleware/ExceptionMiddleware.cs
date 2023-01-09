using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        // Middlewares need a RequestDelegate and also in the ILogger we give it the type
        // from the name of the class we using that we want to log out for such in this case
        // IHostEnvironment just lets us know if we run in Development mode or Production mode

        private readonly RequestDelegate _next; // Essential
        private readonly ILogger<ExceptionMiddleware> _logger; // Optional
        private readonly IHostEnvironment _env; // Optional

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
                IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        // This InvokeAsync is expected because middlewares work like this for RequestDelegate
        // Middleware did job -> next one did job -> next one did job hence why argument as next

        // This whole method will encounter the exceptions if they are not handled anywhere else in our app
        public async Task InvokeAsync(HttpContext context)
        {
            // Made for handling the exceptions. Needs to be done only in one place inside our middleware.
            try
            {
                await _next(context); // We pass as argument the context in our next (RequestDelegate)
            }
            catch (Exception ex) // If there is an exception in our app this piece will catch it eventually
            {
                _logger.LogError(ex, ex.Message); // This makes sure we see what is going on in terminal
                context.Response.ContentType = "application/json"; // Inside APIS this is the default
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Code of 500

                // If we use per say ex.stackTrace? the '?' makes it optional
                var response = _env.IsDevelopment() // '?' if IsDevelopment ':' if not IsDevelopment
                ? new APIException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                : new APIException(context.Response.StatusCode, ex.Message, "Internal Server Error");

                // Again, this is something APIs use by default but we are not in context of our API 
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options); // Make options into JsonSerializer

                await context.Response.WriteAsync(json); // Pass in our JsonSerialized object
            }
        }
    }
}