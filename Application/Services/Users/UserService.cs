using AutoMapper;
using Database;
using Domain.Entity;
using Domain.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _database;


        public UserService(ApplicationContext db, IMapper mapper)
        {
            _mapper = mapper;
            _database= db;
        }

        public async Task RegistrationAndUpdateAsync(UserDto user,CancellationToken token)
        {
            //Create
            if (string.IsNullOrWhiteSpace(user.Id))
            {
                User newUser = _mapper.Map<User>(user);
                await _database.Users.AddAsync(newUser, token);
            }
            //Update
            else 
            {
                User updateUser = _mapper.Map<User>(user);
                _database.Users.Update(updateUser);
            }
            await _database.SaveChangesAsync(token);
        }

        public async Task DeleteUserAsync(Guid userId, CancellationToken token)
        {
            User user = await _database.Users.FirstOrDefaultAsync(u => u.Id == userId, token);

            if (user is null)
                return;

            _database.Users.Remove(user);
            await _database.SaveChangesAsync(token);
        }

        public async Task<UserDto> GetAsync(Guid userId, CancellationToken token)
        {
            var user = await _database.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId, token);

            if (user is null)
                return null;

            var response = _mapper.Map<UserDto>(user);
            return(response);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken token)
        {
            var users = await _database.Users
                .Include(u => u.Role)
                .ToListAsync(token);

            return _mapper.Map<IEnumerable<UserDto>>(users).ToList();
        }
    }
}
