using AutoMapper;
using Domain.Services.Users;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System;
using Yard_Management_System.Entity;

namespace Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _db;


        public UserService(ApplicationContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db= db;
        }

        public async Task RegistrationAndUpdateAsync(UserDto user,CancellationToken token)
        {
            //Create
            if (string.IsNullOrWhiteSpace(user.Id))
            {
                User newUser = _mapper.Map<User>(user);
                await _db.Users.AddAsync(newUser, token);
            }
            //Update
            else 
            {
                User updateUser = _mapper.Map<User>(user);
                _db.Users.Update(updateUser);
            }
            await _db.SaveChangesAsync(token);
        }

        public async Task DeleteUserAsync(Guid userId, CancellationToken token)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId, token);

            if (user is null)
                return;

            _db.Users.Remove(user);
            await _db.SaveChangesAsync(token);
        }

        public async Task<UserDto> GetAsync(Guid userId, CancellationToken token)
        {
            var user = await _db.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId, token);

            if (user is null)
                return null;

            var response = _mapper.Map<UserDto>(user);
            return(response);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken token)
        {
            var users = await _db.Users
                .Include(u => u.Role)
                .ToListAsync(token);

            return _mapper.Map<IEnumerable<UserDto>>(users).ToList();
        }
    }
}
