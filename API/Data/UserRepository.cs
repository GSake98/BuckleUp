using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
               .Where(x => x.UserName == username) // Easy lambda expression
                                                   // Where to project it into and so it knows our profiles the configuration as well
                                                   // _mapper.ConfigurationProvider was added from our ApplicationServiceExtensions
               .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
               .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.Users.ProjectTo<MemberDto>
                        (_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            // Works for ints
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            // Works for strings
            return await _context.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            // For many objects
            // Also if we want related data we have to tell it explicitly to include them
            // We do it by using IGA - Loading the Entity "Include(p => p.Photos)"
            return await _context.Users
                .Include(p => p.Photos)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            // In order to return to bool we just make sure the changes are more than 0
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            // This just tells the EntityFrameworkTracker that something has changed
            // with the user entity that we passed in here --JUST INFORMING NOT SAVING--
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}