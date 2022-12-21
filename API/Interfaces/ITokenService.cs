using API.Entities;

namespace API.Interfaces
{
    // Every class that implements this interface has to support the method in it
    
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}