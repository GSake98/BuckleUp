using API.Entities;
using API.DTOs;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        // Whichever script implements this interface will have access to everything inside
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        // Enumerable is less powerful than a list but if we only want to Get() and
        // iterate something then it is sufficient enough to be used for that ask
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id); // Gets the int id of the user
        Task<AppUser> GetUserByUsernameAsync(string username); // Gets the name of the user

        // We do this for a tiny but easy optimization that doesnt involve a lot of code
        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<MemberDto> GetMemberAsync(string username);
    }
}